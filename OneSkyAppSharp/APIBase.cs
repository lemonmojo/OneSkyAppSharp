using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;

namespace com.lemonmojo.OneSkyAppSharp
{
	internal static class APIBase
	{
		private const string BASE_URL = "https://platform.api.onesky.io/1/";

		internal static T GetResponse<T>(APIConfiguration configuration, string methodName, Dictionary<string, object> arguments = null, Method action = Method.GET)
		{
			if (configuration == null) {
				configuration = APIConfiguration.DefaultConfiguration;
			}

			VerifyAPIConfiguration(configuration);

			RestClient client = new RestClient(BASE_URL);
			RestRequest request = new RestRequest(methodName, action);

			arguments = AddAuthenticationParameters(arguments, configuration.PublicKey, configuration.SecretKey);

			foreach (string arg in arguments.Keys) {
				object val = arguments[arg];

				request.AddParameter(arg, val);
			}

			IRestResponse response = client.Execute(request);

			HandleErrorResponse(response);

			string contentType = response.ContentType.ToLower();

			if (typeof(T) == typeof(IRestResponse)) {
				return (T)response;
			} else if (contentType == "application/json") {
				T dataResponse = new JsonDeserializer().Deserialize<T>(response);
				return dataResponse;
			} else {
				throw new Exception("Unknown content type");
			}
		}

		private static string GetTimeStamp()
		{
			return DateTime.Now.ToUnixTimestamp().ToString();
		}

		private static string GetDevHash(string timeStamp, string secretKey)
		{
			return (timeStamp + secretKey).CalculateMD5Hash().ToLower();
		}

		private static Dictionary<string, object> AddAuthenticationParameters(Dictionary<string, object> arguments, string publicKey, string secretKey)
		{
			if (arguments == null) {
				arguments = new Dictionary<string, object>();
			}

			string timeStamp = GetTimeStamp();
			string hash = GetDevHash(timeStamp, secretKey);

			arguments["api_key"] = publicKey;
			arguments["timestamp"] = timeStamp;
			arguments["dev_hash"] = hash;

			return arguments;
		}

		private static void VerifyAPIConfiguration(APIConfiguration configuration)
		{
			if (configuration == null) {
				throw new ArgumentException("Configuration must not be null");
			}
		}

		private static BaseResponse HandleErrorResponse(IRestResponse response)
		{
			if (response.ErrorException != null) {
				throw response.ErrorException;
			}

			if (response.StatusCode == HttpStatusCode.OK) {
				return null;
			}

			JsonDeserializer ser = new JsonDeserializer();

			BaseResponse resp = ser.Deserialize<BaseResponse>(response);

			throw new Exception(resp.Meta.Message, new Exception(string.Format("Status: {0}", resp.Meta.Status)));
		}

		#region Utils
		private static int ToUnixTimestamp(this DateTime dateTime)
		{
			return (int)(dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
		}

		private static string CalculateMD5Hash(this string input)
		{
			// step 1, calculate MD5 hash from input
			MD5 md5 = MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);

			// step 2, convert byte array to hex string
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++) {
				sb.Append(hash[i].ToString("X2"));
			}

			return sb.ToString();
		}
		#endregion Utils

		/* #region API Calls
		#region Translation
		/// <summary>
		/// Export - export translations in files
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/translation.md#export---export-translations-in-files
		/// </summary>
		public byte[] Translation_Export(string projectId, string locale, string sourceFileName, string exportFileName = null)
		{
			string methodName = string.Format("projects/{0}/translations", projectId);

			Dictionary<string, string> args = new Dictionary<string, string>();
			args["locale"] = locale;
			args["source_file_name"] = sourceFileName;

			if (!string.IsNullOrEmpty(exportFileName)) {
				args["export_file_name"] = exportFileName;
			}

			return SendGetRequest(methodName, args) as byte[];
		}
		#endregion Translation

		#region Project type
		/// <summary>
		/// List - list all project types
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/project_type.md#list---list-all-project-types
		/// </summary>
		public string ProjectType_List()
		{
			return SendGetRequest("project-types") as string;
		}
		#endregion Project type
		#endregion API Calls */
	}
}
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
		#region Constants
		private const string BASE_URL = "https://platform.api.onesky.io/1/";
		#endregion Constants

		#region Internal Methods
		internal static T GetResponse<T>(APIConfiguration configuration, string methodName, Dictionary<string, object> arguments = null, Method action = Method.GET)
		{
			if (configuration == null) {
				configuration = APIConfiguration.DefaultConfiguration;
			}

			VerifyAPIConfiguration(configuration);

			if (arguments == null) {
				arguments = new Dictionary<string, object>();
			}

			arguments = AddConfigurationToArguments(configuration, arguments);

			RestClient client = new RestClient(BASE_URL);

			if (!methodName.EndsWith("/")) {
				methodName += "/";
			}

			methodName = AddArgumentsToMethodName(methodName, arguments);

			RestRequest request = new RestRequest(methodName, action);

			AddArgumentsToRequest(request, arguments);

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
		#endregion Internal Methods

		#region Private Methods
		private static string GetTimeStamp()
		{
			return DateTime.UtcNow.ToUnixTimestamp().ToString();
		}

		private static string GetDevHash(string timeStamp, string secretKey)
		{
			return (timeStamp + secretKey).CalculateMD5Hash().ToLower();
		}

		private static void VerifyAPIConfiguration(APIConfiguration configuration)
		{
			if (configuration == null) {
				throw new ArgumentException("Configuration must not be null");
			}
		}

		private static Dictionary<string, object> AddConfigurationToArguments(APIConfiguration configuration, Dictionary<string, object> arguments)
		{
			string apiKey = configuration.PublicKey;
			string timeStamp = GetTimeStamp();
			string devHash = GetDevHash(timeStamp, configuration.SecretKey);

			arguments["api_key"] = apiKey;
			arguments["timestamp"] = timeStamp;
			arguments["dev_hash"] = devHash;

			return arguments;
		}

		private static string AddArgumentsToMethodName(string methodName, Dictionary<string, object> arguments)
		{
			string paramSepChar = "?";

			foreach (string arg in arguments.Keys) {
				methodName += paramSepChar + arg + "={" + arg + "}";

				paramSepChar = "&";
			}

			return methodName;
		}

		private static void AddArgumentsToRequest(RestRequest request, Dictionary<string, object> arguments)
		{
			foreach (string arg in arguments.Keys) {
				object val = arguments[arg];

				request.AddParameter(arg, val, ParameterType.UrlSegment);
			}
		}

		private static BaseResponse HandleErrorResponse(IRestResponse response)
		{
			if (response.ErrorException != null) {
				throw response.ErrorException;
			}

			if (response.StatusCode == HttpStatusCode.OK ||
				response.StatusCode == HttpStatusCode.Created) {
				return null;
			}

			JsonDeserializer ser = new JsonDeserializer();

			BaseResponse resp = ser.Deserialize<BaseResponse>(response);

			throw new Exception(resp.Meta.Message, new Exception(string.Format("Status: {0}", resp.Meta.Status)));
		}
		#endregion Private Methods

		#region Utils
		private static int ToUnixTimestamp(this DateTime dateTime)
		{
			return (int)(dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
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
	}
}
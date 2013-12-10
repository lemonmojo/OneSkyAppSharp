using System;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace com.lemonmojo.OneSkyAppSharp
{
	public class OneSkyAppAPI
	{
		private const string BASE_URL = "https://platform.api.onesky.io/1/";

		#region Public Members
		public string PublicKey { get; private set; }
		public string SecretKey { get; private set; }

		public OneSkyAppAPI(string publicKey, string secretKey)
		{
			this.PublicKey = publicKey;
			this.SecretKey = secretKey;
		}

		public string ProjectGroup_List()
		{
			return SendGetRequest("project-groups") as string;
		}

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
		#endregion Public Members

		#region Private Members
		private string BuildUrl(string methodName, Dictionary<string, string> arguments)
		{
			if (arguments == null) {
				arguments = new Dictionary<string, string>();
			}

			string timeStamp = DateTime.Now.ToUnixTimestamp().ToString();
			string hash = (timeStamp + this.SecretKey).CalculateMD5Hash().ToLower();

			arguments["api_key"] = this.PublicKey;
			arguments["timestamp"] = timeStamp;
			arguments["dev_hash"] = hash;

			string argsStr = ArgumentStringFromDictionary(arguments);

			string url = string.Format("{0}{1}{2}", BASE_URL, methodName, argsStr);

			return url;
		}

		private string ArgumentStringFromDictionary(Dictionary<string, string> arguments)
		{
			string args = string.Empty;

			bool firstRun = true;

			foreach (string arg in arguments.Keys) {
				string val = arguments[arg];
				char sep = '&';

				if (firstRun) {
					sep = '?';
				}

				args += string.Format("{0}{1}={2}", sep, arg, val);

				firstRun = false;
			}

			return args;
		}

		private object SendGetRequest(string methodName, Dictionary<string, string> arguments = null)
		{
			string url = BuildUrl(methodName, arguments);
			Uri uri = new Uri(url);

			return SendGetRequest(uri);
		}

		private object SendGetRequest(Uri uri)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.Method = "GET";
			request.ContentType = "application/json";

			WebResponse webResponse = request.GetResponse();

			if (webResponse == null) {
				throw new Exception("No response");
			}

			using (Stream webStream = webResponse.GetResponseStream()) {
				if (webStream == null) {
					throw new Exception("No response stream");
				}

				string contentType = webResponse.ContentType.ToLower();

				using (StreamReader responseReader = new StreamReader(webStream)) {
					if (contentType == "application/json") {
						string response = responseReader.ReadToEnd();
						return response;
					} else if (contentType == "application/zip") {
						byte[] bytes = default(byte[]);

						using (MemoryStream memStream = new MemoryStream()) {
							responseReader.BaseStream.CopyTo(memStream);
							bytes = memStream.ToArray();
							return bytes;
						}
					} else {
						throw new Exception("Unknown content type in response.");
					}
				}
			}
		}
		#endregion Private Members
	}
}
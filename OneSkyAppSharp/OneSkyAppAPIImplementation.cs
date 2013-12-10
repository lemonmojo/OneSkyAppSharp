using System;
using System.IO;
using System.Net;
using System.Collections.Generic;

namespace com.lemonmojo.OneSkyAppSharp
{
	internal static class OneSkyAppAPIImplementation
	{
		private const string BASE_URL = "https://platform.api.onesky.io/1/";

		internal static object SendGetRequest(string publicKey, string secretKey, string methodName, Dictionary<string, string> arguments = null)
		{
			string url = BuildUrl(publicKey, secretKey, methodName, arguments);
			Uri uri = new Uri(url);

			return SendGetRequest(uri);
		}

		private static string BuildUrl(string publicKey, string secretKey, string methodName, Dictionary<string, string> arguments)
		{
			if (arguments == null) {
				arguments = new Dictionary<string, string>();
			}

			string timeStamp = DateTime.Now.ToUnixTimestamp().ToString();
			string hash = (timeStamp + secretKey).CalculateMD5Hash().ToLower();

			arguments["api_key"] = publicKey;
			arguments["timestamp"] = timeStamp;
			arguments["dev_hash"] = hash;

			string argsStr = ArgumentStringFromDictionary(arguments);

			string url = string.Format("{0}{1}{2}", BASE_URL, methodName, argsStr);

			return url;
		}

		private static string ArgumentStringFromDictionary(Dictionary<string, string> arguments)
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

		private static object SendGetRequest(Uri uri)
		{
			HttpWebRequest request = CreateWebRequest(uri, "GET");

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
						return GetStringResponse(responseReader);
					} else if (contentType == "application/zip") {
						return GetByteArrayResponse(responseReader);
					} else {
						throw new Exception("Unknown content type in response.");
					}
				}
			}
		}

		private static HttpWebRequest CreateWebRequest(Uri uri, string method)
		{
			HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
			request.Method = "GET";
			request.ContentType = "application/json";

			return request;
		}

		private static string GetStringResponse(StreamReader reader)
		{
			string responseStr = reader.ReadToEnd();
			return responseStr;
		}

		private static byte[] GetByteArrayResponse(StreamReader reader)
		{
			byte[] bytes = default(byte[]);

			using (MemoryStream memStream = new MemoryStream()) {
				reader.BaseStream.CopyTo(memStream);
				bytes = memStream.ToArray();
				return bytes;
			}
		}
	}
}


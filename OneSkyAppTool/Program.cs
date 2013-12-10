using System;
using com.lemonmojo.OneSkyAppSharp;

namespace com.lemonmojo.OneSkyAppTool
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			try {
				string publicKey = string.Empty;
				string secretKey = string.Empty;
				string methodName = string.Empty;

				try {
					publicKey = args[0]; // Your OneSkyApp Public Key
					secretKey = args[1]; // Your OneSkyApp Secret Key

					methodName = args[2]; // Translation_Export
				} catch (Exception) {
					throw new ArgumentException("Failed to parse command argument");
				}

				OneSkyAppAPI api = new OneSkyAppAPI(publicKey, secretKey);

				if (methodName == "Translation_Export") {
					string projectId = string.Empty; // 1234
					string locale = string.Empty; // en
					string sourceFile = string.Empty; // Localizable.strings
					string outFilename = string.Empty; // /Path/to/the/out/file.zip

					try {
						projectId = args[3];
						locale = args[4];
						sourceFile = args[5];
						outFilename = args[6];
					} catch (Exception) {
						throw new ArgumentException("Failed to parse arguments");
					}

					byte[] fileContent = api.Translation_Export(projectId, locale, sourceFile) as byte[];

					if (fileContent == null ||
						fileContent.Length <= 0) {
						throw new Exception("Empty response");
					}

					System.IO.File.WriteAllBytes(outFilename, fileContent);
				} else {
					throw new Exception("Unknown command: " + methodName);
				}
			} catch (Exception ex) {
				Console.Error.WriteLine("Error: " + ex.Message);
				Environment.Exit(1);
			}
		}
	}
}
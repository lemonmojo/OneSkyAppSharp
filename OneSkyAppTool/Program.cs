using System;
using com.lemonmojo.OneSkyAppSharp;
using com.lemonmojo.OneSkyAppSharp.API;

namespace com.lemonmojo.OneSkyAppTool
{
	class MainClass
	{
		#region General Stuff
		public static void Main(string[] args)
		{
			if (args == null ||
				args.Length == 0 ||
				args.Length == 2) {
				RunTests(args);
			} else {
				MainApp(args);
			}
		}

		private static APIConfiguration APIConfigurationFromArguments(string[] args)
		{
			return new APIConfiguration(
				args[0],
				args[1]
			);
		}
		#endregion General Stuff

		#region Main App
		private static void MainApp(string[] args)
		{
			try {
				APIConfiguration config = APIConfigurationFromArguments(args);

				string methodName = string.Empty;

				try {
					methodName = args[2]; // Translation_Export
				} catch (Exception) {
					throw new ArgumentException("Failed to parse command argument");
				}

				if (methodName == "Translation_Export") {
					int projectId = 0; // 1234
					string locale = string.Empty; // en
					string sourceFileName = string.Empty; // Localizable.strings
					string outFilename = string.Empty; // /Path/to/the/out/file.zip

					try {
						projectId = Int32.Parse(args[3]);
						locale = args[4];
						sourceFileName = args[5];
						outFilename = args[6];
					} catch (Exception) {
						throw new ArgumentException("Failed to parse arguments");
					}

					TranslationExportResponse resp = OneSkyAppSharp.API.Translation.Export(projectId, locale, sourceFileName, null, config);

					if (resp.Meta.Status != TranslationExportStatus.ExportReady) {
						throw new Exception(string.Format("Translation file not ready, status: {0}", resp.Meta.Status.ToString()));
					}

					byte[] fileContent = resp.Data;

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
		#endregion Main App

		#region Tests
		private static void RunTests(string[] args)
		{
			try {
				Console.WriteLine("==== Running OneSkyAppSharp Tests ====");

				APIConfiguration config = null;

				try {
					config = APIConfigurationFromArguments(args);
				} catch (Exception) {
					Console.Write("Enter your Public Key: ");
					string publicKey = Console.ReadLine();

					Console.Write("Enter your Secret Key: ");
					string secretKey = Console.ReadLine();

					config = new APIConfiguration(
						publicKey,
						secretKey
					);
				}

				config.SetAsDefault();

				LocaleListResponse localeList = Test_LocaleList();

				ProjectGroupListResponse projectGroupList = Test_ProjectGroupList();
				ProjectGroupShowResponse projectGroupShow = Test_ProjectGroupShow(projectGroupList.Data[0].ID);

				ProjectListResponse projectList = Test_ProjectList(projectGroupShow.Data[0].ID);
				ProjectShowResponse projectShow = Test_ProjectShow(projectList.Data[0].ID);

				TranslationExportResponse translationExport = Test_TranslationExport(projectList.Data[0].ID);
				TranslationStatusResponse translationStatus = Test_TranslationStatus(projectList.Data[0].ID, "Localizable.strings", "en");
			} catch (Exception ex) {
				Console.Error.WriteLine("Error: " + ex.Message);
				Environment.Exit(1);
			}
		}

		private static LocaleListResponse Test_LocaleList()
		{
			Console.WriteLine();
			Console.WriteLine("== Locale List ==");
			LocaleListResponse resp = OneSkyAppSharp.API.Locale.List();

			foreach (LocaleData item in resp.Data) {
				Console.WriteLine(item.LocalName);
			}

			return resp;
		}

		private static ProjectGroupListResponse Test_ProjectGroupList()
		{
			Console.WriteLine();
			Console.WriteLine("== Project Group List ==");
			ProjectGroupListResponse resp = OneSkyAppSharp.API.ProjectGroup.List();

			foreach (ProjectGroupData item in resp.Data) {
				Console.WriteLine(item.Name);
			}

			return resp;
		}

		private static ProjectGroupShowResponse Test_ProjectGroupShow(int projectGroupId)
		{
			Console.WriteLine();
			Console.WriteLine("== Project Group Show ==");
			ProjectGroupShowResponse resp = OneSkyAppSharp.API.ProjectGroup.Show(projectGroupId);

			foreach (ProjectGroupDetailsData item in resp.Data) {
				Console.WriteLine("{0}, Base Language Name: {1}", item.Name, item.BaseLanguage.LocalName);
			}

			return resp;
		}

		private static ProjectListResponse Test_ProjectList(int projectGroupId)
		{
			Console.WriteLine();
			Console.WriteLine("== Project List ==");
			ProjectListResponse resp = OneSkyAppSharp.API.Project.List(projectGroupId);

			foreach (ProjectData item in resp.Data) {
				Console.WriteLine(item.Name);
			}

			return resp;
		}

		private static ProjectShowResponse Test_ProjectShow(int projectId)
		{
			Console.WriteLine();
			Console.WriteLine("== Project Show ==");
			ProjectShowResponse resp = OneSkyAppSharp.API.Project.Show(projectId);

			foreach (ProjectDetailsData item in resp.Data) {
				Console.WriteLine("{0}, Word Count: {1}", item.Name, item.WordCount);
			}

			return resp;
		}

		private static TranslationExportResponse Test_TranslationExport(int projectId)
		{
			Console.WriteLine();
			Console.WriteLine("== Translation Export ==");
			TranslationExportResponse resp = OneSkyAppSharp.API.Translation.Export(projectId, "en", "Localizable.strings");

			if (resp != null) {
				Console.WriteLine("Status: {0}, File Length: {1}", resp.Meta.Status.ToString(), resp.Data != null ? resp.Data.Length : 0);
			}

			return resp;
		}

		private static TranslationStatusResponse Test_TranslationStatus(int projectId, string fileName, string locale)
		{
			Console.WriteLine();
			Console.WriteLine("== Translation Status ==");
			TranslationStatusResponse resp = OneSkyAppSharp.API.Translation.Status(projectId, fileName, locale);

			Console.WriteLine("Filename: {0}, Progress: {1}", resp.Data.FileName, resp.Data.Progress);

			return resp;
		}
		#endregion Tests
	}
}
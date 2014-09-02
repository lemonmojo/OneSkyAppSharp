using System;
using System.Collections.Generic;
using RestSharp;

namespace com.lemonmojo.OneSkyAppSharp.API.Translation
{
	/// <summary>
	/// Translation (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/translation.md"/>)
	/// </summary>
	/// <remarks>
	/// API implementation status: 100% (2/2)
	/// </remarks>
	public static class TranslationAPI
	{
		/// <summary>
		/// Export - export translations in files
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/translation.md#export---export-translations-in-files
		/// </summary>
		public static TranslationExportResponse Export(int projectId, string locale, string sourceFileName, string exportFileName = null, APIConfiguration configuration = null)
		{
			string methodName = string.Format("projects/{0}/translations", projectId.ToString());

			Dictionary<string, object> args = new Dictionary<string, object>();
			args["locale"] = locale;
			args["source_file_name"] = sourceFileName;

			if (!string.IsNullOrEmpty(exportFileName)) {
				args["export_file_name"] = exportFileName;
			}

			IRestResponse resp = APIBase.GetResponse<IRestResponse>(configuration, methodName, args);

			TranslationExportStatus status = TranslationExportStatus.Unknown;

			try {
				status = (TranslationExportStatus)resp.StatusCode;
			} catch (Exception) { }

			TranslationExportResponse dataResponse = new TranslationExportResponse() {
				Meta = new TranslationExportMetadata() { Status = status },
				Data = resp.Content
			};

			return dataResponse;
		}

		/// <summary>
		/// Status - translations status
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/translation.md#status---translations-status
		/// </summary>
		public static TranslationStatusResponse Status(int projectId, string fileName, string locale, APIConfiguration configuration = null)
		{
			string methodName = string.Format("projects/{0}/translations/status", projectId.ToString());

			Dictionary<string, object> args = new Dictionary<string, object>();

			args["file_name"] = fileName;
			args["locale"] = locale;

			return APIBase.GetResponse<TranslationStatusResponse>(configuration, methodName, args);
		}
	}
}
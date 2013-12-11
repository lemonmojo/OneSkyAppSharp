using System;
using System.Collections.Generic;
using RestSharp;

namespace com.lemonmojo.OneSkyAppSharp.API
{
	/// <summary>
	/// Translation
	/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/translation.md
	/// API implementation status: 2/2
	/// </summary>
	public static class Translation
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
				Data = resp.RawBytes
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

	public class TranslationExportResponse
	{
		public TranslationExportMetadata Meta { get; set; }
		public byte[] Data { get; set; }
	}

	public class TranslationExportMetadata
	{
		public TranslationExportStatus Status { get; set; }
	}

	public enum TranslationExportStatus
	{
		Unknown = 0,
		ExportReady = 200,
		Processing = 202,
		NoStringReady = 204
	}

	public class TranslationStatusResponse : BaseResponse
	{
		public TranslationStatusData Data { get; set; }
	}

	public class TranslationStatusData
	{
		public string FileName { get; set; }
		public LocaleData Locale { get; set; }
		public string Progress { get; set; }
		public long StringCount { get; set; }
		public long WordCount { get; set; }
	}
}
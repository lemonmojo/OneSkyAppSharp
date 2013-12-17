using System;
using System.Collections.Generic;
using com.lemonmojo.OneSkyAppSharp.API.Locale;

namespace com.lemonmojo.OneSkyAppSharp.API.Translation
{
	#region Response Classes
	public class TranslationExportResponse
	{
		public TranslationExportMetadata Meta { get; set; }
		public byte[] Data { get; set; }
	}

	public class TranslationStatusResponse : BaseResponse
	{
		public TranslationStatusData Data { get; set; }
	}
	#endregion Response Classes

	#region Data Classes
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
	
	public class TranslationStatusData
	{
		public string FileName { get; set; }
		public LocaleData Locale { get; set; }
		public string Progress { get; set; }
		public long StringCount { get; set; }
		public long WordCount { get; set; }
	}
	#endregion Data Classes

}
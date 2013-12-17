using System;
using System.Collections.Generic;
using com.lemonmojo.OneSkyAppSharp.API.Locale;

namespace com.lemonmojo.OneSkyAppSharp.API.File
{
	#region Response Classes
	public class FileListResponse : BaseResponse
	{
		public List<FileListData> Data { get; set; }
	}

	public class FileUploadResponse : BaseResponse
	{
		public FileUploadData Data { get; set; }
	}

	public class FileDeleteResponse : BaseResponse
	{

	}
	#endregion Response Classes

	#region Data Classes
	public class FileListData
	{
		public string Name { get; set; }
		public long StringCount { get; set; }
		public long WordCount { get; set; }
		public FileLastImportData LastImport { get; set; }
		public DateTime UploadedAt { get; set; }
		public long UploadedAtTimestamp { get; set; }
	}

	public class FileLastImportData
	{
		public int ID { get; set; }
		public string Status { get; set; }
	}

	public class FileUploadData
	{
		public string Name { get; set; }
		public string Format { get; set; }
		public LocaleData Language { get; set; }
		public FileImportData Import { get; set; }
	}

	public class FileImportData
	{
		public int ID { get; set; }
		public DateTime CreatedAt { get; set; }
		public long CreatedAtTimestamp { get; set; }
	}

	public static class FileFormats
	{
		public const string IOS_STRINGS = "IOS_STRINGS";
		public const string GNU_PO = "GNU_PO";
		public const string ANDROID_XML = "ANDROID_XML";
		public const string ANDROID_JSON = "ANDROID_JSON";
		public const string JAVA_PROPERTIES = "JAVA_PROPERTIES";
		public const string RUBY_YML = "RUBY_YML";
		public const string RUBY_YAML = "RUBY_YAML";
		public const string FLASH_XML = "FLASH_XML";
		public const string GNU_POT = "GNU_POT";
		public const string RRC = "RRC";
		public const string RESX = "RESX";
		public const string RESJSON = "RESJSON";
		public const string HIERARCHICAL_JSON = "HIERARCHICAL_JSON";
		public const string PHP = "PHP";
		public const string PHP_VARIABLES = "PHP_VARIABLES";
		public const string HTML = "HTML";
		public const string RESW = "RESW";
		public const string YML = "YML";
		public const string YAML = "YAML";
		public const string ADEMPIERE_XML = "ADEMPIERE_XML";
		public const string QT_TS_XML = "QT_TS_XML";
		public const string TMX = "TMX";
		public const string L10N = "L10N";
		public const string INI = "INI";
	}
	#endregion Data Classes
}
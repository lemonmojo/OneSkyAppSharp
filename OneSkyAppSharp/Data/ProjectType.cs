using System;
using System.Collections.Generic;

namespace com.lemonmojo.OneSkyAppSharp.API.ProjectType
{
	#region Response Classes
	public class ProjectTypeListResponse : BaseResponse
	{
		public List<ProjectTypeData> Data { get; set; }
	}
	#endregion Response Classes

	#region Data Classes
	public class ProjectTypeData
	{
		public string Code { get; set; }
		public string Name { get; set; }
	}

	public static class ProjectTypes
	{
		public const string IPHONE_IPAD_APP = "ios";
		public const string ANDROID_APP = "android";
		public const string WINDOWS_PHONE = "windows-phone";
		public const string WINDOWS_METRO_APP = "windows-metro";
		public const string BLACKBERRY_APP = "blackberry";
		public const string REGULAR_WEBSITE = "website";
		public const string OTHERS_APP = "app-others";
		public const string REGULAR_WEB_APP = "webapp";
		public const string FACEBOOK_APP = "facebook";
		public const string GITHUB_PROJECT = "github";
		public const string SPREADSHEET = "document-spreadsheet";
		public const string OTHERS_WEB_APP = "webapp-others";
		public const string MISCELLANEOUS = "misc";
	}
	#endregion Data Classes
}
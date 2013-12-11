using System;
using System.Collections.Generic;

namespace com.lemonmojo.OneSkyAppSharp.API.Locale
{
	#region Response Classes
	public class LocaleListResponse : BaseResponse
	{
		public List<LocaleData> Data { get; set; }
	}
	#endregion Response Classes

	#region Data Classes
	public class LocaleData
	{
		public string Code { get; set; }
		public string EnglishName { get; set; }
		public string LocalName { get; set; }
		public string Locale { get; set; }
		public string Region { get; set; }
	}
	#endregion Data Classes
}
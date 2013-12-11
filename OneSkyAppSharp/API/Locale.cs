using System;
using System.Collections.Generic;

namespace com.lemonmojo.OneSkyAppSharp.API
{
	/// <summary>
	/// Locale
	/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/locale.md
	/// API implementation status: 1/1
	/// </summary>
	public static class Locale
	{
		/// <summary>
		/// List - list all locales
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/locale.md#list---list-all-locales
		/// </summary>
		public static LocaleListResponse List(APIConfiguration configuration = null)
		{
			return APIBase.GetResponse<LocaleListResponse>(configuration, "locales");
		}
	}

	public class LocaleListResponse : BaseResponse
	{
		public List<LocaleData> Data { get; set; }
	}

	public class LocaleData
	{
		public string Code { get; set; }
		public string EnglishName { get; set; }
		public string LocalName { get; set; }
		public string Locale { get; set; }
		public string Region { get; set; }
	}
}
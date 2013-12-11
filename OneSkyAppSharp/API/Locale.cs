using System;

namespace com.lemonmojo.OneSkyAppSharp.API.Locale
{
	/// <summary>
	/// Locale (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/locale.md"/>)
	/// </summary>
	/// <remarks>
	/// API implementation status: 1/1
	/// </remarks>
	public static class LocaleAPI
	{
		/// <summary>
		/// List - list all locales (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/locale.md#list---list-all-locales"/>)
		/// </summary>
		public static LocaleListResponse List(APIConfiguration configuration = null)
		{
			return APIBase.GetResponse<LocaleListResponse>(configuration, "locales");
		}
	}
}
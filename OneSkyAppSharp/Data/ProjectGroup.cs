using System;
using System.Collections.Generic;
using com.lemonmojo.OneSkyAppSharp.API.Locale;

namespace com.lemonmojo.OneSkyAppSharp.API.ProjectGroup
{
	#region Response Classes
	public class ProjectGroupListResponse : BaseResponse
	{
		public List<ProjectGroupData> Data { get; set; }
	}

	public class ProjectGroupShowResponse : BaseResponse
	{
		public List<ProjectGroupDetailsData> Data { get; set; }
	}

	public class ProjectGroupCreateResponse : BaseResponse
	{
		public ProjectGroupCreateData Data { get; set; }
	}

	public class ProjectGroupRenameResponse : BaseResponse
	{

	}

	public class ProjectGroupDeleteResponse : BaseResponse
	{

	}

	public class ProjectGroupListLanguagesResponse : BaseResponse
	{
		public ProjectGroupListLanguagesData Data { get; set; }
	}
	#endregion Response Classes

	#region Data Classes
	public class ProjectGroupData
	{
		public int ID { get; set; }
		public string Name { get; set; }
	}

	public class ProjectGroupDetailsData : ProjectGroupData
	{
		public LocaleData BaseLanguage { get; set; }
		public int EnabledLanguageCount { get; set; }
		public int ProjectCount { get; set; }
	}

	public class ProjectGroupCreateData : ProjectGroupData
	{
		public LocaleData BaseLanguage { get; set; }
	}

	public class ProjectGroupListLanguagesData
	{
		public LocaleData BaseLanguage { get; set; }
		public ProjectGroupListEnabledLanguagesData EnabledLanguages { get; set; }
	}

	public class ProjectGroupListEnabledLanguagesData
	{
		public int Count { get; set; }
		public List<LocaleData> Languages { get; set; }
	}
	#endregion Data Classes
}
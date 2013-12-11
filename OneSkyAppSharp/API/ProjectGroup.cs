using System;
using System.Collections.Generic;

namespace com.lemonmojo.OneSkyAppSharp.API
{
	/// <summary>
	/// Project Group
	/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md
	/// API implementation status: 2/6
	/// </summary>
	public static class ProjectGroup
	{
		/// <summary>
		/// List - retrieve all project groups
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md#list---retrieve-all-project-groups
		/// </summary>
		public static ProjectGroupListResponse List(int page = 1, int groupsPerPage = 50, APIConfiguration configuration = null)
		{
			Dictionary<string, object> args = new Dictionary<string, object>();

			if (page != 1) {
				args["page"] = page;
			}

			if (groupsPerPage != 50) {
				args["per_page"] = groupsPerPage;
			}

			return APIBase.GetResponse<ProjectGroupListResponse>(configuration, "project-groups", args);
		}

		/// <summary>
		/// Show - retrieve details of a project group
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md#show---retrieve-details-of-a-project-group
		/// </summary>
		public static ProjectGroupShowResponse Show(int projectGroupId, APIConfiguration configuration = null)
		{
			string methodName = string.Format("project-groups/{0}", projectGroupId.ToString());

			return APIBase.GetResponse<ProjectGroupShowResponse>(configuration, methodName, null);
		}
	}

	public class ProjectGroupListResponse : BaseResponse
	{
		public List<ProjectGroupData> Data { get; set; }
	}

	public class ProjectGroupShowResponse : BaseResponse
	{
		public List<ProjectGroupDetailsData> Data { get; set; }
	}

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
}
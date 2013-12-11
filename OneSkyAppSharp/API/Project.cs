using System;
using System.Collections.Generic;

namespace com.lemonmojo.OneSkyAppSharp.API
{
	/// <summary>
	/// Project
	/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/project.md
	/// API implementation status: 2/5
	/// </summary>
	public static class Project
	{
		/// <summary>
		/// List - list projects of a project group
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/project.md#list---list-projects-of-a-project-group
		/// </summary>
		public static ProjectListResponse List(int projectGroupId, APIConfiguration configuration = null)
		{
			string methodName = string.Format("project-groups/{0}/projects", projectGroupId.ToString());

			return APIBase.GetResponse<ProjectListResponse>(configuration, methodName);
		}

		/// <summary>
		/// Show - retrieve details of a project
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/project.md#show---retrieve-details-of-a-project
		/// </summary>
		public static ProjectShowResponse Show(int projectId, APIConfiguration configuration = null)
		{
			string methodName = string.Format("projects/{0}", projectId.ToString());

			return APIBase.GetResponse<ProjectShowResponse>(configuration, methodName);
		}
	}

	public class ProjectListResponse : BaseResponse
	{
		public List<ProjectData> Data { get; set; }
	}

	public class ProjectShowResponse : BaseResponse
	{
		public List<ProjectDetailsData> Data { get; set; }
	}

	public class ProjectData
	{
		public int ID { get; set; }
		public string Name { get; set; }
	}

	public class ProjectDetailsData : ProjectData
	{
		public string Description { get; set; }
		public ProjectTypeData ProjectType { get; set; }
		public long StringCount { get; set; }
		public long WordCount { get; set; }
	}

	public class ProjectTypeData
	{
		public string Code { get; set; }
		public string Name { get; set; }
	}
}
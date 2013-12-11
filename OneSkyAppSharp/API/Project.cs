using System;

namespace com.lemonmojo.OneSkyAppSharp.API.Project
{
	/// <summary>
	/// Project (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project.md"/>)
	/// </summary>
	/// <remarks>
	/// API implementation status: 2/5
	/// </remarks>
	public static class ProjectAPI
	{
		/// <summary>
		/// List - list projects of a project group (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project.md#list---list-projects-of-a-project-group"/>)
		/// </summary>
		public static ProjectListResponse List(int projectGroupId, APIConfiguration configuration = null)
		{
			string methodName = string.Format("project-groups/{0}/projects", projectGroupId.ToString());

			return APIBase.GetResponse<ProjectListResponse>(configuration, methodName);
		}

		/// <summary>
		/// Show - retrieve details of a project (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project.md#show---retrieve-details-of-a-project"/>)
		/// </summary>
		public static ProjectShowResponse Show(int projectId, APIConfiguration configuration = null)
		{
			string methodName = string.Format("projects/{0}", projectId.ToString());

			return APIBase.GetResponse<ProjectShowResponse>(configuration, methodName);
		}
	}
}
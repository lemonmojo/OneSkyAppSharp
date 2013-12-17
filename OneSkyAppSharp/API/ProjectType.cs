using System;
using System.Collections.Generic;

namespace com.lemonmojo.OneSkyAppSharp.API.ProjectType
{
	/// <summary>
	/// Project type (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project_type.md"/>)
	/// </summary>
	/// <remarks>
	/// API implementation status: 100% (1/1)
	/// </remarks>
	public static class ProjectTypeAPI
	{
		/// <summary>
		/// List - list all project types (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project_type.md#list---list-all-project-types"/>)
		/// </summary>
		public static ProjectTypeListResponse List(APIConfiguration configuration = null)
		{
			return APIBase.GetResponse<ProjectTypeListResponse>(configuration, "project-types");
		}
	}
}
using System;
using System.Collections.Generic;

namespace com.lemonmojo.OneSkyAppSharp.API.File
{
	/// <summary>
	/// File (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/file.md"/>)
	/// </summary>
	/// <remarks>
	/// API implementation status: 1/3
	/// </remarks>
	public static class FileAPI
	{
		/// <summary>
		/// List - list uploaded files (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/file.md#list---list-uploaded-files"/>)
		/// </summary>
		public static FileListResponse List(int projectId, int page = 1, int groupsPerPage = 50, APIConfiguration configuration = null)
		{
			string methodName = string.Format("projects/{0}/files", projectId);

			Dictionary<string, object> args = new Dictionary<string, object>();

			if (page != 1) {
				args["page"] = page;
			}

			if (groupsPerPage != 50) {
				args["per_page"] = groupsPerPage;
			}

			return APIBase.GetResponse<FileListResponse>(configuration, methodName, args);
		}
	}
}
using System;
using System.Collections.Generic;
using RestSharp;

namespace com.lemonmojo.OneSkyAppSharp.API.ProjectGroup
{
	/// <summary>
	/// Project Group (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md"/>)
	/// </summary>
	/// <remarks>
	/// API implementation status: 6/6
	/// </remarks>
	public static class ProjectGroupAPI
	{
		/// <summary>
		/// List - retrieve all project groups (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md#list---retrieve-all-project-groups"/>)
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
		/// Show - retrieve details of a project group (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md#show---retrieve-details-of-a-project-group"/>)
		/// </summary>
		public static ProjectGroupShowResponse Show(int projectGroupId, APIConfiguration configuration = null)
		{
			string methodName = string.Format("project-groups/{0}", projectGroupId);

			return APIBase.GetResponse<ProjectGroupShowResponse>(configuration, methodName, null);
		}

		/// <summary>
		/// Create - create a new project group (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md#create---create-a-new-project-group"/>)
		/// </summary>
		public static ProjectGroupCreateResponse Create(string name, string locale = "en", APIConfiguration configuration = null)
		{
			// Should actually return a rich ProjectGroupCreateResponse including Data but it seems there is a bug in the API and it returns an empty string right now

			Dictionary<string, object> args = new Dictionary<string, object>();

			args["name"] = name;

			if (locale != "en") {
				args["locale"] = locale;
			}

			IRestResponse resp = APIBase.GetResponse<IRestResponse>(configuration, "project-groups", args, RestSharp.Method.POST);

			if (resp.StatusCode != System.Net.HttpStatusCode.Created) {
				throw new Exception("Failed to create Project Group", new Exception(string.Format("Status: {0}", (int)resp.StatusCode)));
			}

			ProjectGroupCreateResponse respData = new ProjectGroupCreateResponse() {
				Meta = new Metadata() { Status = (int)resp.StatusCode },
				Data = null
			};

			return respData;
		}

		/// <summary>
		/// Rename - update name of a project group (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md#rename---update-name-of-a-project-group"/>)
		/// </summary>
		public static ProjectGroupRenameResponse Rename(int projectGroupId, string name, APIConfiguration configuration = null)
		{
			Dictionary<string, object> args = new Dictionary<string, object>();

			args["name"] = name;

			string methodName = string.Format("project-groups/{0}/rename", projectGroupId);

			IRestResponse resp = APIBase.GetResponse<IRestResponse>(configuration, methodName, args, RestSharp.Method.PUT);

			if (resp.StatusCode != System.Net.HttpStatusCode.OK) {
				throw new Exception("Failed to rename Project Group", new Exception(string.Format("Status: {0}", (int)resp.StatusCode)));
			}

			ProjectGroupRenameResponse respData = new ProjectGroupRenameResponse() {
				Meta = new Metadata() { Status = (int)resp.StatusCode }
			};

			return respData;
		}

		/// <summary>
		/// Delete - remove a project group (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md#delete---remove-a-project-group"/>)
		/// </summary>
		public static ProjectGroupDeleteResponse Delete(int projectGroupId, APIConfiguration configuration = null)
		{
			string methodName = string.Format("project-groups/{0}", projectGroupId);

			IRestResponse resp = APIBase.GetResponse<IRestResponse>(configuration, methodName, null, RestSharp.Method.DELETE);

			if (resp.StatusCode != System.Net.HttpStatusCode.OK) {
				throw new Exception("Failed to delete Project Group", new Exception(string.Format("Status: {0}", (int)resp.StatusCode)));
			}

			ProjectGroupDeleteResponse respData = new ProjectGroupDeleteResponse() {
				Meta = new Metadata() { Status = (int)resp.StatusCode }
			};

			return respData;
		}

		/// <summary>
		/// Languages - list enabled languages of a project group (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md#languages---list-enabled-languages-of-a-project-group"/>)
		/// </summary>
		public static ProjectGroupListLanguagesResponse ListLanguages(int projectGroupId, APIConfiguration configuration = null)
		{
			string methodName = string.Format("project-groups/{0}/languages", projectGroupId);

			return APIBase.GetResponse<ProjectGroupListLanguagesResponse>(configuration, methodName);
		}
	}
}
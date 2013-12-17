using System;
using System.Collections.Generic;
using RestSharp;

namespace com.lemonmojo.OneSkyAppSharp.API.Project
{
	/// <summary>
	/// Project (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project.md"/>)
	/// </summary>
	/// <remarks>
	/// API implementation status: 5/5
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

		/// <summary>
		/// Create - create a new project (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project.md#create---create-a-new-project"/>)
		/// </summary>
		public static ProjectCreateResponse Create(int projectGroupId, string projectType, string name = "", string description = "", APIConfiguration configuration = null)
		{
			string methodName = string.Format("project-groups/{0}/projects", projectGroupId.ToString());

			Dictionary<string, object> args = new Dictionary<string, object>();

			args["project_type"] = projectType;

			if (!string.IsNullOrEmpty(name)) {
				args["name"] = name;
			}

			if (!string.IsNullOrEmpty(description)) {
				args["description"] = description;
			}

			ProjectCreateResponse resp = APIBase.GetResponse<ProjectCreateResponse>(configuration, methodName, args, Method.POST);

			if (resp.Meta.Status != (int)System.Net.HttpStatusCode.Created) {
				throw new Exception("Failed to create Project", new Exception(string.Format("Status: {0}", (int)resp.Meta.Status)));
			}

			return resp;
		}

		/// <summary>
		/// Update - update project (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project.md#update---update-project"/>)
		/// </summary>
		public static ProjectUpdateResponse Update(int projectId, string name = "", string description = "", APIConfiguration configuration = null)
		{
			string methodName = string.Format("projects/{0}", projectId);

			Dictionary<string, object> args = new Dictionary<string, object>();

			if (!string.IsNullOrEmpty(name)) {
				args["name"] = name;
			}

			if (!string.IsNullOrEmpty(description)) {
				args["description"] = description;
			}

			IRestResponse resp = APIBase.GetResponse<IRestResponse>(configuration, methodName, args, RestSharp.Method.PUT);

			if (resp.StatusCode != System.Net.HttpStatusCode.OK) {
				throw new Exception("Failed to update Project", new Exception(string.Format("Status: {0}", (int)resp.StatusCode)));
			}

			ProjectUpdateResponse respData = new ProjectUpdateResponse() {
				Meta = new Metadata() { Status = (int)resp.StatusCode }
			};

			return respData;
		}

		/// <summary>
		/// Delete - remove project (<see cref="https://github.com/onesky/api-documentation-platform/blob/master/resources/project.md#delete---remove-project"/>)
		/// </summary>
		public static ProjectDeleteResponse Delete(int projectId, APIConfiguration configuration = null)
		{
			string methodName = string.Format("projects/{0}", projectId);

			IRestResponse resp = APIBase.GetResponse<IRestResponse>(configuration, methodName, null, RestSharp.Method.DELETE);

			if (resp.StatusCode != System.Net.HttpStatusCode.OK) {
				throw new Exception("Failed to delete Project", new Exception(string.Format("Status: {0}", (int)resp.StatusCode)));
			}

			ProjectDeleteResponse respData = new ProjectDeleteResponse() {
				Meta = new Metadata() { Status = (int)resp.StatusCode }
			};

			return respData;
		}
	}
}
using System;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace com.lemonmojo.OneSkyAppSharp
{
	public class OneSkyAppAPI
	{
		#region Public Members
		public string PublicKey { get; private set; }
		public string SecretKey { get; private set; }
		#endregion Public Members

		#region Constructor
		public OneSkyAppAPI(string publicKey, string secretKey)
		{
			if (string.IsNullOrWhiteSpace(publicKey)) {
				throw new ArgumentException("Public Key must not be empty.");
			}

			if (string.IsNullOrWhiteSpace(secretKey)) {
				throw new ArgumentException("Secret Key must not be empty.");
			}

			this.PublicKey = publicKey;
			this.SecretKey = secretKey;

		}
		#endregion Constructor

		#region Private Methods
		private object SendGetRequest(string methodName, Dictionary<string, string> arguments = null)
		{
			return OneSkyAppAPIImplementation.SendGetRequest(
				this.PublicKey, 
				this.SecretKey,
				methodName,
				arguments
			);
		}
		#endregion Private Methods

		#region API Calls
		#region Project Group
		/// <summary>
		/// List - retrieve all project groups
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md#list---retrieve-all-project-groups
		/// </summary>
		public string ProjectGroup_List()
		{
			return SendGetRequest("project-groups") as string;
		}

		/// <summary>
		/// Show - retrieve details of a project group
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md#show---retrieve-details-of-a-project-group
		/// </summary>
		public string ProjectGroup_Show(string projectGroupId)
		{
			string methodName = string.Format("project-groups/{0}", projectGroupId);

			return SendGetRequest(methodName, null) as string;
		}

		/// <summary>
		/// Languages - list enabled languages of a project group
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/project_group.md#languages---list-enabled-languages-of-a-project-group
		/// </summary>
		public string ProjectGroup_Languages(string projectGroupId)
		{
			string methodName = string.Format("project-groups/{0}/languages", projectGroupId);

			return SendGetRequest(methodName) as string;
		}
		#endregion Project Group

		#region Translation
		/// <summary>
		/// Export - export translations in files
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/translation.md#export---export-translations-in-files
		/// </summary>
		public byte[] Translation_Export(string projectId, string locale, string sourceFileName, string exportFileName = null)
		{
			string methodName = string.Format("projects/{0}/translations", projectId);

			Dictionary<string, string> args = new Dictionary<string, string>();
			args["locale"] = locale;
			args["source_file_name"] = sourceFileName;

			if (!string.IsNullOrEmpty(exportFileName)) {
				args["export_file_name"] = exportFileName;
			}

			return SendGetRequest(methodName, args) as byte[];
		}
		#endregion Translation

		#region Locale
		/// <summary>
		/// List - list all locales
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/locale.md#list---list-all-locales
		/// </summary>
		public string Locale_List()
		{
			return SendGetRequest("locales") as string;
		}
		#endregion Locale

		#region Project type
		/// <summary>
		/// List - list all project types
		/// Documentation: https://github.com/onesky/api-documentation-platform/blob/master/resources/project_type.md#list---list-all-project-types
		/// </summary>
		public string ProjectType_List()
		{
			return SendGetRequest("project-types") as string;
		}
		#endregion Project type
		#endregion API Calls
	}
}
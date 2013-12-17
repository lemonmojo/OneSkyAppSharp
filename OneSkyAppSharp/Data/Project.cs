using System;
using System.Collections.Generic;
using com.lemonmojo.OneSkyAppSharp.API.ProjectType;

namespace com.lemonmojo.OneSkyAppSharp.API.Project
{
	#region Response Classes
	public class ProjectListResponse : BaseResponse
	{
		public List<ProjectData> Data { get; set; }
	}

	public class ProjectShowResponse : BaseResponse
	{
		public List<ProjectDetailsData> Data { get; set; }
	}

	public class ProjectCreateResponse : BaseResponse
	{
		public ProjectCreateData Data { get; set; }
	}

	public class ProjectUpdateResponse : BaseResponse
	{

	}

	public class ProjectDeleteResponse : BaseResponse
	{

	}
	#endregion Response Classes

	#region Data Classes
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

	public class ProjectCreateData : ProjectData
	{
		public string Description { get; set; }
		public ProjectTypeData ProjectType { get; set; }
	}
	#endregion Data Classes
}
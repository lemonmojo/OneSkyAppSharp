using System;

namespace com.lemonmojo.OneSkyAppSharp
{
	public class Metadata
	{
		public int Status { get; set; }
		public string Message { get; set; }

		public long RecordCount { get; set; }
		public long PageCount { get; set; }
		public string NextPage { get; set; }
		public string PrevPage { get; set; }
		public string FirstPage { get; set; }
		public string LastPage { get; set; }
	}
}
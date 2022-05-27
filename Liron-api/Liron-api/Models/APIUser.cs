
using System;
namespace Liron_api.Models
{
public class APIUser
{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Server { get; set; }
		public string Last { get; set; }
		public string Lastdate { get; set; }

		public APIUser(string id, string name, string server, string last, string lastdate)
		{
			this.Id = id;
			this.Name = name;
			this.Server = server;
			this.Last = last;
			this.Lastdate = lastdate;		}
	}
}


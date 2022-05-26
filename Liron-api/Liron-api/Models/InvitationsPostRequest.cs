using System;
namespace Liron_api.Models
{
public class InvitationsPostRequest
	{
		public string From { get; set; }
		public string To { get; set; }
		public string Server { get; set; }
		public InvitationsPostRequest(string id, string name, string server)
		{
			this.From = id;
			this.To = name;
			this.Server = server;
		}
	}
}
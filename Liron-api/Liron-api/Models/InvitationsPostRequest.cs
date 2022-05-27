using System;
namespace Liron_api.Models
{
public class InvitationsPostRequest
	{
		public string From { get; set; }
		public string To { get; set; }
		public string Server { get; set; }
		public InvitationsPostRequest(string from, string to, string server)
		{
			this.From = from;
			this.To = to;
			this.Server = server;
		}
	}
}
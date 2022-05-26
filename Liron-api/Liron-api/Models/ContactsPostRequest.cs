using System;
namespace Liron_api.Models
{
public class ContactsPostRequest
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Server { get; set; }
		public ContactsPostRequest(string id, string name, string server)
		{
			this.Id = id;
			this.Name = name;
			this.Server = server;
		}

	}
}


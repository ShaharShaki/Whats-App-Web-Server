using System;
namespace Liron_api.Models
{
public class ContactsPutRequest
	{
		public string Name { get; set; }
		public string Server { get; set; }
		public ContactsPutRequest( string name, string server)
		{
			this.Name = name;
			this.Server = server;
		}
	}
}


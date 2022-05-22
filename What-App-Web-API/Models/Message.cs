
using System;
namespace Liron_api.Models
{
public class Message
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public string Created { get; set; }
		public bool Sent { get; set; }

		public Message(int id, string content, string created, bool sent)
        {
			this.Id = id;
			this.Content = content;
			this.Created = created;
			this.Sent = sent;
        }
	}
}
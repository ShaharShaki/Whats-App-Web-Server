using System;
namespace Liron_api.Models
{
public class MessagePostRequest
	{
		public string Content { get; set; }
		public MessagePostRequest(string content)
		{
			this.Content = content;
		}
	}
}
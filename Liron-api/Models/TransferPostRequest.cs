using System;
namespace Liron_api.Models
{
public class TransferPostRequest
	{
		public string From { get; set; }
		public string To { get; set; }
		public string Content { get; set; }
		public TransferPostRequest(string from, string to, string content)
		{
			this.From = from;
			this.To = to;
			this.Content = content;
		}
	}
}
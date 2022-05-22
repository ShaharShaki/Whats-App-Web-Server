
using System;
namespace Liron_api.Models
{
	public class Response
	{
		public bool Error { get; set; }
		public string Message { get; set; }

		public Response(bool error, string message) 
		{
			this.Error = error;
			this.Message = message;
		}
	}
}
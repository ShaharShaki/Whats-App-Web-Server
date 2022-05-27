using System;
namespace Liron_api.Models
{
public class Conversation
	{
		public List<Message> Messages { get; set; }
        public string Last { get; set; }
        public User User1;
        public User User2;
		public int MessageId;
		public Conversation(User user1, User user2)
        {
            this.Messages = new List<Message>();
            this.MessageId = 1;
            this.User1 = user1;
            this.User2 = user2;
            this.Last = "";
        }

        public void CreateMessage (string content, string created, bool sent)
        {
			Message newMes = new Message(MessageId, content, created, sent);
            Messages.Add(newMes);
            this.Last = content;
			MessageId++;
        }

        public Message GetMessage(int id)
        {
            return this.Messages.Find(message => message.Id == id);
        }

        public void DeleteMessage(int id)
        {
            Message message = this.GetMessage(id);
            if (message != null)
                this.Messages.Remove(message);
        }
    }
}
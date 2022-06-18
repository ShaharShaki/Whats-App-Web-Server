using System;
namespace Liron_api.Models;

using System.ComponentModel.DataAnnotations;

public class Conversation
	{
        [Key]
        public int MessageId { get; set; }
        public List<Message> Messages { get; set; }
        public string Last { get; set; }
        public User[] Users { get; set; }
        // public User User1 { get; set; }
        // public User User2 { get; set; }
     
        public Conversation(User user1, User user2)
        {
            this.Messages = new List<Message>();
            this.MessageId = 1;
            this.Users = new User[2];
            this.Users[1] = user1;
            this.Users[2] = user2;
            this.Last = "";
        }

        public Conversation()
        {
            this.Messages = new List<Message>();
            this.MessageId = 1;
            this.Users = new User[2];
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

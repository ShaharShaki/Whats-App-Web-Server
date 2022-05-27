
using System;
namespace Liron_api.Models
{
public class User
{
		public string Name { get; set; }
		public string Password { get; set; }
		public string Server { get; set; }
		public string Lastdate { get; set; }
		public string Id { get; set; }
		public List<User> Contacts { get; set; }
		public List<Conversation> Conversations { get; set; }
		public User(string id, string name, string server, string lastdate, string password)
		{
			this.Id = id;
			this.Name = name;
			this.Server = server;
			this.Lastdate = lastdate;
			this.Contacts = new List<User>();
			this.Password = password;
			this.Conversations = new List<Conversation>();
		}

		public List<User> GetContacts()
		{
			return this.Contacts;
		}

		public void AddContact(User newUser)
		{
			this.Contacts.Add(newUser);
		}

		public void RemoveContact(User removeUser)
		{
			this.Contacts.Remove(removeUser);
		}

		public void AddConversation(Conversation conversation)
        {
			this.Conversations.Add(conversation);
        }

		public bool CreateNewConversation(User newUser)
		{
			//  if the user doesnt exist.
			User u = this.Contacts.Find(user => user.Id == newUser.Id);
			if (u == null)
            {
				this.AddContact(newUser);
				Conversation newConversation = new Conversation(this, newUser);
				this.AddConversation(newConversation);
				return true;
			}
			return false;
		}


		public Conversation GetConversationWith(User user)
		{
			Conversation conversation =  this.Conversations.Find(conversation => conversation.User1.Id == user.Id || conversation.User2.Id == user.Id);
			return conversation;
/*			if (conversation == null)
            {
				conversation =  new Conversation(this, user);
				this.AddConversation(conversation);
				user.AddConversation(conversation);
				return Conversation;
            }*/
		}

	}
}


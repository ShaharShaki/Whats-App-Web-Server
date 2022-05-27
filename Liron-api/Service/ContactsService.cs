using Liron_api.Models;
using Microsoft.AspNetCore.Http;

namespace Liron_api.Service
{
    public class ContactsService:IService
    {
        private static List<User> Contacts = new List<User>();
        public ContactsService()
        {
            /*User(string id, string name, string server, string lastdate, string password)*/

            User liron = new User("Liron", "liron", "localhost:7061", "17:00", "1111");
            User erel = new User("Erel", "erel", "localhost:7061", "17:10", "2222");
            liron.AddContact(erel);
            erel.AddContact(liron);

            Conversation lironConversation = new Conversation(liron, erel);
            lironConversation.CreateMessage("hi liron", "17:00", false);
            lironConversation.CreateMessage("hi erel", "17:01", true);

            Conversation omerConversation = new Conversation(liron, erel);
            omerConversation.CreateMessage("hi liron", "17:00", true);
            omerConversation.CreateMessage("hi erel", "17:01", false);

            liron.AddConversation(lironConversation);
            erel.AddConversation(omerConversation);

            Contacts.Add(liron);
            Contacts.Add(erel);
        }

        public void AddNewUser(User user)
        {
            Contacts.Add(user);
        }

        public List<User> GetAllContacts(string username)
        {
            User u = GetContact(username);
            if (u == null)
            {
                return null;
            }

            return u.Contacts;
        }

        public List<User> GetServerUsers()
        {
            return Contacts;
        }


        public List<APIUser> GetAllContactsAPI(string username)
            {
                User currentUser = GetContact(username);
                if (currentUser == null)
                {
                    return null;
                }

                List<APIUser> APIContacts = new List<APIUser>();
                for (int i = 0; i < currentUser.Contacts.Count; i++)
                {
                    User contact = currentUser.Contacts[i];
                    APIContacts.Add(new APIUser(contact.Id, contact.Name, contact.Server, currentUser.GetConversationWith(contact).Last, contact.Lastdate));
                }

                return APIContacts;
            }

        // from all the DB
        public User GetContact(string id)
        {
            return Contacts.Find(user=>user.Id == id);
        }

        public APIUser GetContactAPI(string id)
        {
            User currentUser = GetContact(id);
            return new APIUser(currentUser.Id, currentUser.Name, currentUser.Server, "sdsdsd", currentUser.Lastdate);
        }


    }
}

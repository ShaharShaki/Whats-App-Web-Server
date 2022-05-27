using Liron_api.Models;
using Microsoft.AspNetCore.Http;
using System.Globalization;


namespace Liron_api.Service
{
    public class ContactsService:IService
    {
        private static List<User> Contacts = new List<User>();
        public ContactsService()
        {
           
            /*User(string id, string name, string server, string lastdate, string password)*/

            User liron = new User("Liron", "liron", "localhost:7061", DateTime.Now.ToString("T", DateTimeFormatInfo.InvariantInfo), "1111");
            User erel = new User("Erel", "erel", "localhost:7061", DateTime.Now.ToString("T", DateTimeFormatInfo.InvariantInfo), "2222");
            liron.AddContact(erel);
            erel.AddContact(liron);

            Conversation lironConversation = new Conversation(liron, erel);
            lironConversation.CreateMessage("hi liron", DateTime.Now.ToString("T", DateTimeFormatInfo.InvariantInfo), false);
            lironConversation.CreateMessage("hi erel", DateTime.Now.ToString("T", DateTimeFormatInfo.InvariantInfo), true);

            Conversation omerConversation = new Conversation(liron, erel);
            omerConversation.CreateMessage("hi liron", DateTime.Now.ToString("T", DateTimeFormatInfo.InvariantInfo), true);
            omerConversation.CreateMessage("hi erel", DateTime.Now.ToString("T", DateTimeFormatInfo.InvariantInfo), false);

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

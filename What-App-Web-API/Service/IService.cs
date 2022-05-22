using Liron_api.Models;

namespace Liron_api.Service
{
    public interface IService
    {
       /* public List<User> GetAllContacts(string username);*/
        public User GetContact(string username);
        public List<User> GetAllContacts(string username);
        public List<APIUser> GetAllContactsAPI(string username);
        public APIUser GetContactAPI(string id);
        public void AddNewUser(User user);
    }
}

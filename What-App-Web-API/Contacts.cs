namespace Whats_App_Web_Server.Models
{
    public class Contacts
    {

        public int Id { get; set; }

        public string name { get; set; }

        public string last_seen { get; set; }

        public string pic { get; set; }

        public List<Messages> messages { get; set; }

    }
}

#nullable disable
using Microsoft.AspNetCore.Mvc;
using chatAPI;

namespace Whats_App_Web_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        // GET: Users

        private static List<User> _users = new List<User>() { new User() { Id = 1, Username = "erel", Password = "1111", Nickname = "Erel" },
                                                              new User() { Id = 2, Username = "eHrel", Password = "111FG1", Nickname = "ErGFGel" } };
        [HttpGet]
        public IEnumerable<User> Index()
        {
            return _users;
        }

        
        /*
        [HttpGet("{id")]
        public User Details(int? id)
        {
            return _users.Where(x => x.Id == id).FirstOrDefault();
        }
       */
       
     
        [HttpPost]
        public void Create([Bind("Id,Username,Password,Nickname")] User user)
        {
            _users.Add(user);
        }
       
        
    }
}
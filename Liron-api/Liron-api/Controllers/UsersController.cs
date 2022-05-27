using Liron_api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Globalization;
using Liron_api.Models;

namespace WhatsAppAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        DateTime _dt = DateTime.Now;
        public IConfiguration _configuration;
        public static IService _service = new ContactsService();

        public UsersController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpGet("/contacts")]
        public IActionResult GetUserContacts(string id)
        {
            List<APIUser> Contacts = _service.GetAllContactsAPI(id);
            if (Contacts == null)
            {
                return NotFound();
            }

            return Ok(Contacts);
        }

        [HttpPost("/contacts")]
        public IActionResult AddContact(string currentId, [Bind("id,name,server")] ContactsPostRequest newContact)
        {
            User currentUser = _service.GetContact(currentId);
            if (currentUser == null)
            {
                return NotFound();
            }

            string now = _dt.ToString("T", DateTimeFormatInfo.InvariantInfo);
            User newUser = new User(newContact.Id, newContact.Name, newContact.Server, now, "3333");
            bool createSuccess = currentUser.CreateNewConversation(newUser);
            if (createSuccess)
                return StatusCode(201);
            return NoContent();
        }


        [HttpPost("/invitations")]
        public IActionResult Invitations(string fromName, [Bind("id,name,server")] InvitationsPostRequest invitation)
        {
            User currentUser = _service.GetContact(invitation.To);
            if (currentUser == null)
            {
                return NotFound();
            }

            string now = _dt.ToString("T", DateTimeFormatInfo.InvariantInfo);
            User newUser = new User(invitation.From, fromName, invitation.Server, now, "3333");
            bool createSuccess = currentUser.CreateNewConversation(newUser);
            if (createSuccess)
                return StatusCode(201);
            return NoContent();
        }



        [HttpGet("/contacts/{id}")]
        public IActionResult GetUserInfo(string id, string currentId)
        {

            User currentUser = _service.GetContact(currentId);
            if (currentUser == null)
            {
                return NotFound();
            }
            List<APIUser> contacts = _service.GetAllContactsAPI(currentId);

            APIUser user = contacts.Find(user => user.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPut("/contacts/{id}")]
        public IActionResult UpdateUserInfo(string id, string currentId, [Bind("name,server")] ContactsPutRequest updateContact)
        {
            User currentUser = _service.GetContact(currentId);
            if (currentUser == null)
            {
                return NotFound();
            }
            List<User> contacts = _service.GetAllContacts(currentId);

            User user = contacts.Find(user => user.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = updateContact.Name;
            user.Server = updateContact.Server;
            return NoContent();
        }



        [HttpDelete("/contacts/{id}")]
        public IActionResult DeleteUser(string id, string currentId)
        {
            User currentUser = _service.GetContact(currentId);
            if (currentUser == null)
            {
                return NotFound();
            }
            List<User> contacts = _service.GetAllContacts(currentId);

            User userToDelete = contacts.Find(user => user.Id == id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            currentUser.RemoveContact(userToDelete);

            return NoContent();
        }


        [HttpGet("/contacts/{id}/messages")]
        public IActionResult GetUserMessages(string id, string currentId)
        {
            User currentUser = _service.GetContact(currentId);
            if (currentUser == null)
            {
                return NotFound();
            }

            User otherUser = _service.GetContact(id);
            if (otherUser == null)
            {
                return NotFound();
            }

            if (currentUser.GetConversationWith(otherUser) == null)
                return NotFound();

            List<Message> conversation = currentUser.GetConversationWith(otherUser).Messages;
            return Ok(conversation);
        }




        [HttpPost("/contacts/{id}/messages")]
        public IActionResult CreateMessages(string id, string currentId, bool amIsent, [Bind("content")] MessagePostRequest message)
        {
            User currentUser = _service.GetContact(currentId);
            if (currentUser == null)
            {
                return NotFound();
            }

            User otherUser = _service.GetContact(id);
            if (otherUser == null)
            {
                return NotFound();
            }

            if (currentUser.GetConversationWith(otherUser) == null)
                return NotFound();

            string now = _dt.ToString("T", DateTimeFormatInfo.InvariantInfo);
            currentUser.GetConversationWith(otherUser).CreateMessage(message.Content, now, amIsent);
            return StatusCode(201);
        }



        [HttpPost("/transfer")]
        public IActionResult TransferMessage(bool amIsent, [Bind("from,to,content")] TransferPostRequest message)
        {
            User currentUser = _service.GetContact(message.To);
            if (currentUser == null)
            {
                return NotFound();
            }

            User otherUser = _service.GetContact(message.From);
            if (otherUser == null)
            {
                return NotFound();
            }

            if (currentUser.GetConversationWith(otherUser) == null)
                return NotFound();

            string now = _dt.ToString("T", DateTimeFormatInfo.InvariantInfo);
            currentUser.GetConversationWith(otherUser).CreateMessage(message.Content, now, amIsent);
            return StatusCode(201);
        }



        [HttpGet("/contacts/{id}/messages/{messageId}")]
        public IActionResult GetMessage(string id, int messageId, string currentId)
        {
            User currentUser = _service.GetContact(currentId);
            if (currentUser == null)
            {
                return NotFound();
            }

            User otherUser = _service.GetContact(id);
            if (otherUser == null)
            {
                return NotFound();
            }

            if (currentUser.GetConversationWith(otherUser) == null)
                return NotFound();

            Conversation conversation = currentUser.GetConversationWith(otherUser);
            Message message = conversation.GetMessage(messageId);
            if (message == null)
                return NotFound();
            return Ok(message);
        }



        [HttpDelete("/contacts/{id}/messages/{messageId}")]
        public IActionResult DeleteMessage(string id, int messageId, string currentId)
        {
            User currentUser = _service.GetContact(currentId);
            if (currentUser == null)
            {
                return NotFound();
            }

            User otherUser = _service.GetContact(id);
            if (otherUser == null)
            {
                return NotFound();
            }

            if (currentUser.GetConversationWith(otherUser) == null)
                return NotFound();

            currentUser.GetConversationWith(otherUser).DeleteMessage(messageId);
            return NoContent();
        }

        [HttpPut("/contacts/{id}/messages/{messageId}")]
        public IActionResult UpdateMessage(string id, int messageId, string currentId, [Bind("content")] MessagePostRequest message)
        {
            User currentUser = _service.GetContact(currentId);
            if (currentUser == null)
                return NotFound();
            

            User otherUser = _service.GetContact(id);
            if (otherUser == null)
                return NotFound();
            
            // no conversation
            if (currentUser.GetConversationWith(otherUser) == null)
                return NotFound();

            // no message
            if (currentUser.GetConversationWith(otherUser).GetMessage(messageId) == null)
                return NotFound();

            currentUser.GetConversationWith(otherUser).GetMessage(messageId).Content = message.Content;
            return NoContent();
        }




        [HttpPost("/register/{id}/{name}/{password}")]
        public IActionResult Register(string id, string name, string password)
        {
            // user is already exists
            if (_service.GetContact(id) != null)
            {
                return NotFound();
            }

            string now = _dt.ToString("T", DateTimeFormatInfo.InvariantInfo);
            _service.AddNewUser(new User(id, name, "localhost:7061", now, password));
            return Ok();
        }


        [HttpPost("/login/{id}/{password}")]
        public IActionResult Login(string id,string password)
        {
            User currentUser = _service.GetContact(id);

            // user dosen't exist.
            if (currentUser == null)
                return NotFound();

            // wrong password.
            if (currentUser.Password != password)
                return NoContent();

            // correct password
            return Ok();
        }

        [HttpPost("/serverdb")]
        public IActionResult GetServerUsers()
        {
            return Ok(_service.GetServerUsers());
        }


        /*        [HttpPost]
                public IActionResult Post(string username, string password)
                {
                    // add user validation user
                    if (true)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWTParams:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim("UserId", username)
                        };

                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:SecretKey"]));
                        var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["JWTParams:Issuer"],
                            _configuration["JWTParams:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(20),
                            signingCredentials: mac);
                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                }*/
    }
}
using Liron_api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Liron_api.Models;

namespace WhatsAppAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
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

            User newUser = new User(newContact.Id, newContact.Name, newContact.Server, "17:20", "3333");
            currentUser.CreateNewConversation(newUser);
            return StatusCode(201);
        }


        [HttpPost("/invitations")]
        public IActionResult Invitations(string fromName, [Bind("id,name,server")] InvitationsPostRequest invitation)
        {
            User currentUser = _service.GetContact(invitation.To);
            if (currentUser == null)
            {
                return NotFound();
            }

            User newUser = new User(invitation.From, fromName, invitation.Server, "17:20", "3333");
            currentUser.CreateNewConversation(newUser);
            return StatusCode(201);
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

            currentUser.GetConversationWith(otherUser).CreateMessage(message.Content, "19:00", amIsent);
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

            Conversation conversation = currentUser.GetConversationWith(otherUser);
            Message message = conversation.GetMessage(messageId);
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

            currentUser.GetConversationWith(otherUser).DeleteMessage(messageId);
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

            _service.AddNewUser(new User(id, name, "localhost:7061", "17:03", password));
            return Ok();
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
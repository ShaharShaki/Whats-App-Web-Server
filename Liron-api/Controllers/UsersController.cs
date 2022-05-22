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
        public IService _service;

        public UsersController(IConfiguration config)
        {
            _configuration = config;
            _service = new ContactsService();
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


        [HttpGet("/contacts/{id}")]
        public IActionResult GetUserInfo(string id, string currentId)
        {

            User currentUser = _service.GetContact(currentId);
            if (currentUser == null)
            {
                return NotFound();
            }
            List<APIUser> Contacts = _service.GetAllContactsAPI(currentId);

            APIUser user = Contacts.Find(user => user.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
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



        [HttpPost("/register/{id}/{name}/{password}")]
        public IActionResult Register(string id, string name, string password)
        {
            // user is already exists
            if (_service.GetContact(id) != null)
            {
                return Ok(new Response(true, "Username already exists."));
            }

            var claims = new[]
               {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWTParams:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("username", id)
                };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:SecretKey"]));
            var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JWTParams:Issuer"],
                _configuration["JWTParams:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: mac);
            _service.AddNewUser(new User(id, name, "localhost:7061", "17:03", password));
            return Ok(new Response(false, new JwtSecurityTokenHandler().WriteToken(token)));
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
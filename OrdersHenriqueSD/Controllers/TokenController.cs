using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrdersHenriqueSD.Models;
using OrdersHenriqueSD.Repositories;

namespace OrdersHenriqueSD.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private readonly IUserPortalRepository _userPortalRepository;

        public TokenController(IConfiguration config, IUserPortalRepository userPortalRepository)
        {
            _config = config;
            _userPortalRepository = userPortalRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public IActionResult CreateToken([FromBody]UserPortal login)
        {
            try
            {
                IActionResult response = Unauthorized();

                if (login == null)
                    return StatusCode((int)HttpStatusCode.BadRequest, "To SignUp, please inform the user name and the password.");
                if (login.UserName == null || login.UserName == "")
                    return StatusCode((int)HttpStatusCode.BadRequest, "To SignUp, please inform the user name.");
                if (login.Password == null || login.Password == "")
                    return StatusCode((int)HttpStatusCode.BadRequest, "To SignUp, please inform the password.");

                var user =  Authenticate(login.UserName, login.Password);

                if (user != null)
                {
                    var tokenString = BuildToken(user);
                    response = Ok(new { token = tokenString });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, "The user was not found. Please verify if you have inform the right username and password.");
                }

                return response;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private string BuildToken(UserPortal user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserPortal Authenticate(string userName, string password)
        {
            var user =  _userPortalRepository.Login(userName, password);
            return user;
        }
    }
}
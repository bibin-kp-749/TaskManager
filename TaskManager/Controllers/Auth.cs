using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Model;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    //Controller for manage User Functionalities

    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private IConfiguration configuration;
        private readonly IUserService userService;

        //Dependency Injected using constructor

        public Auth(IConfiguration configuration,IUserService user)
        {
            this.configuration = configuration;
            this.userService = user;
        }

        //Method for Register New User

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            if (user == null)
                return BadRequest();
            return Ok(userService.RegisterUser(user));
        }

        //Method for Login  

        [HttpPost("Login")]
        public IActionResult Login(Login user)
        {
            var ValidUser=userService.Login(user);

            if (user != null)
            {
                if (ValidUser != null)
                {

                    //Generating the Jwt Token if the User is valid 

                    var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                    var signingCredentials = new SigningCredentials(
                                            new SymmetricSecurityKey(key),
                                            SecurityAlgorithms.HmacSha512Signature
                                        );
                    var subject = new ClaimsIdentity(new[]
                                {
                              new Claim(ClaimTypes.NameIdentifier, ValidUser.UserName),
                              new Claim(ClaimTypes.Role,ValidUser.Role),
                });
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = subject,
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        SigningCredentials = signingCredentials
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    //return the Generated token  
                    return Ok(jwtToken);
                }
            }
            //if the user is not valid  
            return BadRequest();
        }
    }
}

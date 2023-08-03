using Microsoft.AspNetCore.Mvc;
using DotnetToken.Models;

namespace DotnetToken.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        public AuthController(IConfiguration configuration)
        {
            this.configuration=configuration;
        }
        [HttpGet]
        public ActionResult<User> GetUser()
        {
            return Ok(user);
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            string PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user = new User
            {
                UserName = request.UserName,
                PasswordHash = PasswordHash
            };

            return Ok(user);
        }


        [HttpPost("login")]
        public ActionResult<User>login(UserDto request)
        {
            if(user.UserName!=request.UserName){
                return BadRequest("User not found.");
            }
            if(!BCrypt.Net.BCrypt.Verify(request.Password,user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }            
            return Ok(user);
        }

        private string CreateToken(User user)
        {
            List<Claim>claims=new List<Claim>{
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var key=new SymmetricSecuritykey(Encoding.UTF8.getBytes(
                _configuration.GetSection("AppSetings:Token").Value!));
                var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
        }
    }
}

namespace controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase{
       public stataic User user=new User();
        [HttpPost("register")]
        public ActionResult<user>Register(UserDto request)
        {
            string passwordHash=BCrypt.Net.BCrypt.HAshPassword(request.Password);
            
        }
    }
}
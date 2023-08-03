namespace DotnetToken.Models
{
    public class User
    {
        public string UserName {get;init;}=string.Empty;
        public string PasswordHash{get;init;}=string.Empty;
    }
}
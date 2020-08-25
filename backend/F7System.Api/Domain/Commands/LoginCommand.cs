namespace F7System.Api.Domain.Commands
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; } 
    }
    
    public class UserCommand: LoginModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
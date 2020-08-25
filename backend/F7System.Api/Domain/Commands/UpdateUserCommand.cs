namespace F7System.Api.Domain.Commands
{
    public class UpdateUserCommand
    {
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; } 
    }
}
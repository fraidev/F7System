using System;

namespace F7System.Api.Domain.Models
{
    public class UserPerson
    {
        public UserPerson()
        {
            UserPersonId = Guid.NewGuid();
        }
        
        public Guid UserPersonId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
    }
}
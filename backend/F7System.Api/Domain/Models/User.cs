using System;

namespace F7System.Api.Domain.Models
{
    public class User
    {
        public User()
        {
            UserId = Guid.NewGuid();
        }
        
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
    }
}
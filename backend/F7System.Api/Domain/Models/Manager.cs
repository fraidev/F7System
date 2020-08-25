using System;

namespace F7System.Api.Domain.Models
{
    public class Manager
    {
        public Manager()
        {
            ManagerId = Guid.NewGuid();
        }
        public Guid ManagerId { get; set; }
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
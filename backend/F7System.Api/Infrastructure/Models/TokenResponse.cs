using System;

namespace F7System.Api.Infrastructure.Models
{
    public class TokenResponse
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Token { get; set; }
    }
}
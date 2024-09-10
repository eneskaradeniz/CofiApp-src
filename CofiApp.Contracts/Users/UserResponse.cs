﻿namespace CofiApp.Contracts.Users
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}

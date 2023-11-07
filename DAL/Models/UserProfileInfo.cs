﻿namespace DAL.Infrastructure.Models
{
    public class UserProfileInfo
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public bool ShowConfidentialInformation { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }
    }
}
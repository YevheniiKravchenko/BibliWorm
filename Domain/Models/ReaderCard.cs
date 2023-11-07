using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class ReaderCard
    {
        public int UserId { get; set; }

        [Required]
        [MinLength(ValidationConstant.NameMinLength)]
        [MaxLength(ValidationConstant.NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(ValidationConstant.NameMinLength)]
        [MaxLength(ValidationConstant.NameMaxLength)] 
        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set; }

        [Required]
        [MinLength(ValidationConstant.AddressMinLength)]
        [MaxLength(ValidationConstant.AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [RegularExpression(RegularExpressions.PhoneNumber,
            ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MinLength(ValidationConstant.EmailMinLength)]
        [EmailAddress]
        public string Email { get; set; }

        #region Relations

        [JsonIgnore]
        public User User { get; set; }

        #endregion
    }
}
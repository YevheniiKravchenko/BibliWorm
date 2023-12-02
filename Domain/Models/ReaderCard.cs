using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class ReaderCard
    {
        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        [MinLength(ValidationConstant.NameMinLength, ErrorMessage = "MIN_LENGTH_ERROR")]
        [MaxLength(ValidationConstant.NameMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        [MinLength(ValidationConstant.NameMinLength, ErrorMessage = "MIN_LENGTH_ERROR")]
        [MaxLength(ValidationConstant.NameMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")] 
        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        [MinLength(ValidationConstant.AddressMinLength, ErrorMessage = "MIN_LENGTH_ERROR")]
        [MaxLength(ValidationConstant.AddressMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
        public string Address { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        [RegularExpression(RegularExpressions.PhoneNumber,
            ErrorMessage = "INVALID_PHONE_NUMBER_ERROR")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        [MinLength(ValidationConstant.EmailMinLength, ErrorMessage = "MIN_LENGTH_ERROR")]
        [EmailAddress(ErrorMessage = "INVALID_EMAIL_ADDRESS_ERROR")]
        public string Email { get; set; }

        #region Relations

        [JsonIgnore]
        public User User { get; set; }

        #endregion
    }
}
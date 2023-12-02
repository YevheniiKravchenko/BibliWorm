using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace DAL.Infrastructure.Models
{
    public class RegisterUserModel
    {
        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        [MinLength(ValidationConstant.LoginMinLength)]
        [MaxLength(ValidationConstant.LoginMaxLength)]
        [RegularExpression(RegularExpressions.Login,
            ErrorMessage = "INVALID_LOGIN_ERROR")]
        public string Login { get; set; }

        [Required]
        [MinLength(ValidationConstant.PasswordMinLength)]
        [MaxLength(ValidationConstant.PasswordMaxLength)]
        [RegularExpression(RegularExpressions.Password,
            ErrorMessage = "INVALID_PASSWORD_ERROR")]
        public string Password { get; set; }

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
    }
}
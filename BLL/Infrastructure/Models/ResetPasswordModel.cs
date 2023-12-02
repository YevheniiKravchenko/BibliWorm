using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        public string Token { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        [MinLength(ValidationConstant.PasswordMinLength, ErrorMessage = "MIN_LENGTH_ERROR")]
        [MaxLength(ValidationConstant.PasswordMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
        [RegularExpression(RegularExpressions.Password,
            ErrorMessage = "INVALID_PASSWORD_ERROR")]
        public string Password { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        [Compare("Password", ErrorMessage = "PASSWORD_MATCH_ERROR")]
        public string ConfirmPassword { get; set; }
    }
}
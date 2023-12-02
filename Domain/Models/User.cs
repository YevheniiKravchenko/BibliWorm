using Common.Constants;
using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class User
    {
        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        [MinLength(ValidationConstant.LoginMinLength, ErrorMessage = "MIN_LENGTH_ERROR")]
        [MaxLength(ValidationConstant.LoginMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
        [RegularExpression(RegularExpressions.Login,
            ErrorMessage = "INVALID_LOGIN_ERROR")]
        public string Login { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        public DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        public Role Role { get; set; }

        public bool IsBlocked { get; set; }

        #region Relations

        [JsonIgnore]
        public ReaderCard ReaderCard { get; set; }

        [JsonIgnore]
        public ICollection<Booking> Bookings { get; set; }

        [JsonIgnore]
        public ICollection<ReservationQueue> ReservationQueues { get; set; }

        [JsonIgnore]
        public ICollection<RefreshToken> RefreshTokens { get; set; }

        [JsonIgnore]
        public ICollection<ResetPasswordToken> ResetPasswordTokens { get; set; }

        [JsonIgnore]
        public ICollection<Book> FavouriteBooks { get; set; }

        [JsonIgnore]
        public ICollection<BookReview> BookReviews { get; set; }

        #endregion
    }
}
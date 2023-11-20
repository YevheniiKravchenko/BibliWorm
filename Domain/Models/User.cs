using Common.Constants;
using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MinLength(ValidationConstant.LoginMinLength)]
        [MaxLength(ValidationConstant.LoginMaxLength)]
        [RegularExpression(RegularExpressions.Login,
            ErrorMessage = "Login may contain only latin characters, numbers, hyphens and underscores")]
        public string Login { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

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
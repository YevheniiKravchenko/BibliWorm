using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class RefreshToken
    {
        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        public Guid RefreshTokenId { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        public DateTime ExpiresOnUtc { get; set; }

        #region Relations

        [JsonIgnore]
        public User User { get; set; }

        #endregion
    }
}

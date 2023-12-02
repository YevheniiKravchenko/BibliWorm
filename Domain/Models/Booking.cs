using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class Booking
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid BookingId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public DateTime BookedOnUtc { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    [Range(ValidationConstant.MustReturnInDaysMinValue, ValidationConstant.MustReturnInDaysMaxValue, ErrorMessage = "RANGE_ERROR_FROM")]
    public int MustReturnInDays { get; set; }

    public DateTime? ReturnedOnUtc { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid BookCopyId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int UserId { get; set; }

    #region Relations

    [JsonIgnore]
    public BookCopy BookCopy { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    #endregion
}

using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class BookCopy
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid BookCopyId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public string RFID { get; set; }

    [MaxLength(ValidationConstant.BookCopyConditionMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
    public string Condition { get; set; }

    public bool IsAvailable { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid BookId { get; set; }

    #region Relations

    [JsonIgnore]
    public Book Book { get; set; }

    [JsonIgnore]
    public ICollection<Booking> Bookings { get; set; }

    #endregion
}

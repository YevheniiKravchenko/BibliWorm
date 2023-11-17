using System.Text.Json.Serialization;

namespace Domain.Models;
public class Booking
{
    public Guid BookingId { get; set; }

    public DateTime BookedOnUtc { get; set; }

    public int MustReturnInDays { get; set; }

    public DateTime? ReturnedOnUtc { get; set; }

    public Guid BookCopyId { get; set; }

    public int UserId { get; set; }

    #region Relations

    [JsonIgnore]
    public BookCopy BookCopy { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    #endregion
}

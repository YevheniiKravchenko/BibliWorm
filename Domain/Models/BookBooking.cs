using System.Text.Json.Serialization;

namespace Domain.Models;
public class BookBooking
{
    public Guid BookBookingId { get; set; }

    public Guid BookId { get; set; }

    public int UserId { get; set; }

    public DateTime BookedOnUtc { get; set; }

    public int MustReturnInDays { get; set; }

    public DateTime? ReturnedOnUtc { get; set; }

    #region Relations

    [JsonIgnore]
    public Book Book { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    #endregion
}

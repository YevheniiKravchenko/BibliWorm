using System.Text.Json.Serialization;

namespace Domain.Models;
public class BookCopy
{
    public Guid BookCopyId { get; set; }

    public string RFID { get; set; }

    public string Condition { get; set; }

    public bool IsAvailable { get; set; }

    public Guid BookId { get; set; }

    #region Relations

    [JsonIgnore]
    public Book Book { get; set; }

    [JsonIgnore]
    public ICollection<Booking> Bookings { get; set; }

    #endregion
}

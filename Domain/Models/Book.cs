using Common.Enums;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class Book
{
    public Guid BookId { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string ISBN { get; set; }

    public byte[] RFID { get; set; }

    public Genre Genre { get; set; }

    public DateTime? PublicationDate { get; set; }

    public string Publisher { get; set; }

    public string Description { get; set; }

    public byte[] CoverImage { get; set; }

    public bool IsAvailable { get; set; }

    public string Location { get; set; }

    public int TotalCopies { get; set; }

    public int CurrentAvailableCopies { get; set; }

    public Guid? ReservationQueueId { get; set; }

    public string KeyWords { get; set; }

    #region Relations

    [JsonIgnore]
    public ICollection<ReservationQueue> ReservationQueues { get; set; }

    [JsonIgnore]
    public ICollection<BookBooking> BookBookings { get; set; }

    #endregion
}

using System.Text.Json.Serialization;

namespace Domain.Models;
public class ReservationQueue
{
    public Guid ReservationQueueId { get; set; }

    public DateTime ReservationDate { get; set; }

    public Guid BookId { get; set; }

    public int UserId { get; set; }

    #region Relations

    [JsonIgnore]
    public Book Book { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    #endregion
}

namespace Domain.Models;
public class ReservationQueue
{
    public Guid ReservationQueueId { get; set; }

    public Guid BookId { get; set; }

    public int UserId { get; set; }

    public int QueuePosition { get; set; }

    public DateTime ReservationDate { get; set; }

    #region Relations

    public Book Book { get; set; }

    public User User { get; set; }

    #endregion
}

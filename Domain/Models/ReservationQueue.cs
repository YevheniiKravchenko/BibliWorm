using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class ReservationQueue
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid ReservationQueueId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public DateTime ReservationDate { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid BookId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int UserId { get; set; }

    #region Relations

    [JsonIgnore]
    public Book Book { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    #endregion
}

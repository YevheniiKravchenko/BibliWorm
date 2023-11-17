using System.Text.Json.Serialization;

namespace Domain.Models;
public class BookReview
{
    public Guid BookReviewId { get; set; }

    public double Mark { get; set; }

    public string Comment { get; set; }

    public Guid BookId { get; set; }

    public int UserId { get; set; }

    #region Relations

    [JsonIgnore]
    public Book Book { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    #endregion
}

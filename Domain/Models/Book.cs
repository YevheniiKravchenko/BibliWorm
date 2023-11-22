using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class Book
{
    public Guid BookId { get; set; }

    [Required]
    public string Title { get; set; }

    public string Author { get; set; }

    [Required]
    public string ISBN { get; set; }

    public DateTime? PublicationDate { get; set; }

    public string Publisher { get; set; }

    public string Description { get; set; }

    public int PagesAmount { get; set; }

    public byte[] CoverImage { get; set; }

    public string KeyWords { get; set; }

    public int? DepartmentId { get; set; }

    #region Relations

    [JsonIgnore]
    public ICollection<EnumItem> Genres { get; set; }

    [JsonIgnore]
    public ICollection<BookCopy> BookCopies { get; set; }

    [JsonIgnore]
    public ICollection<BookReview> BookReviews { get; set; }

    [JsonIgnore]
    public Department Department { get; set; }

    [JsonIgnore]
    public ICollection<ReservationQueue> ReservationQueues { get; set; }

    [JsonIgnore]
    public ICollection<User> Users { get; set; }

    #endregion
}

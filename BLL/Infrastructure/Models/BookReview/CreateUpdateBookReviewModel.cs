using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.BookReview;
public class CreateUpdateBookReviewModel
{
    public Guid BookReviewId { get; set; }

    [Required]
    public double Mark { get; set; }

    [Required]
    public string Comment { get; set; }

    [Required]
    public Guid BookId { get; set; }

    [Required]
    public int UserId { get; set; }
}

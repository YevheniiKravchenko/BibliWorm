namespace BLL.Infrastructure.Models.BookReview;
public class BookReviewModel
{
    public Guid BookReviewId { get; set; }

    public double Mark { get; set; }

    public string Comment { get; set; }

    public Guid BookId { get; set; }

    public int UserId { get; set; }

    public string AuthorName { get; set; }
}

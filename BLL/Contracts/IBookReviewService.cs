using BLL.Infrastructure.Models.BookReview;

namespace BLL.Contracts;
public interface IBookReviewService
{
    void AddBookReview(CreateUpdateBookReviewModel bookReviewModel);

    void UpdateBookReview(CreateUpdateBookReviewModel bookReviewModel);

    void DeleteBookReview(Guid bookReviewId);

    BookReviewModel GetBookReviewById(Guid bookReviewId);

    IEnumerable<BookReviewModel> GetReviewsForBook(Guid bookId);
}

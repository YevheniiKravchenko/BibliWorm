using Domain.Models;

namespace DAL.Contracts;
public interface IBookReviewRepository
{
    void Create(BookReview newBookReview);

    void Delete(Guid bookId);

    IQueryable<BookReview> GetAll();
    
    BookReview GetById(Guid reviewId);

    void Update(BookReview updatedBookReview);
}

using BLL.Infrastructure.Models.Book;
using DAL.Infrastructure.Models.Filters;

namespace BLL.Contracts;
public interface IBookService
{
    void AddBook(CreateUpdateBookModel bookModel);

    void UpdateBook(CreateUpdateBookModel bookModel);

    IEnumerable<BookListItemModel> BookList(BookFilter filter);

    void AddBookCopy(CreateUpdateBookCopyModel bookCopyModel);

    void UpdateBookCopy(CreateUpdateBookCopyModel bookCopyModel);

    void DeleteBook(Guid bookId);

    void DeleteBookCopy(Guid bookCopyId);

    void BookTheBookCopies(int userId, List<Guid> bookCopiesIds);

    void ReturnTheBookCopies(List<Guid> bookingsIds);

    IEnumerable<BookListItemModel> GetMostPopularBooks();

    IEnumerable<BookListItemModel> GetMostPopularBooksInGenre(int genreId);

    BookModel GetBookById(Guid bookId);

    BookCopyModel GetBookCopyById(Guid bookCopyId);

    IEnumerable<BookCopyListItemModel> GetBookCopies(Guid bookId);

    BookModel GetRandomBook();

    IEnumerable<BookListItemModel> GetRecomendationsForUser(int userId);

    BookCopyModel GetBookCopyByRFID();

    List<BookingListItemModel> GetUserBookings(int userId);
}

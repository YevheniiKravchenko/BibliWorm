using AutoMapper;
using BLL.Contracts;
using BLL.Infrastructure.Models.Book;
using DAL.Contracts;
using DAL.Infrastructure.Models.Filters;
using Domain.Models;

namespace BLL.Services;
public class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Lazy<IMapper> _mapper;

    public BookService(
        IUnitOfWork unitOfWork,
        Lazy<IMapper> mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public void AddBook(CreateUpdateBookModel bookModel)
    {
        var book = _mapper.Value.Map<Book>(bookModel);

        _unitOfWork.Books.Value.Create(book);
    }

    public void AddBookCopy(CreateUpdateBookCopyModel bookCopyModel)
    {
        var bookCopy = _mapper.Value.Map<BookCopy>(bookCopyModel);

        _unitOfWork.BookCopies.Value.Create(bookCopy);
    }

    public IEnumerable<BookListItemModel> BookList(BookFilter filter)
    {
        var books = _unitOfWork.Books.Value.GetAll(filter);
        var bookModels = _mapper.Value.Map<List<BookListItemModel>>(books);

        return bookModels;
    }

    public void BookTheBookCopies(int userId, List<Guid> bookCopiesIds)
    {
        var bookings = new List<Booking>();

        foreach (var bookCopyId in bookCopiesIds)
        {
            bookings.Add(new Booking
            {
                BookingId = Guid.Empty,
                BookedOnUtc = DateTime.UtcNow,
                MustReturnInDays = 5,
                ReturnedOnUtc = null,
                BookCopyId = bookCopyId,
                UserId = userId
            });
        }

        // TODO Remove reservations

        //var reservationQueues = _unitOfWork.ReservationQueues.Value.GetAll()
        //    .Where(rq => rq.UserId == userId )) 

        _unitOfWork.Bookings.Value.Create(bookings);
    }

    public void DeleteBook(Guid bookId)
    {
        _unitOfWork.Books.Value.Delete(bookId);
    }

    public void DeleteBookCopy(Guid bookCopyId)
    {
        _unitOfWork.BookCopies.Value.Delete(bookCopyId);
    }

    public BookModel GetBookById(Guid bookId)
    {
        var book = _unitOfWork.Books.Value.GetById(bookId);
        var bookModel = _mapper.Value.Map<BookModel>(book);

        return bookModel;
    }

    public IEnumerable<BookCopyListItemModel> GetBookCopies(Guid bookId)
    {
        var bookCopies = _unitOfWork.BookCopies.Value.GetAll()
            .Where(bc => bc.BookId == bookId);
        var bookCopiesModels = _mapper.Value.Map<List<BookCopyListItemModel>>(bookCopies);

        return bookCopiesModels;

    }

    public BookCopyModel GetBookCopyById(Guid bookCopyId)
    {
        var bookCopy = _unitOfWork.BookCopies.Value.GetById(bookCopyId);
        var bookCopyModel = _mapper.Value.Map<BookCopyModel>(bookCopy);

        return bookCopyModel;
    }

    public IEnumerable<BookListItemModel> GetMostPopularBooks()
    {
        var mostPopularBooks = _unitOfWork.Books.Value.GetAll()
            .OrderByDescending(b => b.BookCopies
                .Sum(bc => bc.Bookings.Count()))
            .Take(10);
        var mostPopulatBooksModels = _mapper.Value.Map<List<BookListItemModel>>(mostPopularBooks);

        return mostPopulatBooksModels;
    }

    public IEnumerable<BookListItemModel> GetMostPopularBooksInGenre(int genreId)
    {
        var booksInGenre = _unitOfWork.Books.Value.GetAll()
            .Where(b => b.Genres
                .Select(g => g.EnumItemId)
                .Contains(genreId));
        var mostPopularBooksInGenre = booksInGenre
            .OrderByDescending(b => b.BookCopies
                .Sum(bc => bc.Bookings.Count()))
            .Take(10);
        var mostPopulatBooksInGenreModels = _mapper.Value.Map<List<BookListItemModel>>(mostPopularBooksInGenre);

        return mostPopulatBooksInGenreModels;
    }

    public void ReturnTheBookCopies(List<Guid> bookingsIds)
    {
        var bookings = _unitOfWork.Bookings.Value.GetAll()
            .Where(b => bookingsIds.Contains(b.BookingId))
            .ToList();

        foreach (var booking in bookings)
            booking.ReturnedOnUtc = DateTime.UtcNow;

        _unitOfWork.Bookings.Value.Update(bookings);
    }

    public void UpdateBook(CreateUpdateBookModel bookModel)
    {
        var book = _mapper.Value.Map<Book>(bookModel);

        _unitOfWork.Books.Value.Update(book);
    }

    public void UpdateBookCopy(CreateUpdateBookCopyModel bookCopyModel)
    {
        var bookCopy = _mapper.Value.Map<BookCopy>(bookCopyModel);

        _unitOfWork.BookCopies.Value.Update(bookCopy);
    }

    public BookModel GetRanomBook()
    {
        var totalBooks = _unitOfWork.Books.Value.GetAll().Count();
        var randomBookNumber = Random.Shared.Next(1, totalBooks);

        var randomBook = _unitOfWork.Books.Value.GetAll()
            .Skip(randomBookNumber - 1)
            .Take(1)
            .FirstOrDefault();
        var randomBookModel = _mapper.Value.Map<BookModel>(randomBook);

        return randomBookModel;
    }

    public IEnumerable<BookListItemModel> GetRecomendationsForUser(int userId)
    {
        var alreadyReadBooksAmount = _unitOfWork.Bookings.Value.GetAll()
            .Where(b => b.UserId == userId)
            .Count();

        if (alreadyReadBooksAmount == 0)
            throw new ArgumentException("NOT_ENOUGH_DATA");

        var userFavouriteGenre = 1;

        var recomendedBooks = GetMostPopularBooksInGenre(userFavouriteGenre);

        return recomendedBooks;
    }
}

using AutoMapper;
using BLL.Contracts;
using BLL.Infrastructure.Enums;
using BLL.Infrastructure.Models.Book;
using BLL.Infrastructure.Models.ExternalDevices;
using Common.Configs;
using DAL.Contracts;
using DAL.Infrastructure.Extensions;
using DAL.Infrastructure.Models.Filters;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BLL.Services;
public class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Lazy<IMapper> _mapper;
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<RFIDReaderSettings> _rfidReaderSettings;

    public BookService(
        IUnitOfWork unitOfWork,
        Lazy<IMapper> mapper,
        Lazy<IUserService> userService,
        Lazy<RFIDReaderSettings> rfidReaderSettings)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userService = userService;
        _rfidReaderSettings = rfidReaderSettings;
    }

    public void AddBook(CreateUpdateBookModel bookModel)
    {
        var book = MapCreateUpdateBookModelToBook(bookModel);

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

        var reservationQueuesIds = _unitOfWork.ReservationQueues.Value.GetAll()
            .Where(rq => rq.UserId == userId && rq.Book.BookCopies
                .Any(bc => bookCopiesIds.Contains(bc.BookCopyId)))
            .Select(rq => rq.ReservationQueueId)
            .ToList();

        _unitOfWork.ReservationQueues.Value.Delete(reservationQueuesIds);
        _unitOfWork.Bookings.Value.Create(bookings);
        _unitOfWork.BookCopies.Value.SetBookCopiesAsUnavailable(bookCopiesIds);
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
       
        bookModel.Departments = GetDepartmentOptions();
        bookModel.Genres = GetGenreOptions();

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
            .GetTopTenMostPopularBooks();
        var mostPopulatBooksModels = _mapper.Value.Map<List<BookListItemModel>>(mostPopularBooks);

        return mostPopulatBooksModels;
    }

    public IEnumerable<BookListItemModel> GetMostPopularBooksInGenre(int genreId)
    {
        var booksInGenre = _unitOfWork.Books.Value.GetAll()
            .Where(b => b.Genres
                .Select(g => g.EnumItemId)
                .Contains(genreId));
        var mostPopularBooksInGenre = booksInGenre.GetTopTenMostPopularBooks();
        var mostPopulatBooksInGenreModels = _mapper.Value.Map<List<BookListItemModel>>(mostPopularBooksInGenre);

        return mostPopulatBooksInGenreModels;
    }

    public void ReturnTheBookCopies(List<Guid> bookingsIds)
    {
        var bookings = _unitOfWork.Bookings.Value.GetAll()
            .Include(b => b.BookCopy)
            .Where(b => bookingsIds.Contains(b.BookingId))
            .ToList();

        foreach (var booking in bookings)
        {
            booking.ReturnedOnUtc = DateTime.UtcNow;
            booking.BookCopy.IsAvailable = true;
        }

        _unitOfWork.Bookings.Value.Update(bookings);
    }

    public void UpdateBook(CreateUpdateBookModel bookModel)
    {
        var book = MapCreateUpdateBookModelToBook(bookModel);

        _unitOfWork.Books.Value.Update(book);
    }

    public void UpdateBookCopy(CreateUpdateBookCopyModel bookCopyModel)
    {
        var bookCopy = _mapper.Value.Map<BookCopy>(bookCopyModel);

        _unitOfWork.BookCopies.Value.Update(bookCopy);
    }

    public BookModel GetRandomBook()
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
            throw new ArgumentException("NO_DATA");

        var userFavouriteGenreId = _userService.Value.GetUserFavouriteGenre(userId).EnumItemId;

        var recomendedBooks = GetMostPopularBooksInGenre(userFavouriteGenreId);

        return recomendedBooks;
    }

    public BookCopyModel GetBookCopyByRFID()
    {
        var rfidTag = ReadRFID().Result;

        var bookCopy = _unitOfWork.BookCopies.Value.GetByRFID(rfidTag);
        var bookCopyModel = _mapper.Value.Map<BookCopyModel>(bookCopy);

        return bookCopyModel;
    }

    #region Helpers

    private Book MapCreateUpdateBookModelToBook(CreateUpdateBookModel bookModel)
    {
        var book = _mapper.Value.Map<Book>(bookModel);
        var bookGenres = _unitOfWork.EnumItems.Value.GetAll()
            .Where(ei => bookModel.Genres.Contains(ei.EnumItemId))
            .ToList();

        book.Genres = bookGenres;

        return book;
    }

    private Dictionary<int, string> GetDepartmentOptions()
    {
        var departments = _unitOfWork.Departments.Value.GetAll()
           .Select(d => new
           {
               d.DepartmentId,
               d.Name
           });
        var departmentDictionary = new Dictionary<int, string>();
        foreach (var department in departments)
            departmentDictionary.Add(department.DepartmentId, department.Name);

        return departmentDictionary;
    }

    private Dictionary<int, string> GetGenreOptions()
    {
        var genres = _unitOfWork.EnumItems.Value.GetAll()
            .Include(ei => ei.Enum)
            .Where(ei => ei.Enum.Code == "Genre")
            .Select(ei => new
            {
                ei.EnumItemId,
                ei.Value
            });
        var genreDictionary = new Dictionary<int, string>();
        foreach (var genre in genres)
            genreDictionary.Add(genre.EnumItemId, genre.Value);

        return genreDictionary;
    }

    private async Task<string> ReadRFID()
    {
        var ipAddress = _rfidReaderSettings.Value.IPAddress;
        if (string.IsNullOrEmpty(ipAddress))
            throw new ArgumentException("RFID_READER_IP_ADDRESS_NOT_SET");

        var rfidReaderURL = string.Format("https://{0}:433", ipAddress);
        var request = new RFIDReaderRequest
        {
            Command = Command.ReadRFID
        };

        var serializedRequest = JsonConvert.SerializeObject(request, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        using var clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback
            = (sender, cert, chain, sslPolicyErrors) => true;

        using var client = new HttpClient(clientHandler);
        var requestContent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
        var httpResponse = await client.PostAsync(rfidReaderURL, requestContent);

        var bytes = await httpResponse.Content.ReadAsByteArrayAsync();
        var serializedResponse = Encoding.UTF8.GetString(bytes);

        var response = JsonConvert.DeserializeObject<Response>(serializedResponse);

        return response.Body;
    }

    #endregion
}

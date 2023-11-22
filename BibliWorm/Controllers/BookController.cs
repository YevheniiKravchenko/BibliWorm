using BibliWorm.Infrastructure.Models;
using BLL.Contracts;
using BLL.Infrastructure.Models.Book;
using DAL.Infrastructure.Models.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliWorm.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost("add-book")]
    [Authorize(Roles = "Admin,Librarian")]
    public ActionResult AddBook([FromBody] CreateUpdateBookModel bookModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _bookService.AddBook(bookModel);

        return Ok();
    }

    [HttpPost("add-book-copy")]
    [Authorize(Roles = "Admin,Librarian")]
    public ActionResult AddBookCopy([FromBody] CreateUpdateBookCopyModel bookCopyModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _bookService.AddBookCopy(bookCopyModel);

        return Ok();
    }

    [HttpGet("book-list")]
    [Authorize]
    public ActionResult BookList([FromQuery] BookFilter filter)
    {
        var books = _bookService.BookList(filter);

        return Ok(books);
    }

    [HttpPost("book-the-book-copies")]
    [Authorize(Roles = "Admin,Librarian")]
    public ActionResult BookTheBookCopies([FromBody] BookTheBookCopiesModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _bookService.BookTheBookCopies(model.UserId, model.BookCopiesIds);

        return Ok();
    }

    [HttpDelete("delete-book")]
    [Authorize(Roles = "Admin,Librarian")]
    public ActionResult DeleteBook([FromQuery] Guid bookId)
    {
        _bookService.DeleteBook(bookId);

        return Ok();
    }

    [HttpDelete("delete-book-copy")]
    [Authorize(Roles = "Admin,Librarian")]
    public ActionResult DeleteBookCopy([FromQuery] Guid bookCopyId)
    {
        _bookService.DeleteBookCopy(bookCopyId);

        return Ok();
    }

    [HttpGet("get-book")]
    [Authorize]
    public ActionResult GetBookById([FromQuery] Guid bookId)
    {
        var book = _bookService.GetBookById(bookId);

        return Ok(book);
    }

    [HttpGet("get-book-copies")]
    [Authorize(Roles = "Admin,Librarian")]
    public ActionResult GetBookCopies([FromQuery] Guid bookId)
    {
        var bookCopies = _bookService.GetBookCopies(bookId);

        return Ok(bookCopies);
    }

    [HttpGet("get-book-copy")]
    [Authorize(Roles = "Admin,Librarian")]
    public ActionResult GetBookCopyById([FromQuery] Guid bookCopyId)
    {
        var bookCopy = _bookService.GetBookCopyById(bookCopyId);

        return Ok(bookCopy);
    }

    [HttpGet("get-most-popular-books")]
    [Authorize(Roles = "Admin,Librarian")]
    public ActionResult GetMostPopularBooks()
    {
        var mostPopularBooks = _bookService.GetMostPopularBooks();

        return Ok(mostPopularBooks);
    }

    [HttpGet("get-most-popular-books-in-genre")]
    [Authorize(Roles = "Admin,Librarian")]
    public ActionResult GetMostPopulatBooksInGenre([FromQuery] int genreId)
    {
        var mostPopularBooksInGenre = _bookService.GetMostPopularBooksInGenre(genreId);

        return Ok(mostPopularBooksInGenre);
    }

    [HttpPost("return-book-copies")]
    [Authorize(Roles = "Admin,Librarian")]
    public ActionResult ReturnBookCopies([FromBody] List<Guid> bookingsIds)
    {
        _bookService.ReturnTheBookCopies(bookingsIds);

        return Ok();
    }

    [HttpPost("update-book")]
    [Authorize(Roles = "Admin,Librarian")]
    public ActionResult UpdateBook([FromBody] CreateUpdateBookModel bookModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _bookService.UpdateBook(bookModel);

        return Ok();
    }

    [HttpPost("update-book-copy")]
    [Authorize(Roles = "Admin,Librarian")]
    public ActionResult UpdateBookCopy([FromBody] CreateUpdateBookCopyModel bookCopyModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _bookService.UpdateBookCopy(bookCopyModel);

        return Ok();
    }

    [HttpGet("get-random-book")]
    [Authorize]
    public ActionResult GetRandomBook()
    {
        var randomBook = _bookService.GetRanomBook();

        return Ok(randomBook);
    }

    [HttpGet("get-recomendations-for-user")]
    [Authorize]
    public ActionResult GetRecomendationsForUser([FromQuery] int userId)
    {
        var recomendations = _bookService.GetRecomendationsForUser(userId);

        return Ok(recomendations);
    }
}

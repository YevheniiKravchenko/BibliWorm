using BLL.Contracts;
using BLL.Infrastructure.Models.BookReview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliWorm.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BookReviewController : ControllerBase
{
    private readonly IBookReviewService _bookReviewService;

    public BookReviewController(IBookReviewService bookReviewService)
    {
        _bookReviewService = bookReviewService;
    }

    [HttpPost("add")]
    public ActionResult AddBookReview([FromBody] CreateUpdateBookReviewModel bookReviewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _bookReviewService.AddBookReview(bookReviewModel);

        return Ok();
    }

    [HttpDelete("delete")]
    public ActionResult DeleteBookReview([FromQuery] Guid bookReviewId) 
    {
        _bookReviewService.DeleteBookReview(bookReviewId);

        return Ok();
    }

    [HttpGet("get")]
    public ActionResult GetBookReviewById([FromQuery] Guid bookReviewId)
    {
        var bookReview = _bookReviewService.GetBookReviewById(bookReviewId);

        return Ok(bookReview);
    }

    [HttpGet("get-reviews-for-book")]
    public ActionResult GetReviewsForBook([FromQuery] Guid bookId)
    {
        var reviewsForBook = _bookReviewService.GetReviewsForBook(bookId);

        return Ok(reviewsForBook);
    }

    [HttpPost("update")]
    public ActionResult UpdateBookReview([FromBody] CreateUpdateBookReviewModel bookReviewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _bookReviewService.UpdateBookReview(bookReviewModel);

        return Ok();
    }
}

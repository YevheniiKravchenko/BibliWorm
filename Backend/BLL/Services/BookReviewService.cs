using AutoMapper;
using BLL.Contracts;
using BLL.Infrastructure.Models.BookReview;
using DAL.Contracts;
using Domain.Models;

namespace BLL.Services;
public class BookReviewService : IBookReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Lazy<IMapper> _mapper;

    public BookReviewService(
        IUnitOfWork unitOfWork,
        Lazy<IMapper> mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public void AddBookReview(CreateUpdateBookReviewModel bookReviewModel)
    {
        var bookReview = _mapper.Value.Map<BookReview>(bookReviewModel);
        
        _unitOfWork.BookReviews.Value.Create(bookReview);
    }

    public void DeleteBookReview(Guid bookReviewId)
    {
        _unitOfWork.BookReviews.Value.Delete(bookReviewId);
    }

    public BookReviewModel GetBookReviewById(Guid bookReviewId)
    {
        var bookReview = _unitOfWork.BookReviews.Value.GetById(bookReviewId);
        var bookReviewModel = _mapper.Value.Map<BookReviewModel>(bookReview);
        
        return bookReviewModel;
    }

    public IEnumerable<BookReviewModel> GetReviewsForBook(Guid bookId)
    {
        var reviewsForBook = _unitOfWork.BookReviews.Value.GetAll()
            .Where(b => b.BookId == bookId);
        var reviewsForBookModels = _mapper.Value.Map<List<BookReviewModel>>(reviewsForBook);

        return reviewsForBookModels;
    }

    public void UpdateBookReview(CreateUpdateBookReviewModel bookReviewModel)
    {
        var bookReview = _mapper.Value.Map<BookReview>(bookReviewModel);

        _unitOfWork.BookReviews.Value.Update(bookReview);
    }
}

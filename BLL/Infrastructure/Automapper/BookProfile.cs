using AutoMapper;
using BLL.Infrastructure.Models.BookReview;
using Domain.Models;

namespace BLL.Infrastructure.Automapper;
public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookReview, BookReviewModel>()
            .ReverseMap();
    }
}

using AutoMapper;
using BLL.Infrastructure.Models.Book;
using BLL.Infrastructure.Models.BookReview;
using Domain.Models;

namespace BLL.Infrastructure.Automapper;
public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookReview, CreateUpdateBookReviewModel>()
            .ReverseMap();

        CreateMap<BookReview, BookReviewModel>()
            .ForMember(dst => dst.AuthorName,
                opt => opt.MapFrom(src => src.User.ReaderCard.LastName + " " + src.User.ReaderCard.FirstName));

        CreateMap<Book, BookModel>();

        CreateMap<CreateUpdateBookModel, BookModel>();

        CreateMap<Book, BookListItemModel>()
            .ForMember
            (
                dst => dst.PublicationDate,
                opt => opt.MapFrom(src => src.PublicationDate == null
                    ? ""
                    : src.PublicationDate.Value.ToLongDateString())
            )
            .ForMember
            (
                dst => dst.DepartmentName,
                opt => opt.MapFrom(src => src.DepartmentId == null
                    ? ""
                    : src.Department.Name)
            );

        CreateMap<BookCopy, BookCopyModel>();

        CreateMap<CreateUpdateBookCopyModel, BookCopy>();

        CreateMap<BookCopy, BookCopyListItemModel>();

        CreateMap<Booking, BookingListItemModel>()
            .ForMember
            (   
                dst => dst.BookedOn,
                opt => opt.MapFrom(src => src.BookedOnUtc
                    .ToLocalTime()
                    .ToLongDateString())
            )
            .ForMember
            (
                dst => dst.MustReturnOn,
                opt => opt.MapFrom(src => src.BookedOnUtc
                    .AddDays(src.MustReturnInDays)
                    .ToLocalTime()
                    .ToLongDateString())
            )
            .ForMember
            (
                dst => dst.BookTitle,
                opt => opt.MapFrom(src => src.BookCopy.Book.Title)
            )
            .ForMember
            (
                dst => dst.IsReturned,
                opt => opt.MapFrom(src => src.ReturnedOnUtc != null)
            );
    }
}

using AutoMapper;
using BLL.Infrastructure.Models.Book;
using BLL.Infrastructure.Models.BookReview;
using Domain.Models;

namespace BLL.Infrastructure.Automapper;
public class BookProfile : Profile
{
    public BookProfile()
    {
        #region BookReview

        CreateMap<BookReview, CreateUpdateBookReviewModel>()
            .ReverseMap();

        CreateMap<BookReview, BookReviewModel>()
            .ForMember(dst => dst.AuthorName,
                opt => opt.MapFrom(src => src.User.ReaderCard.LastName + " " + src.User.ReaderCard.FirstName));

        #endregion

        #region Book

        CreateMap<Book, BookModel>()
            .ForMember(dst => dst.BookGenres,
                opt => opt.MapFrom(src => src.Genres.Select(g => g.EnumItemId)))
            .ForMember(dst => dst.Genres,
                opt => opt.Ignore());

        CreateMap<CreateUpdateBookModel, Book>()
            .ForMember(dst => dst.Genres,
                opt => opt.Ignore());

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

        #endregion

        #region BookCopy

        CreateMap<BookCopy, BookCopyModel>();

        CreateMap<CreateUpdateBookCopyModel, BookCopy>();

        CreateMap<BookCopy, BookCopyListItemModel>();

        #endregion

        #region Booking

        CreateMap<Booking, BookingListItemModel>()
            .ForMember
            (   
                dst => dst.BookedOn,
                opt => opt.MapFrom(src => src.BookedOnUtc
                    .ToLocalTime()
                    .ToShortDateString())
            )
            .ForMember
            (
                dst => dst.MustReturnOn,
                opt => opt.MapFrom(src => src.BookedOnUtc
                    .AddDays(src.MustReturnInDays)
                    .ToLocalTime()
                    .ToShortDateString())
            )
            .ForMember
            (
                dst => dst.ReturnedOn,
                opt => opt.MapFrom(src => src.ReturnedOnUtc.HasValue
                    ? src.ReturnedOnUtc.Value
                        .ToLocalTime()
                        .ToShortDateString()
                    : string.Empty)
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

        #endregion
    }
}

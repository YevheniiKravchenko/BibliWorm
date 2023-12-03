using AutoMapper;
using Domain.Models;

namespace DAL.Infrastructure.Automapper;
public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, Book>()
            .ForMember(dst => dst.BookCopies,
                opt => opt.Ignore())
            .ForMember(dst => dst.BookReviews,
                opt => opt.Ignore())
            .ForMember(dst => dst.Genres,
                opt => opt.Ignore())
            .ForMember(dst => dst.Department,
                opt => opt.Ignore())
            .ForMember(dst => dst.ReservationQueues,
                opt => opt.Ignore())
            .ForMember(dst => dst.Users,
                opt => opt.Ignore())
            .ForMember(dst => dst.CoverImage,
                opt => opt.Ignore());

        CreateMap<BookReview, BookReview>()
            .ForMember(dst => dst.Book,
                opt => opt.Ignore())
            .ForMember(dst => dst.User,
                opt => opt.Ignore());

        CreateMap<Department, Department>()
            .ForMember(dst => dst.DepartmentStatistics,
                opt => opt.Ignore())
            .ForMember(dst => dst.Books,
                opt => opt.Ignore());

        CreateMap<BookCopy, BookCopy>()
            .ForAllMembers(opts =>
            {
                opts.AllowNull();
                opts.Condition((src, dest, srcMember) => srcMember != null);
            });

        CreateMap<Booking, Booking>()
            .ForAllMembers(opts =>
            {
                opts.Condition((src, dest, srcMember) => srcMember != null);
            });

        CreateMap<ReservationQueue, ReservationQueue>()
            .ForAllMembers(opts =>
            {
                opts.Condition((src, dest, srcMember) => srcMember != null);
            });
    }
}

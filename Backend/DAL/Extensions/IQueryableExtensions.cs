using DAL.Infrastructure.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Infrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> GetPage<T>(this IOrderedQueryable<T> query, PagingModel model)
        {
            if (model is null)
                return query;

            return (IOrderedQueryable<T>)query
                .Skip((model.PageNumber - 1) * model.PageSize)
                .Take(model.PageSize);
        }

        public static IQueryable<T> GetTopTenMostPopularBooks<T>(this IQueryable<T> books) 
            where T : Book
        {
            return books.Include(b => b.Department)
                .OrderByDescending(b => b.BookCopies
                    .SelectMany(bc => bc.Bookings).Count())
                .Take(10);
        }
    }
}
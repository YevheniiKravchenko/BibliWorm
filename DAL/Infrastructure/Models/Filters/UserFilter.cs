using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Infrastructure.Models.Filters;
public class UserFilter : BaseFilter<User>
{
    public string? SearchQuery { get; set; }

    public override IQueryable<User> Filter(DbSet<User> users)
    {
        IQueryable<User> query = users.AsQueryable()
            .Include(u => u.ReaderCard);

        if (!string.IsNullOrEmpty(SearchQuery))
        {
            query = query.Where(u => u.Login.StartsWith(SearchQuery)
                || (u.ReaderCard.LastName + " " + u.ReaderCard.FirstName).StartsWith(SearchQuery)
                || u.ReaderCard.PhoneNumber.StartsWith(SearchQuery)
                || u.ReaderCard.Email.StartsWith(SearchQuery));
        }

        return query;
    }
}

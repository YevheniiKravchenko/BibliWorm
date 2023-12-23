using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Infrastructure.Models.Filters;
public class BookFilter : BaseFilter<Book>
{
    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? ISBN { get; set; }

    public DateTime? PublishedAfter { get; set; }

    public DateTime? PublishedBefore { get; set; }

    public string? KeyWords { get; set; }

    public List<int?>? Genres { get; set; }

    public int? DepartmentId { get; set; }

    public override IQueryable<Book> Filter(DbSet<Book> books)
    {
        var query = books.AsQueryable();

        if (!string.IsNullOrEmpty(Title))
            query = query.Where(b => b.Title.StartsWith(Title));

        if (!string.IsNullOrEmpty(Author))
            query = query.Where(b => b.Author.StartsWith(Author));

        if (!string.IsNullOrEmpty(ISBN))
            query = query.Where(b => b.ISBN.StartsWith(ISBN));

        if (PublishedAfter != null)
        {
            query = query.Where(b => b.PublicationDate != null
                && b.PublicationDate.Value >= PublishedAfter);
        }

        if (PublishedBefore != null)
        {
            query = query.Where(b => b.PublicationDate != null
                && b.PublicationDate.Value <= PublishedBefore);
        }

        if (!string.IsNullOrEmpty(KeyWords))
            query = query.Where(b => b.KeyWords.Contains(KeyWords));

        if (DepartmentId != null)
        {
            query = query.Where(b => b.DepartmentId != null
                && b.DepartmentId.Value == DepartmentId.Value);
        }

        if (Genres != null 
            && Genres.Count > 0 
            && Genres[0] != null)
        {
            query = query.Where(b => b.Genres
                .Select(g => g.EnumItemId)
                .Where(v => Genres.Contains(v))
                .Any()
            );
        }

        return query;
    }
}

namespace BibliWorm.Infrastructure.Models;

public class BookTheBookCopiesModel
{
    public int UserId { get; set; }

    public List<Guid> BookCopiesIds { get; set; }
}

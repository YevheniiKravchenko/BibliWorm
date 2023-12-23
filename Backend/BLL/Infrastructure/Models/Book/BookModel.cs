namespace BLL.Infrastructure.Models.Book;
public class BookModel
{
    public Guid BookId { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string ISBN { get; set; }

    public DateTime? PublicationDate { get; set; }

    public string Publisher { get; set; }

    public string Description { get; set; }

    public int PagesAmount { get; set; }

    public byte[] CoverImage { get; set; }

    public string KeyWords { get; set; }

    public int? DepartmentId { get; set; }

    public List<int> BookGenres { get; set; }

    public Dictionary<int, string> Genres { get; set; }

    public Dictionary<int, string> Departments { get; set; }
}

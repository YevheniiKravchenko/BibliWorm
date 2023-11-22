namespace BLL.Infrastructure.Models.Book;
public class BookListItemModel
{
    public Guid BookId { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string ISBN { get; set; }

    public string PublicationDate { get; set; }

    public string Description { get; set; }

    public string KeyWords { get; set; }

    public string DepartmentName { get; set; }
}

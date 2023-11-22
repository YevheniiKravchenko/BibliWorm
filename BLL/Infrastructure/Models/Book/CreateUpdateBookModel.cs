using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.Book;
public class CreateUpdateBookModel
{
    public Guid BookId { get; set; }

    [Required]
    public string Title { get; set; }

    public string Author { get; set; }

    [Required]
    public string ISBN { get; set; }

    public DateTime? PublicationDate { get; set; }

    public string Publisher { get; set; }

    public string Description { get; set; }

    public int PagesAmount { get; set; }

    public byte[] CoverImage { get; set; }

    public string KeyWords { get; set; }

    public int? DepartmentId { get; set; }
}

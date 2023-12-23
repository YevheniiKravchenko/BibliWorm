using System.ComponentModel.DataAnnotations;

namespace BibliWorm.Infrastructure.Models;

public class BookTheBookCopiesModel
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public List<Guid> BookCopiesIds { get; set; }
}

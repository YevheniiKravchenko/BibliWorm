using System.ComponentModel.DataAnnotations;

namespace BibliWorm.Infrastructure.Models;

public class ReserveBookModel
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid BookId { get; set; }
}

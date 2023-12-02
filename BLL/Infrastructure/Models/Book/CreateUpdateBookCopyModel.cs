using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.Book;
public class CreateUpdateBookCopyModel
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid BookCopyId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public string RFID { get; set; }

    [MaxLength(ValidationConstant.BookCopyConditionMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
    public string Condition { get; set; }

    public bool IsAvailable { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid BookId { get; set; }
}

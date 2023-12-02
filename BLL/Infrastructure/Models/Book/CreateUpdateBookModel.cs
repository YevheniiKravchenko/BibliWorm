using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.Book;
public class CreateUpdateBookModel
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid BookId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    [MinLength(ValidationConstant.BookTitleMinLength, ErrorMessage = "MIN_LENGTH_ERROR")]
    [MaxLength(ValidationConstant.BookTitleMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
    public string Title { get; set; }

    [MaxLength(ValidationConstant.AuthorMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
    public string Author { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public string ISBN { get; set; }

    public DateTime? PublicationDate { get; set; }

    [MaxLength(ValidationConstant.PublisherMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
    public string Publisher { get; set; }

    [MaxLength(ValidationConstant.DescriptionMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
    public string Description { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    [Range(ValidationConstant.PagesAmountMinValue, ValidationConstant.PagesAmountMaxValue, ErrorMessage = "RANGE_ERROR_FROM")]
    public int PagesAmount { get; set; }

    public byte[] CoverImage { get; set; }

    [MaxLength(ValidationConstant.KeyWordsMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
    public string KeyWords { get; set; }

    public int? DepartmentId { get; set; }
}

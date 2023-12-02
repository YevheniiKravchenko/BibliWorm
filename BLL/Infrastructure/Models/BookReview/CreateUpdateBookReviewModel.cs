using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.BookReview;
public class CreateUpdateBookReviewModel
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid BookReviewId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    [Range(ValidationConstant.MarkMinValue, ValidationConstant.MarkMaxValue, ErrorMessage = "RANGE_ERROR_BETWEEN")]
    public double Mark { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    [MinLength(ValidationConstant.CommentMinLength, ErrorMessage = "MIN_LENGTH_ERROR")]
    [MaxLength(ValidationConstant.CommentMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
    public string Comment { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid BookId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int UserId { get; set; }
}

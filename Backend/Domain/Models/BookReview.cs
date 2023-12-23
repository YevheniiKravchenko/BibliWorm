using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class BookReview
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

    #region Relations

    [JsonIgnore]
    public Book Book { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    #endregion
}

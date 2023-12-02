using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class EnumItem
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int EnumItemId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    [MaxLength(ValidationConstant.EnumItemValueMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
    public string Value { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int EnumId { get; set; }

    #region Relations

    [JsonIgnore]
    public Enum Enum { get; set; }

    [JsonIgnore]
    public ICollection<Book> Books { get; set; }

    #endregion
}

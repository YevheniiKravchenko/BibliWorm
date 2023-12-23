using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.EnumItem;
public class EnumItemModel
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int EnumItemId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    [MaxLength(ValidationConstant.EnumItemValueMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
    public string Value { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int EnumId { get; set; }
}

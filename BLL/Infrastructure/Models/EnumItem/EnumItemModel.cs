using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.EnumItem;
public class EnumItemModel
{
    [Required]
    public int EnumItemId { get; set; }

    [Required]
    public string Value { get; set; }

    [Required]
    public int EnumId { get; set; }
}

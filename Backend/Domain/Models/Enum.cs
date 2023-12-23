using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class Enum
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int EnumID { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public string Code { get; set; }

    #region Relations

    [JsonIgnore]
    public ICollection<EnumItem> EnumItems { get; set; }

    #endregion
}

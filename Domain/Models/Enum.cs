using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class Enum
{
    public int EnumID { get; set; }

    [Required]
    public string Code { get; set; }

    #region Relations

    [JsonIgnore]
    public ICollection<EnumItem> EnumItems { get; set; }

    #endregion
}

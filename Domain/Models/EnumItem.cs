using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class EnumItem
{
    public int EnumItemId { get; set; }

    [Required]
    public string Value { get; set; }

    public int EnumId { get; set; }

    #region Relations

    [JsonIgnore]
    public Enum Enum { get; set; }

    [JsonIgnore]
    public ICollection<Book> Books { get; set; }

    #endregion
}

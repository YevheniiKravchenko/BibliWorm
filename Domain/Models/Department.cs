using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class Department
{
    public int DepartmentId { get; set; }

    [Required]
    public string Name { get; set; }

    #region Relations

    [JsonIgnore]
    public ICollection<Book> Books { get; set; }

    [JsonIgnore]
    public ICollection<DepartmentStatistics> DepartmentStatistics { get; set; }

    #endregion
}

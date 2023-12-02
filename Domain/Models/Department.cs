using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class Department
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    [MaxLength(ValidationConstant.DepartmentNameMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
    public string Name { get; set; }

    #region Relations

    [JsonIgnore]
    public ICollection<Book> Books { get; set; }

    [JsonIgnore]
    public ICollection<DepartmentStatistics> DepartmentStatistics { get; set; }

    #endregion
}

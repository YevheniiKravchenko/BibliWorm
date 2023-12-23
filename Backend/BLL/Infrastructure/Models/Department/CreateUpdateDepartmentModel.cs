using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.Department;
public class CreateUpdateDepartmentModel
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    [MaxLength(ValidationConstant.DepartmentNameMaxLength, ErrorMessage = "MAX_LENGTH_ERROR")]
    public string Name { get; set; }
}

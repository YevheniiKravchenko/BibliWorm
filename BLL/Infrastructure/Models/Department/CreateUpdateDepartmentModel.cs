using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.Department;
public class CreateUpdateDepartmentModel
{
    [Required]
    public int DepartmentId { get; set; }

    [Required]
    public string Name { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.Department;
public class DepartmentModel
{
    public int DepartmentId { get; set; }

    public string Name { get; set; }

    public int PeopleAttendedForLastMonth { get; set; }
}

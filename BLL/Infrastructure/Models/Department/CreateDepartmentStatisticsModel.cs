using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.Department;
public class CreateDepartmentStatisticsModel
{
    [Required]
    public Guid DepartmentStatisticsId { get; set; }

    [Required]
    public DateTime RecordDate { get; set; }

    [Required]
    public int NumberOfPeopleAttended { get; set; }

    [Required]
    public int DepartmentId { get; set; }
}

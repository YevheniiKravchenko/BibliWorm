using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.Department;
public class CreateDepartmentStatisticsModel
{
    public Guid DepartmentStatisticsId { get; set; } = Guid.NewGuid();

    public DateTime RecordDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    [Range(ValidationConstant.NumberOfPeopleAttendedMinValue, ValidationConstant.NumberOfPeopleAttendedMaxValue, ErrorMessage = "RANGE_ERROR_FROM"),]
    public int NumberOfPeopleAttended { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int DepartmentId { get; set; }
}

using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models.Department;
public class CreateDepartmentStatisticsModel
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid DepartmentStatisticsId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public DateTime RecordDate { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    [Range(ValidationConstant.NumberOfPeopleAttendedMinValue, ValidationConstant.NumberOfPeopleAttendedMaxValue, ErrorMessage = "RANGE_ERROR_FROM"),]
    public int NumberOfPeopleAttended { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int DepartmentId { get; set; }
}

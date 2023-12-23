using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;
public class DepartmentStatistics
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Guid DepartmentStatisticsId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public DateTime RecordDate { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    [Range(ValidationConstant.NumberOfPeopleAttendedMinValue, ValidationConstant.NumberOfPeopleAttendedMaxValue, ErrorMessage = "RANGE_ERROR_FROM"), ]
    public int NumberOfPeopleAttended { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int DepartmentId { get; set; }

    #region Relations

    [JsonIgnore]
    public Department Department { get; set; }

    #endregion

}

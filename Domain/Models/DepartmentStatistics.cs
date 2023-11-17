using System.Text.Json.Serialization;

namespace Domain.Models;
public class DepartmentStatistics
{
    public Guid DepartmentStatisticsId { get; set; }

    public DateTime RecordDate { get; set; }

    public int NumberOfPeopleAttended { get; set; }

    public int DepartmentId { get; set; }

    #region Relations

    [JsonIgnore]
    public Department Department { get; set; }

    #endregion

}

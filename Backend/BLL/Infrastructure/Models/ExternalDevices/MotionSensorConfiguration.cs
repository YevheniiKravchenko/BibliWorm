namespace BLL.Infrastructure.Models.ExternalDevices;
public class MotionSensorConfiguration : ConfigurationBase
{
    public int DepartmentId { get; set; }

    public string ServerURL { get; set; }

    public int SendingPeriodInSeconds { get; set; }
}

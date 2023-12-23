using BLL.Infrastructure.Models.ExternalDevices;

namespace BLL.Contracts;
public interface IExternalDeviceConfigurer
{
    Response ConfigureRFIDReader(RFIDReaderConfiguration configuration);

    Response ConfigureMotionDetector(MotionSensorConfiguration configuration);
}

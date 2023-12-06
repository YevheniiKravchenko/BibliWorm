using BLL.Contracts;
using BLL.Infrastructure.Models.ExternalDevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliWorm.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ExternalDeviceController : ControllerBase
{
    private readonly IExternalDeviceConfigurer _externalDeviceConfigurer;

    public ExternalDeviceController(IExternalDeviceConfigurer externalDeviceConfigurer)
    {
        _externalDeviceConfigurer = externalDeviceConfigurer;
    }

    [HttpPost("configure-rfid-reader")]
    public ActionResult ConfigureRFIDReader(RFIDReaderConfiguration configuration)
    {
        var response = _externalDeviceConfigurer.ConfigureRFIDReader(configuration);

        return Ok(response);
    }

    [HttpPost("configure-motion-sensor")]
    public ActionResult ConfigureMotionSensor(MotionSensorConfiguration configuration)
    {
        var response = _externalDeviceConfigurer.ConfigureMotionDetector(configuration);

        return Ok(response);
    }
}

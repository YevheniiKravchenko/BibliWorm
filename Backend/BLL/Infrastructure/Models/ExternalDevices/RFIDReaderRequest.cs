namespace BLL.Infrastructure.Models.ExternalDevices;
public class RFIDReaderRequest : RequestBase
{
    public RFIDReaderConfiguration Configuration { get; set; }
}

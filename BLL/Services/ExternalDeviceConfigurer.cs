using BLL.Contracts;
using BLL.Infrastructure.Enums;
using BLL.Infrastructure.Models.ExternalDevices;
using Newtonsoft.Json;
using System.IO.Ports;

namespace BLL.Services;
public class ExternalDeviceConfigurer : IExternalDeviceConfigurer
{
    public ExternalDeviceConfigurer()
    { }

    public Response ConfigureMotionDetector(MotionSensorConfiguration configuration)
    {
        var request = new MotionSensorRequest
        {
            Command = Command.Configure,
            Configuration = configuration
        };

        var response = SendViaSerialPort(request);
        return response;
    }

    public Response ConfigureRFIDReader(RFIDReaderConfiguration configuration)
    {
        var request = new RFIDReaderRequest
        {
            Command = Command.Configure,
            Configuration = configuration
        };

        var response = SendViaSerialPort(request);
        return response;
    }

    private Response SendViaSerialPort(RequestBase request)
    {
        var serializedRequest = JsonConvert.SerializeObject(request);
        var serialPort = GetSerialPort(9600);
        var response = TrySendRequest(serialPort, serializedRequest);

        return response;
    }

    private SerialPort GetSerialPort(int baudRate)
    {
        var serialPorts = SerialPort.GetPortNames();
        var serialPort = new SerialPort(serialPorts[0], baudRate, Parity.None, 8, StopBits.One);

        return serialPort;
    }

    private Response TrySendRequest(SerialPort serialPort, string serializedRequest)
    {
        try
        {
            serialPort.Open();
            serialPort.WriteLine(serializedRequest);

            var serializedResponse = string.Empty;
            while (!serializedResponse.Contains("Body") || serializedResponse == null)
                serializedResponse = serialPort.ReadLine();

            var response = JsonConvert.DeserializeObject<Response>(serializedResponse);
            return response;
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            serialPort.Close();
        }
    }
}

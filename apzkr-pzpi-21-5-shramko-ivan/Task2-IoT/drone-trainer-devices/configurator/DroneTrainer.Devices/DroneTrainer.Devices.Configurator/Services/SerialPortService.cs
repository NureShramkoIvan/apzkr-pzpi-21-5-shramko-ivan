using DroneTrainer.Devices.Configurator.Constants;
using DroneTrainer.Devices.Configurator.Models;
using System.IO.Ports;
using System.Text.Json;

namespace DroneTrainer.Devices.Configurator.Services;

internal sealed class SerialPortService
{
    public bool WriteDeviceConfiguration(DeviceConfiguration configuration)
    {
        try
        {
            var port = new SerialPort(
                SerialPortDefaults.Name,
                SerialPortDefaults.BaudRate,
                SerialPortDefaults.ParityFlag,
                SerialPortDefaults.DataBits,
                SerialPortDefaults.StopBitsFlag);

            port.Open();

            var serializedConfig = JsonSerializer.Serialize(configuration);

            port.Write(serializedConfig);

            return true;
        }
        catch
        {
            return false;
        }
    }
}

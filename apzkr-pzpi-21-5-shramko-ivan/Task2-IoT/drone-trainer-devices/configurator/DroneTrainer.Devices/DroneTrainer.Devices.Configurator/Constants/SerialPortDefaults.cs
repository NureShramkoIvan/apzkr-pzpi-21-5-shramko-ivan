using System.IO.Ports;

namespace DroneTrainer.Devices.Configurator.Constants;

internal static class SerialPortDefaults
{
    public const string Name = "COM3";
    public const int BaudRate = 9600;
    public const int DataBits = 8;
    public const Parity ParityFlag = Parity.None;
    public const StopBits StopBitsFlag = StopBits.One;
}

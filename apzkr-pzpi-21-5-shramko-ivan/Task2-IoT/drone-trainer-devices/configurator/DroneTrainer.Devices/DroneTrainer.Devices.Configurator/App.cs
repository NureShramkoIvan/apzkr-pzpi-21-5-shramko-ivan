using DroneTrainer.Devices.Configurator.Models;

namespace DroneTrainer.Devices.Configurator;

internal sealed class App
{
    public DeviceConfiguration ReadDeviceConfiguration()
    {
        var config = new DeviceConfiguration();

        Console.Write("Welcome to Drone trainer device configurator!\n\n");

        Console.WriteLine("Enter wifi ssid:");
        config.SSID = Console.ReadLine();

        Console.WriteLine("Enter wifi password:");
        config.Password = Console.ReadLine();

        Console.WriteLine("Enter wifi device unique ID:");
        config.UniqueId = "gate" + Console.ReadLine();

        Console.WriteLine("Enter wifi device primary key:");
        config.PrimaryKey = Console.ReadLine();

        Console.Write("Begin configuration setting...\n\n");

        return config;
    }
}

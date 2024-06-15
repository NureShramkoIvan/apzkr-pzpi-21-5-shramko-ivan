//get input:
//wifi ssid
//wifi pass
//unique id

//create device id
//device primary key
using DroneTrainer.Devices.Configurator;
using DroneTrainer.Devices.Configurator.Services;

var app = new App();
var serialService = new SerialPortService();

var config = app.ReadDeviceConfiguration();
var res = serialService.WriteDeviceConfiguration(config);

Console.WriteLine(res ? "Configuration was set" : "Failed to set configuration");

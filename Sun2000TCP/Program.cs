
using Modbus.Device;
using Modbus.IO;
using SimpleWifi;
using SimpleWifi.Win32;
using Sun2000TCP.extension;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;


//var wifi = new Wifi();

//wifi.Disconnect();

//var accessPoints = wifi.GetAccessPoints();

//var accessPoint = accessPoints.FirstOrDefault(x => x.Name == "SUN2000-HV22A0643005");
//var connected = false;
//if (accessPoint != null && !accessPoint.IsConnected)
//{
//    connected = accessPoint.Connect("Changeme","Installer");


//}
//if (connected)
//{
//    wifi.Disconnect();
//}






byte slaveAddress = 0 ;
int port = 6607;
//int port = 502;
//IPAddress ipaddress = IPAddress.Parse("192.168.200.1");
IPAddress ipaddress = IPAddress.Parse("192.168.1.27");
TcpClient client = new TcpClient();
client.ReceiveTimeout = 2000;
client.SendTimeout = 2000;
client.Connect(ipaddress, port);


IModbusMaster masterRTU = ModbusSerialMaster.CreateRtu(client);


//var ushortArray = masterRTU.ReadHoldingRegisters(slaveAddress, 32090, 2);
var timeout = true;
byte addr = 50;
while(timeout)

try
{
        Console.WriteLine($"ddr: {addr}");
    var ushortArray = masterRTU.ReadHoldingRegisters(1, 32080, 2);
        timeout = false;
}
catch (Exception ex)
{
        Thread.Sleep(1000);
    ex = ex;
}




using Modbus.Device;
using ModBusUtil;
using ModBusUtil.extension;
using System.IO.Ports;
using System.Net.Sockets;

//using (SerialPort serialPort = new SerialPort("COM9", 9600, Parity.None, 8, StopBits.One))
String server = "192.168.1.10";
Int32 port = 8899;
// Prefer a using declaration to ensure the instance is Disposed later.

while (true)
{
    try
    {
        using (TcpClient client = new TcpClient(server, port))
        {
            //serialPort.Open();
            //IModbusMaster masterRTU = ModbusSerialMaster.CreateRtu(serialPort);
            IModbusMaster masterRTU = ModbusSerialMaster.CreateRtu(client);

            //ModBusUtil.ShowRegistersScan(masterRTU, 12, 65280);
            //ModBusUtil.ShowRegistersSTR(masterRTU, 12, 0, 16);
            var resp = MbUtil.ShowRegistersU16(masterRTU, 12, 16384, 32);
            //var resp = MbUtil.ShowRegistersU16(masterRTU);
            //MbUtil.ShowRegistersU16(masterRTU, 12, 12288, 2);
            //MbUtil.ShowRegistersU16(masterRTU, 12, 12298, 2);
            Console.WriteLine(resp.Length);
            Thread.Sleep(5000);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}






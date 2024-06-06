using Modbus.Device;
using Sun2000RS485.extension;
using System.IO.Ports;

using (SerialPort serialPort = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One))
{
    serialPort.Open();
    IModbusMaster masterRTU = ModbusSerialMaster.CreateRtu(serialPort);

    Sun2000Util.ShowRegistersBytes(masterRTU, 1, 30000, 36);
    Sun2000Util.ShowRegistersBytes(masterRTU, 1, 30070, 14);

}
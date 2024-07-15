using Modbus.Device;
using Sun2000RS485.extension;
using System.IO.Ports;

using (SerialPort serialPort = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One))
{
    serialPort.Open();
    IModbusMaster masterRTU = ModbusSerialMaster.CreateRtu(serialPort);

    Sun2000Util.ShowRegistersSTR(masterRTU, 1, 30000, 250);
    //Sun2000Util.ShowRegistersU16(masterRTU, 1, 30070, 3);
    //Sun2000Util.ShowRegistersU32(masterRTU, 1, 30073, 6);

}
using DDSU666.extension;
using Modbus.Device;
using System.IO.Ports;

byte slaveId = 1;
using (SerialPort serialPort = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One))
{
    serialPort.Open();
    IModbusMaster masterRTU = ModbusSerialMaster.CreateRtu(serialPort);
    var ushortArray = masterRTU.ReadHoldingRegisters(slaveId, 32016, 1);

    var len = ushortArray.Length;


    for (int i = 0; i < ushortArray.Length; i += 2)
    {
        var pair = new ushort[]
        {
                    ushortArray[i],
                    ushortArray[i+1],
        };
        var value = pair.ModbusWordArrayToFloat();
        Console.WriteLine($"addr: {8192 + i} content: {value}");
    }
}



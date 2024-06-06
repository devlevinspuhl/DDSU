using Modbus.Device;
using System;
using System.Reflection;


namespace Sun2000RS485.extension
{
    public class Sun2000Util
    {
        const string formatter = "{0,5}{1,17}{2,8}";
        public static void ShowRegistersBytes(IModbusMaster masterRTU, byte slaveId, ushort address, ushort numberOfPoints)
        {
            numberOfPoints /=2;
            
            var ushortArray = masterRTU.ReadHoldingRegisters(slaveId, address, numberOfPoints);
            var bytes = Ushort2Bytes(ushortArray);
            for (int index = 0; index < bytes.Length; index=index+2)
            {
                var high = bytes[index + 1];
                var low = bytes[index];
                Console.WriteLine(
                    $"addr: {address+index} content: {low}-{high} -> {Convert.ToChar(low)} {Convert.ToChar(high)}");
            }
        }
        public static byte[] Ushort2Bytes(ushort[] ushortArray)
        {
            var bytes = new byte[ushortArray.Length*2];
            var i = 0;
            foreach (var item in ushortArray)
            {

                bytes[i+1] = (byte)item;
                bytes[i] = (byte)(item >> 8);
                i += 2;
            }

            return bytes;
        }
    }
}

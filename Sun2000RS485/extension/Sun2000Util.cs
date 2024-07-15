using Modbus.Device;
using System;
using System.Reflection;


namespace Sun2000RS485.extension
{
    public class Sun2000Util
    {
        public static void ShowRegistersSTR(IModbusMaster masterRTU, byte slaveId, ushort address, ushort numberOfPoints)
        {
            ushort points = (ushort)(numberOfPoints / 2);
            points += (ushort)(numberOfPoints % 2);


            var ushortArray = masterRTU.ReadHoldingRegisters(slaveId, address, points);
            var bytes = Ushort2Bytes(ushortArray);
            for (int index = 0; index < numberOfPoints; index++)
            {
                Console.WriteLine(
                    $"addr: {address + index} content: {bytes[index]} -> {Convert.ToChar(bytes[index])}");
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

        internal static void ShowRegistersU16(IModbusMaster masterRTU, byte slaveId, ushort address, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        internal static void ShowRegistersU32(IModbusMaster masterRTU, int v1, int v2, int v3)
        {
            throw new NotImplementedException();
        }
    }
}

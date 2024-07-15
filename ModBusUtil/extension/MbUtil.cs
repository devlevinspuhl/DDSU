using Modbus.Device;

namespace ModBusUtil.extension
{
    public static class MbUtil
    {
        public static Single ModbusWordArrayToFloat(this UInt16[] data)
        {
            if (data.Length != 2)
                throw new ArgumentException("2 words of data required for a float");

            byte[] bData = new byte[4];

            byte[] w1 = BitConverter.GetBytes(data[0]);
            byte[] w2 = BitConverter.GetBytes(data[1]);

            //reverse words
            Array.Copy(w2, 0, bData, 0, 2);
            Array.Copy(w1, 0, bData, 2, 2);

            return BitConverter.ToSingle(bData, 0);
        }
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
        public static void ShowRegistersScan(IModbusMaster masterRTU, byte slaveId, int startAddr)
        {
            for(int address = startAddr; address < 65536;address++)
            {
                try
                {
                    var ushortArray = masterRTU.ReadHoldingRegisters(slaveId, (ushort)address, 1);
                    Console.WriteLine($"addr: {address.ToString("X4")} {address} content: {ushortArray[0]}");
                }
                catch (Exception ex)
                {
                    ex = ex;
                    if (address % 256 == 0)
                    {
                        Console.WriteLine($"addr: {address.ToString("X4")} {address}");
                    }
                }
            }
            
        }
        public static byte[] Ushort2Bytes(ushort[] ushortArray)
        {
            var bytes = new byte[ushortArray.Length * 2];
            var i = 0;
            foreach (var item in ushortArray)
            {

                bytes[i + 1] = (byte)item;
                bytes[i] = (byte)(item >> 8);
                i += 2;
            }

            return bytes;
        }

        public static ushort[] ShowRegistersU16(IModbusMaster masterRTU, byte slaveId, ushort address, ushort numberOfPoints)
        {
            var ushortArray = masterRTU.ReadHoldingRegisters(slaveId, address, numberOfPoints);
            var len = ushortArray.Length;

            
            for (int i = 0; i < ushortArray.Length; i += 2)
            {
                var value = UshortToFloat(ushortArray[i], ushortArray[i + 1]);
                Console.WriteLine($"addr: {(address + i).ToString("X4")} {address + i} content: {value}");
            }
            
            return ushortArray;
        }
        public static ushort[] ShowRegistersU16(IModbusMaster masterRTU)
        {
            var ushortArray = masterRTU.ReadHoldingRegisters();
            var len = ushortArray.Length;


            for (int i = 0; i < ushortArray.Length; i += 2)
            {
                var value = UshortToFloat(ushortArray[i], ushortArray[i + 1]);
                Console.WriteLine($"content: {value}");
            }

            return ushortArray;
        }
        public static float UshortToFloat(ushort first, ushort second)
        {
            var pair = new ushort[]
{
                            first,
                            second,
};
            return pair.ModbusWordArrayToFloat();
        }
    }
}

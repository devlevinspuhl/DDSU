using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDSU666.extension
{
    public static class ModBusUtil
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
    }
}

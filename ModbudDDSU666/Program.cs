using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModbudDDSU666
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //FirstMethod();
            SecondMethod();
            Console.ReadLine();
        }
        private static void FirstMethod()
        {
            byte slaveId = 12;
            using (SerialPort serialPort = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One))
            {
                serialPort.Open();

                Console.WriteLine("This is the beginning: ");
                IModbusMaster masterRTU = ModbusSerialMaster.CreateRtu(serialPort);
                //string hex_add = "0x2000";
                //ushort dec_add = 8192;//Convert.ToUInt16(hex_add, 16);
                ushort[] ushortArray = null;
                for (ushort dec_add = 8192; dec_add < (12288 + 10); dec_add += 2)
                {
                    if (dec_add == 8228) { dec_add = 12288; }
                    //Console.WriteLine("Value of hex: " + hex_add);
                    //Console.WriteLine("Value of ushort: " + dec_add);


                    ushort startAddress = dec_add;
                    ushort numberOfPoints = 2;
                    try
                    {
                        ushortArray = masterRTU.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);

                    }
                    catch (Exception ex)
                    {
                        ex = ex;
                        //continue;
                    }

                    var value = ModbusWordArrayToFloat(ushortArray);
                    Console.WriteLine($"{dec_add} {dec_add:X4} - float:{value} values:{ushortArray[0]} {ushortArray[1]}");


                    //Console.WriteLine("Here 0 " + ushortArray[0]);
                    //Console.WriteLine("Here 1 " + ushortArray[1]);

                    //ushort val1 = ushortArray[1];
                    //ushort val2 = ushortArray[0];

                    //var byteval1 = BitConverter.GetBytes(val1);
                    //var byteval2 = BitConverter.GetBytes(val2);

                    //byte[] temp2 = new byte[4];
                    //temp2[0] = byteval1[0];
                    //temp2[1] = byteval1[1];
                    //temp2[2] = byteval2[0];
                    //temp2[3] = byteval2[1];

                    //float myFloat = System.BitConverter.ToSingle(temp2, 0);

                    //Console.WriteLine("Value is: " + myFloat);
                }
            }

        }
        private static void SecondMethod()
        {
            byte slaveId = 12;
            using (SerialPort serialPort = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One))
            {
                serialPort.Open();
                IModbusMaster masterRTU = ModbusSerialMaster.CreateRtu(serialPort);
                var ushortArray = masterRTU.ReadHoldingRegisters(slaveId, 8192, 16);

                var len = ushortArray.Length;


                for (int i = 0; i < ushortArray.Length; i += 2)
                {
                    var pair = new ushort[]
                    {
                    ushortArray[i],
                    ushortArray[i+1],
                    };
                    var value = ModbusWordArrayToFloat(pair);
                    Console.WriteLine($"addr: {8192 + i} content: {value}");
                }
            }
        }
        private static Single ModbusWordArrayToFloat(UInt16[] data)
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

        private static UInt16[] FloatToModbusWordArray(float value)
        {
            UInt16[] data = new UInt16[2];

            byte[] bytes = BitConverter.GetBytes(value);

            byte[] w1 = new byte[2];
            byte[] w2 = new byte[2];

            Array.Copy(bytes, 0, w1, 0, 2);
            Array.Copy(bytes, 2, w2, 0, 2);

            //reverse words
            data[0] = BitConverter.ToUInt16(w2, 0);
            data[1] = BitConverter.ToUInt16(w1, 0);

            return data;
        }
    }
}

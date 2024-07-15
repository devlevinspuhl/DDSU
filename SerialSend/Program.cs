using System.IO.Ports;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SerialSend
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SerialPort serialPort = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
            serialPort.Open();
            var data = new Byte[64];
            while (true)
            {
                data = new Byte[64];
                var resp = serialPort.Read(data,0,64);
                var hexa = Convert.ToHexString(data);
                Console.WriteLine(hexa);
                Thread.Sleep(1000);
            }
        }
    }
}

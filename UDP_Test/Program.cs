using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using ModBusUtil.extension;
using System;
using Modbus.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using System.Buffers.Binary;
using System.Xml.Linq;

namespace UDP_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Connect();
        }
        static void Connect()
        {
            try
            {
                var server = "192.168.1.31";
                Int32 port = 1337;
                using UdpClient client = new UdpClient(port);
                var impSetpoint = 150;
                byte high = (byte)(impSetpoint / 256);
                byte low = (byte)(impSetpoint - high * 256);
                var imp = new byte[] { 0xF2, 0x00, 0x00, 0x01, low, high, 0x00 };
                var cks = Checksum(imp);
                var cmd = new byte[imp.Length+1];
                Array.Copy(imp,cmd,imp.Length);
                cmd[cmd.Length-1] = cks;
                //var a = 0xF2 ^ 0x01 ^ 0x00 ^ 0x0A ^ 0x55; 
                var a = 242 ^ 1 ^ 0x8A ^ 0x02 ^ 85;
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(server), port); // endpoint where server is listening
                client.Send(cmd, 8, ep);

                var data = client.Receive(ref ep);
                Console.WriteLine(Convert.ToHexString(data));
                Console.WriteLine(Convert.ToHexString(data, 7, 2));
                Console.WriteLine(Convert.ToHexString(data, 6, 1));
                float setpoint = (data[4] + data[5] * 256) / 10f;
                float currenttemp = (data[7] + data[8] * 256) / 10f;
                Console.WriteLine(setpoint);
                Console.WriteLine(currenttemp);
                Thread.Sleep(10000);
                Console.Clear();
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }
        private static byte Checksum(byte[] data)
        {
            int checksum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                checksum ^= data[i];
            }
            return (byte)(checksum ^ 0x55);
        }
    }
}

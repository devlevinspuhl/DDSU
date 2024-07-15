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
                var server = "192.168.1.30";
                Int32 port = 8080;
                using TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();
                while (true)
                {
                    var data = new Byte[82];
                    var responseData = string.Empty;
                    Int32 bytes = stream.Read(data, 0, data.Length - 0);
                    
                    var n1 = BitConverter.ToUInt16(data, 4);
                    if (n1 != 0x2000) continue;
                    Console.Clear();
                    Console.WriteLine(Convert.ToHexString(data));
                    var endian = new byte[69];
                    var bigendian = new byte[69];
                    Array.Copy(data, 11, endian, 0, 69);
                    for (int i = 0; i < endian.Length-1; i += 2)
                    {
                        bigendian[i + 1] = endian[i];
                        bigendian[i] = endian[i+1];

                    }
                    Console.WriteLine(Convert.ToHexString(bigendian));
                    for (int i = 0; i < bigendian.Length - 4; i += 4)
                    {


                        var conv = MbUtil.UshortToFloat(

                            BitConverter.ToUInt16(bigendian, i),
                            BitConverter.ToUInt16(bigendian, i + 2)
                            );
                        Console.WriteLine(conv);
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}

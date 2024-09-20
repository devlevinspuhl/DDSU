using System.Net.Sockets;
using System.Net;
using System.Net.Http;

namespace ImmersionHeaterConsole
{
    internal class Program
    {
        private static TcpClient tcpClient;

        static void Main(string[] args)
        {
            string tcpServer = "192.168.1.10";
            int tcpPort = 8000;

            try
            {
                using (tcpClient = new TcpClient(tcpServer, tcpPort))
                {

                    NetworkStream stream = tcpClient.GetStream();
                    var data = new Byte[82];
                    stream.Read(data, 0, data.Length - 0);
                    var n1 = BitConverter.ToUInt16(data, 4);
                    var endian = new byte[69];
                    var bigendian = new byte[69];
                    Array.Copy(data, 11, endian, 0, 69);
                    for (int i = 0; i < endian.Length - 1; i += 2)
                    {
                        bigendian[i + 1] = endian[i];
                        bigendian[i] = endian[i + 1];
                    }
                    switch (n1)
                    {
                        case 0x1A00:
                            Unknow(bigendian);
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        private static void Unknow(byte[] bigendian)
        {
            var percent = BitConverter.ToUInt16(bigendian, 8);

            Console.Clear();
            Console.WriteLine($"Capacity {percent} %");

        }
    }
}

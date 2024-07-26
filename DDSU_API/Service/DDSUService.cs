using DDSU_API.Model;
using System.Net.Http;
using System.Net.Sockets;
using System.Timers;

namespace DDSU_API.Service
{
    public class DDSUService : IDDSUService
    {
        private readonly IConfiguration _configuration;
        private readonly System.Timers.Timer timer1;
        private bool loop;
        private readonly string? server;
        private readonly int port;

        public DDSUService(IConfiguration configuration)
        {
            _configuration = configuration;
            server = _configuration.GetValue<string>("server");
            port = _configuration.GetValue<int>("port");
            timer1 = new System.Timers.Timer();
            var delay = _configuration.GetValue<int>("timeout");
            timer1.Interval = TimeSpan.FromSeconds(delay).TotalMilliseconds;
            timer1.Elapsed += new ElapsedEventHandler(Timer1_Event);
        }

        private void Timer1_Event(object? sender, ElapsedEventArgs e)
        {
            loop = false;
        }

        public object GetGridValues(int id)
        {
            

            using (var tcpClient = new TcpClient(server, port))
            {
                return ReadTCP(tcpClient, id);
                //NetworkStream stream = tcpClient.GetStream();
                //var data = new Byte[82];
                //while (loop)
                //{
                //    stream.Read(data, 0, data.Length - 0);
                //    var n1 = BitConverter.ToUInt16(data, 4);
                //    var endian = new byte[69];
                //    var bigendian = new byte[69];
                //    Array.Copy(data, 11, endian, 0, 69);
                //    for (int i = 0; i < endian.Length - 1; i += 2)
                //    {
                //        bigendian[i + 1] = endian[i];
                //        bigendian[i] = endian[i + 1];
                //    }
                //    if (n1 != 0x2200) continue;
                //    grid = GetGrid(bigendian);
                //    break;
                //}
            }
        }
        private object ReadTCP(TcpClient tcpClient, int id)
        {
            loop = true;
            timer1.Start();
            NetworkStream stream = tcpClient.GetStream();
            var data = new Byte[82];
            while (loop)
            {
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
                if (n1 != id) continue;
                loop = false;
                switch (id)
                {
                    case 0x2200: return GetGrid(bigendian);
                    case 0x2000: return GetMeter(bigendian);
                }

            }
            return null;
        }

        private DDSUMeter GetMeter(byte[] bigendian)
        {
            var meter = new DDSUMeter();
            var expPower = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 40),
                BitConverter.ToUInt16(bigendian, 42)
                );
            meter.exportPower = expPower.ToString("n2");
            var impPower = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 20),
                BitConverter.ToUInt16(bigendian, 22)
                );
            meter.importPower = impPower.ToString("n2");
            return meter;
        }
        private DDSUGrid GetGrid(byte[] bigendian)
        {
            var grid = new DDSUGrid();
            var conv = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 0),
                BitConverter.ToUInt16(bigendian, 2)
                );
            grid.voltage = conv.ToString();
            conv = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 4),
                BitConverter.ToUInt16(bigendian, 6)
                );
            grid.current = conv.ToString();
            conv = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 12),
                BitConverter.ToUInt16(bigendian, 14)
                );
            grid.power = conv.ToString();
            conv = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 64),
                BitConverter.ToUInt16(bigendian, 66)
                );
            grid.frequency = conv.ToString();
            return grid;
        }
        private float UshortToFloat(ushort first, ushort second)
        {
            var pair = new ushort[]
            {
                first,
                second
            };
            return ModbusWordArrayToFloat(pair);
        }
        private Single ModbusWordArrayToFloat(UInt16[] data)
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

using Microsoft.Win32;
using System.Drawing;
using System.Net.Sockets;

namespace LunaBatteryWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        //private TcpClient tcpClient;
        private readonly RegistryKey regkey;
        private readonly decimal ImportPrice = 0.258M;
        private readonly decimal ExportPrice = 0.21M;
        private float expCounter;
        private float impCounter;
        private bool impFirstTime;
        private bool expFirstTime;
        private TcpClient tcpClient;
        public Worker(ILogger<Worker> logger)
        {
            regkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WinRegistry\ddsu");
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string tcpServer = "192.168.1.10";
                int tcpPort = 8899;
                var data = new Byte[82];
                var endian = new byte[69];
                var bigendian = new byte[69];
                try
                {
                    using (tcpClient = new TcpClient(tcpServer, tcpPort))
                    {

                        NetworkStream stream = tcpClient.GetStream();

                        //stream.Read(data, 0, data.Length - 0);
                        await stream.ReadAsync(data, 0, data.Length - 0);
                        var n1 = BitConverter.ToUInt16(data, 4);

                        Array.Copy(data, 11, endian, 0, 69);
                        for (int i = 0; i < endian.Length - 1; i += 2)
                        {
                            bigendian[i + 1] = endian[i];
                            bigendian[i] = endian[i + 1];
                        }
                        //var test1 = bigendian.Where(x => x != 0).ToList();
                        //if (test1.Count == 0) return;
                        switch (n1)
                        {
                            case 0x1A00:
                                Unknow(bigendian);
                                break;
                            case 0x2000:
                                RightPanel(bigendian);
                                break;
                        }

                    }
                    tcpClient.Close();
                    tcpClient.Dispose();
                }
                catch (Exception ex)
                {
                    ex = ex;
                    tcpClient.Close();
                    tcpClient.Dispose();
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
        private void LeftPanel(byte[] bigendian)
        {
            var conv = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 0),
                BitConverter.ToUInt16(bigendian, 2)
                );
            Console.WriteLine($"Voltage {conv.ToString()} Vac");
            conv = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 4),
                BitConverter.ToUInt16(bigendian, 6)
                );
            Console.WriteLine($"Current {conv.ToString()} A");
            conv = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 12),
                BitConverter.ToUInt16(bigendian, 14)
                );
            //txPower.Text = conv.ToString();
            Console.WriteLine($"Power {conv.ToString()} KW");

            conv = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 64),
                BitConverter.ToUInt16(bigendian, 66)
                );
            Console.WriteLine($"Frequency {conv.ToString()} Hz");

        }
        private void RightPanel(byte[] bigendian)
        {
            var regImpPower = float.Parse(regkey.GetValue("ImpPower")?.ToString() ?? "0");
            var regExpPower = float.Parse(regkey.GetValue("ExpPower")?.ToString() ?? "0");
            var timeImport = DateTime.Parse(regkey.GetValue("TimeImport")?.ToString() ?? DateTime.Now.ToString());
            var timeExport = DateTime.Parse(regkey.GetValue("TimeExport")?.ToString() ?? DateTime.Now.ToString());
            var expPower = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 40),
                BitConverter.ToUInt16(bigendian, 42)
                );
            var txExpPower = (expPower - regExpPower).ToString("n2");
            Console.WriteLine($"Export {txExpPower}");
            var impPower = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 20),
                BitConverter.ToUInt16(bigendian, 22)
                );
            var txImpPower = (impPower - regImpPower).ToString("n2");

            Console.WriteLine($"Import {txImpPower}");
            var crediDebitValue = CreditDebitCalc(decimal.Parse(txImpPower), decimal.Parse(txExpPower));

            Console.WriteLine($"Credit/Debit {crediDebitValue.ToString("n2")}");

            if (expCounter == 0)
            {
                expCounter = expPower;
                //timeExpStarted = DateTime.Now;
            }
            if (impCounter == 0)
            {
                impCounter = impPower;
                //timeImpStarted = DateTime.Now;
            }



            var diff = expPower - expCounter;
            if (diff == 0)
            {
                expFirstTime = false;

            }
            if (diff > 0 && !expFirstTime)
            {
                timeExport = DateTime.Now;
                regkey.SetValue("TimeExport", DateTime.Now);
                expFirstTime = true;
            }
            var diff2 = impPower - impCounter;
            if (diff2 == 0)
            {
                impFirstTime = false;
            }

            if (diff2 > 0 && !impFirstTime)
            {
                timeImport = DateTime.Now;
                regkey.SetValue("TimeImport", timeImport);
                impFirstTime = true;
            }

            var txExpCounter = diff.ToString("n2");
            var txImpCounter = diff2.ToString("n2");
            Console.WriteLine($"Current export {txExpCounter}");
            Console.WriteLine($"Current import {txImpCounter}");

        }
        public decimal CreditDebitCalc(decimal import, decimal export)
        {
            var importValue = import * ImportPrice;
            var exportValue = export * ExportPrice;
            var creditDebit = exportValue - importValue;
            return creditDebit / ImportPrice;
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
        private void Unknow(byte[] bigendian)
        {
            var percent = BitConverter.ToUInt16(bigendian, 8);

            //Console.Clear();
            Console.WriteLine($"Capacity {percent} %");
        }
    }
}

using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using ModBusUtil.extension;
using System;
using Modbus.Utility;
using static ModBusUtil.extension.MbUtil;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Win32;


namespace UDP_Test
{
    internal class Program
    {
        static UdpClient client;
        private static RegistryKey regkey;
        private static decimal ImportPrice = 0.258M;
        private static decimal ExportPrice = 0.21M;
        private static float expCounter;
        private static float impCounter;
        private static bool impFirstTime;
        private static bool expFirstTime;

        static void Main(string[] args)
        {
            regkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WinRegistry\ddsu");
            Connect();
        }
        static void Connect()
        {
            var server = "192.168.1.10";
            var port = 9999;
            var firstTime = true;
            while (true)
            {
                try
                {
                    using (client = new UdpClient(port))
                    {
                        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(server), port);
                        var cmd = new byte[1];
                        client.Send(cmd, 1, ep);
                        var data = new Byte[82];
                        
                        var recData = client.Receive(ref ep);
                        if (recData.Length < 70) continue;
                        var maxize = 82;
                        if (recData.Length < 82)
                        {
                            maxize = recData.Length;
                        }
                        Array.Copy(recData,data, maxize);
                        //Console.WriteLine($"data length {data.Length}");
                        var n1 = BitConverter.ToUInt16(data, 4);
                        //Console.WriteLine($"n1 {n1.ToString("X")}");
                        var endian = new byte[69];
                        var bigendian = new byte[69];
                        Array.Copy(data, 11, endian, 0, 69);
                        for (int i = 0; i < endian.Length - 1; i += 2)
                        {
                            bigendian[i + 1] = endian[i];
                            bigendian[i] = endian[i + 1];
                        }
                        //var sushortarry = data.Chunk(2).Select(b => BitConverter.ToUInt16(b)).ToArray();
                        switch (n1)
                        {
                            case 0x1A00:
                                Console.Write("Battery ");
                                Console.WriteLine($"{BitConverter.ToUInt16(data, 20)}% ");
                                Console.WriteLine($"data length {recData.Length}");

                                break;
                            case 0x2000:
                                if (recData.Length < 77) break;
                                Console.WriteLine("Import/Export ");
                                Console.WriteLine($"data length {recData.Length}");
                                RightPanel(bigendian);
                                break;
                            case 0x2200:
                                if (recData.Length < 81) break;
                                Console.WriteLine("House Consume ");
                                Console.WriteLine($"data length {recData.Length}");
                                LeftPanel(bigendian);
                                break;
                        }
                        
                    }
                    client.Close();
                    client.Dispose();


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    break;
                }
            }

        }
        private static void RightPanel(byte[] bigendian)
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
        private static void LeftPanel(byte[] bigendian)
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
        private static decimal CreditDebitCalc(decimal import, decimal export)
        {
            var importValue = import * ImportPrice;
            var exportValue = export * ExportPrice;
            var creditDebit = exportValue - importValue;
            return creditDebit / ImportPrice;
        }
        private static void PrintUshort(byte[] data)
        {
           // var ushortArray = data.Chunk(2).Select(b => BitConverter.ToUInt16(b)).ToArray();
            //for (int i = 0; i < ushortArray.Length; i += 2)
            //{
            //    var value = UshortToFloat(ushortArray[i], ushortArray[i + 1]);
            //    Console.WriteLine($"content: {value}");
            //}
            foreach(byte value in data)
            {
                Console.WriteLine($"content: {value}");
            }
        }
    }
}

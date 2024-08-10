using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using NLog;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DDSUFormTCP
{
    public partial class Form1 : Form
    {
        private readonly System.Timers.Timer timer1;
        //private readonly System.Timers.Timer timer2;
        private readonly System.Timers.Timer timer3;
        //private readonly System.Timers.Timer timer4;
        private TcpClient? tcpClient;
        private readonly Logger logger;
        private float expCounter;
        private DateTime timeExpStarted = DateTime.Now;
        private DateTime timeImpStarted = DateTime.Now;
        private float impCounter;
        private readonly RegistryKey regkey;
        private bool impFirstTime;
        private bool expFirstTime;
        private bool combFirstTime;
        string tcpServer = "192.168.1.30";
        int tcpPort = 8080;
        string udpServer = "192.168.1.31";
        int udpPort = 1337;
        string path = @"TempGraph.csv";
        private bool enableArchive;
        private int maxMinute;
        private float expPower, impPower;

        private readonly IConfiguration _configuration;
        private  MySettings? _settings;

        public Form1()
        {
            InitializeComponent();
            regkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WinRegistry\ddsu");
            logger = LogManager.GetLogger("Form1");
            logger.Info("Started");
            timer1 = new System.Timers.Timer();
            timer1.Interval = 100;
            timer1.Elapsed += new ElapsedEventHandler(Timer1_Event);
            //timer2 = new System.Timers.Timer();
            //timer2.Interval = 15000;
            //timer2.Elapsed += new ElapsedEventHandler(Timer2_Event);
            //timer2.Enabled = true;
            enableArchive = true;
            //timer3 = new System.Timers.Timer();
            //timer3.Interval = 1000;
            //timer3.Elapsed += new ElapsedEventHandler(Timer3_Event);
            //timer3.Enabled = true;

            //timer4 = new System.Timers.Timer();
            //timer4.Interval = 30000;
            //timer4.Elapsed += new ElapsedEventHandler(Timer4_Event);
            //timer4.Enabled = true;

            timer1.Enabled = true;
            timer1.Start();
        }

        //private void Timer4_Event(object? sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        var date = DateTime.Now;
        //        var line = $"{date.ToString("dd/MM/yyyy HH:mm:ss")},{txTempHigh.Text}";
        //        AppendTextLine(line);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex);
        //    }

        //}

        //private void Timer3_Event(object? sender, ElapsedEventArgs e)
        //{
        //    timer3.Stop();
        //    ReadUDP();
        //    timer3.Start();
        //}
        private void Timer2_Event(object? sender, ElapsedEventArgs e)
        {
            //timer2.Stop();
            timer1.Stop();
            tcpClient?.Close();
            tcpClient?.Dispose();
            timer1.Start();
        }

        private void Timer1_Event(object? sender, ElapsedEventArgs e)
        {
            timer1.Stop();
            //timer2.Start();
            ReadTCP();
            //timer2.Stop();
            timer1.Start();
        }
        //private void ReadUDP()
        //{
        //    Invoke(new Action(() =>
        //    {
        //        try
        //        {
        //            var now = DateTime.Now;
        //            var sec = now.Second;
        //            var min = now.Minute;
        //            if (sec > 30 && enableArchive)
        //            {
        //                maxMinute = min;
        //                var line = $"{now.ToString("dd/MM/yyyy HH:mm:ss")},{txTempHigh.Text}";
        //                //AppendTextLine(line);
        //                logger.Info(line);
        //                enableArchive = false;
        //            }
        //            if (min > maxMinute)
        //            {
        //                enableArchive = true;
        //            }
        //            //if (now.Hour==0 && now.Minute==0 && now.Second == 0 && now.Millisecond < 1000)
        //            //{
        //            //    expCounter = 0;
        //            //    impCounter = 0;
        //            //}
        //            using (UdpClient client = new UdpClient(udpPort))
        //            {
        //                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(udpServer), udpPort);
        //                client.Send(new byte[] { 241, 0, 0, 164 }, 4, ep);
        //                var data = client.Receive(ref ep);
        //                float setpoint = (data[4] + data[5] * 256) / 10f;
        //                int index = comboBox1.FindStringExact(setpoint.ToString());
        //                if (index != -1 && !combFirstTime)
        //                {
        //                    combFirstTime = true;
        //                    comboBox1.SelectedIndex = index;

        //                }
        //                float tempHigh = (data[7] + data[8] * 256) / 10f;
        //                lbOnOff.Text = data[9] == 0 ? "OFF" : "ON";
        //                float tempLow = (data[11] + data[12] * 256) / 10f;
        //                txSetpoint.Text = setpoint.ToString("n1");
        //                txTempHigh.Text = tempHigh.ToString("n1");
        //                txTempLow.Text = tempLow.ToString("n1");
        //            }


        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error(ex);
        //        }

        //    }
        //    ));
        //}

        private void AppendTextLine(string line)
        {

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("datetime,highTemp");

                }
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(line);

            }
        }

        private void ReadTCP()
        {
            Invoke(new Action(() =>
            {
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
                            case 0x200:
                                break;
                            case 0x2200:
                                LeftPanel(bigendian);
                                break;
                            case 0x2000:
                                RightPanel(bigendian);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }));
        }
        private void RightPanel(byte[] bigendian)
        {
            var regImpPower = float.Parse(regkey.GetValue("ImpPower")?.ToString() ?? "0");
            var regExpPower = float.Parse(regkey.GetValue("ExpPower")?.ToString() ?? "0");
            var timeImport = DateTime.Parse(regkey.GetValue("TimeImport")?.ToString() ?? DateTime.Now.ToString());
            var timeExport = DateTime.Parse(regkey.GetValue("TimeExport")?.ToString() ?? DateTime.Now.ToString());
            expPower = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 40),
                BitConverter.ToUInt16(bigendian, 42)
                );
            txExpPower.Text = (expPower - regExpPower).ToString("n2");
            impPower = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 20),
                BitConverter.ToUInt16(bigendian, 22)
                );
            txImpPower.Text = (impPower - regImpPower).ToString("n2");
            var crediDebitValue = _settings.CreditDebitCalc(decimal.Parse(txImpPower.Text), decimal.Parse(txExpPower.Text));

            TxCresitDebit.Text = crediDebitValue.ToString("n2");
            TxCresitDebit.ForeColor = crediDebitValue < 0 ? Color.Red : Color.Blue;
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
            
            if (diff > 0 && expFirstTime)
            {
                timeExport = DateTime.Now;
                regkey.SetValue("TimeExport", DateTime.Now);
                expFirstTime = false;
            }
            //expFirstTime = diff == 0;
            var diff2 = impPower - impCounter;
            
            if (diff2 > 0 && impFirstTime)
            {
                timeImport = DateTime.Now;
                regkey.SetValue("TimeImport", timeImport);
                impFirstTime = false;
            }
            //impFirstTime = diff2 == 0;
            txExpCounter.Text = diff.ToString("n2");
            txImpCounter.Text = diff2.ToString("n2");
            lbTimeExp.Text = timeExport.ToString("HH:mm");
            lbTimeImp.Text = timeImport.ToString("HH:mm");
        }
        private void LeftPanel(byte[] bigendian)
        {
            var conv = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 0),
                BitConverter.ToUInt16(bigendian, 2)
                );
            txVoltage.Text = conv.ToString();
            conv = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 4),
                BitConverter.ToUInt16(bigendian, 6)
                );
            txCurrent.Text = conv.ToString();
            conv = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 12),
                BitConverter.ToUInt16(bigendian, 14)
                );
            txPower.Text = conv.ToString();
            txPower.ForeColor = conv < 0 ? Color.Green : Color.Red;
            conv = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 64),
                BitConverter.ToUInt16(bigendian, 66)
                );
            txFrequency.Text = conv.ToString();
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
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            ReadTCP();
            timer1.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Text = "Stopped";
        }
        private void BtResetExpCounter_Click(object sender, EventArgs e)
        {
            expCounter = 0;
            regkey.SetValue("ExpCounter", expCounter);
            regkey.SetValue("TimeExport", DateTime.Now);
            expFirstTime = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            impCounter = 0;
            regkey.SetValue("ImpCounter", impCounter);
            regkey.SetValue("TimeImport", DateTime.Now);
            impFirstTime = true;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            regkey.SetValue("ImpCounter", impCounter);
            regkey.SetValue("ExpCounter", expCounter);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            impFirstTime = true;
            expFirstTime = true;
            if (regkey != null)
            {
                impCounter = float.Parse(regkey.GetValue("ImpCounter")?.ToString() ?? "0");
                expCounter = float.Parse(regkey.GetValue("ExpCounter")?.ToString() ?? "0");
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _settings = Program.Configuration.GetSection("MySettings").Get<MySettings>();
            
        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var spoint = comboBox1.SelectedItem.ToString();
        //        var impSetpoint = int.Parse(spoint) * 10;
        //        byte high = (byte)(impSetpoint / 256);
        //        byte low = (byte)(impSetpoint - high * 256);
        //        var imp = new byte[] { 0xF2, 0x00, 0x00, 0x01, low, high, 0x00 };
        //        var cks = Checksum(imp);
        //        var cmd = new byte[imp.Length + 1];
        //        Array.Copy(imp, cmd, imp.Length);
        //        cmd[cmd.Length - 1] = cks;

        //        using (UdpClient client = new UdpClient(udpPort))
        //        {
        //            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(udpServer), udpPort);
        //            client.Send(cmd, 8, ep);
        //            var data = client.Receive(ref ep);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private static byte Checksum(byte[] data)
        {
            int checksum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                checksum ^= data[i];
            }
            return (byte)(checksum ^ 0x55);
        }

        private void btMeterReset_Click(object sender, EventArgs e)
        {
            regkey.SetValue("ImpPower", impPower);
            regkey.SetValue("ExpPower", expPower);
        }
    }
}


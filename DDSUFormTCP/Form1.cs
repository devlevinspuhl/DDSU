using Microsoft.Win32;
using NLog;
using System.Net.Sockets;
using System.Timers;

namespace DDSUFormTCP
{
    public partial class Form1 : Form
    {
        private readonly System.Timers.Timer timer1;
        private readonly System.Timers.Timer timer2;
        private TcpClient? tcpClient;
        private readonly Logger logger;
        private float expCounter;
        private DateTime timeExpStarted = DateTime.Now;
        private DateTime timeImpStarted = DateTime.Now;
        private float impCounter;
        private readonly RegistryKey regkey;
        private bool impFirstTime;
        private bool expFirstTime;
        string server = "192.168.1.30";
        int port = 8080;

        public Form1()
        {
            InitializeComponent();
            regkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WinRegistry\ddsu");
            logger = LogManager.GetLogger("Form1");
            logger.Info("Started");
            timer1 = new System.Timers.Timer();
            timer1.Interval = 100;
            timer1.Elapsed += new ElapsedEventHandler(Timer1_Event);
            timer2 = new System.Timers.Timer();
            timer2.Interval = 15000;
            timer2.Elapsed += new ElapsedEventHandler(Timer2_Event);
            timer2.Enabled = true;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void Timer2_Event(object? sender, ElapsedEventArgs e)
        {
            timer2.Stop();
            timer1.Stop();
            tcpClient?.Close();
            tcpClient?.Dispose();
            timer1.Start();
        }

        private void Timer1_Event(object? sender, ElapsedEventArgs e)
        {
            timer1.Stop();
            timer2.Start();
            ReadTCP();
            timer2.Stop();
            timer1.Start();
        }
        private void ReadTCP()
        {
            Invoke(new Action(() =>
            {
                try
                {
                    using (tcpClient = new TcpClient(server, port))
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
            var expPower = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 40),
                BitConverter.ToUInt16(bigendian, 42)
                );
            txExpPower.Text = expPower.ToString("n2");
            var impPower = UshortToFloat(
                BitConverter.ToUInt16(bigendian, 20),
                BitConverter.ToUInt16(bigendian, 22)
                );
            txImpPower.Text = impPower.ToString("n2");
            if (expCounter == 0)
            {
                expCounter = expPower;
                timeExpStarted = DateTime.Now;
            }
            if (impCounter == 0)
            {
                impCounter = impPower;
                timeImpStarted = DateTime.Now;
            }
            var diff = expPower - expCounter;
            if (diff > 0 && expFirstTime)
            {
                timeExpStarted = DateTime.Now;
            }
            var diff2 = impPower - impCounter;
            if (diff2 > 0 && impFirstTime)
            {
                impFirstTime = false;
                timeImpStarted = DateTime.Now;
            }
            expFirstTime = diff == 0;
            impFirstTime = diff2 == 0;
            txExpCounter.Text = diff.ToString("n2");
            txImpCounter.Text = diff2.ToString("n2");
            lbTimeExp.Text = timeExpStarted.ToString("HH:mm");
            lbTimeImp.Text = timeImpStarted.ToString("HH:mm");
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
        }
        private void button3_Click(object sender, EventArgs e)
        {
            impCounter = 0;
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
    }
}


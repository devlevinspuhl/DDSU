using Microsoft.Win32;
using Modbus.Device;
using ModBusUtil.extension;
using NLog;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using System.Windows.Forms;

namespace DDSUForm
{
    public partial class Form1 : Form
    {
        private readonly IModbusMaster masterRTU;
        private readonly System.Timers.Timer timer1;
        private readonly System.Timers.Timer timer2;
        //private SerialPort serialPort;
        private TcpClient client;
        private readonly Logger logger;
        private float expCounter;
        private DateTime timeExpStarted = DateTime.Now;
        private DateTime timeImpStarted = DateTime.Now;
        private float impCounter;
        private readonly RegistryKey regkey;
        private  bool impFirstTime;
        private  bool expFirstTime;

        public Form1()
        {
            InitializeComponent();
            regkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WinRegistry\ddsu");
            logger = LogManager.GetLogger("Form1");
            logger.Info("Started");
            client = new TcpClient();
            client.Connect(IPAddress.Parse("192.168.1.29"), 23);
            //serialPort = new SerialPort("COM7", 9600, Parity.None, 8, StopBits.One);

            timer1 = new System.Timers.Timer();
            timer1.Interval = 500;
            timer1.Elapsed += new ElapsedEventHandler(Timer1_Event);

            timer2 = new System.Timers.Timer();
            timer2.Interval = 4000;
            timer2.Elapsed += new ElapsedEventHandler(Timer2_Event);
            timer2.Enabled = true;

            timer1.Enabled = true;
            timer1.Start();

        }

        private void Timer2_Event(object? sender, ElapsedEventArgs e)
        {
            timer2.Stop();
            timer1.Stop();
            client.Close();
            client.Dispose();
            client = new TcpClient();
            client.Connect(IPAddress.Parse("192.168.1.29"), 23);
            //serialPort.Close();
            //serialPort.Dispose();
            //serialPort = new SerialPort("COM10", 9600, Parity.None, 8, StopBits.One);
            timer1.Start();
        }

        private void Timer1_Event(object? sender, ElapsedEventArgs e)
        {
            timer1.Stop();
            timer2.Start();
            DoCommunication();
            timer2.Stop();
            timer1.Start();
        }

        private void DoCommunication()
        {
            Invoke(new Action(() =>
            {
                try
                {
                    this.Text = $"DDSU Monitor - {DateTime.Now.ToString("HH:mm:ss")}";
                    //serialPort.Open();
                    //client = new TcpClient();
                    //client.Connect(IPAddress.Parse("192.168.1.29"), 23);
                    var masterRTU = ModbusSerialMaster.CreateRtu(client);

                    var stream1 = MbUtil.ShowRegistersU16(masterRTU, 12, 8192, 34);
                    var voltage = MbUtil.UshortToFloat(stream1[0], stream1[1]);
                    txVoltage.Text = voltage.ToString("n2");
                    //masterRTU.Dispose();


                    var current = MbUtil.UshortToFloat(stream1[2], stream1[3]);
                    txCurrent.Text = current.ToString("n4");



                    var power = MbUtil.UshortToFloat(stream1[6], stream1[7]);
                    txPower.Text = power.ToString("n4");
                    txPower.ForeColor = power < 0 ? Color.Green : Color.Red;


                    var frequency = MbUtil.UshortToFloat(stream1[32], stream1[33]);
                    txFrequency.Text = frequency.ToString("n2");

                    //serialPort.Open();
                    //client = new TcpClient();
                    //client.Connect(IPAddress.Parse("192.168.1.29"), 23);
                    //masterRTU = ModbusSerialMaster.CreateRtu(client);
                    var stream3 = MbUtil.ShowRegistersU16(masterRTU, 12, 12288, 2);
                    var impPower = MbUtil.UshortToFloat(stream3[0], stream3[1]);
                    txImpPower.Text = impPower.ToString("n2");
                    //masterRTU.Dispose();

                    //serialPort.Open();
                    //client = new TcpClient();
                    //client.Connect(IPAddress.Parse("192.168.1.29"), 23);
                    //masterRTU = ModbusSerialMaster.CreateRtu(client);
                    var stream2 = MbUtil.ShowRegistersU16(masterRTU, 12, 12298, 2);
                    var expPower = MbUtil.UshortToFloat(stream2[0], stream2[1]);
                    txExpPower.Text = expPower.ToString("n2");
                    //masterRTU.Dispose();
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
                    if(diff>0 && expFirstTime)
                    {
                        timeExpStarted = DateTime.Now;
                    }
                    var diff2 = impPower - impCounter;
                    if(diff2>0 && impFirstTime)
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
                catch (Exception ex)
                {
                    logger.Error(ex);
                }


            }));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            DoCommunication();
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

namespace DDSUFormTCP
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txVoltage = new TextBox();
            txCurrent = new TextBox();
            txPower = new TextBox();
            txFrequency = new TextBox();
            txImpPower = new TextBox();
            txExpPower = new TextBox();
            groupBox2 = new GroupBox();
            lbFrequency = new Label();
            lbPower = new Label();
            lbCurrent = new Label();
            blVoltage = new Label();
            label5 = new Label();
            label4 = new Label();
            groupBox1 = new GroupBox();
            label8 = new Label();
            lbTimeImp = new Label();
            button3 = new Button();
            label7 = new Label();
            label6 = new Label();
            label3 = new Label();
            txImpCounter = new TextBox();
            BtResetExpCounter = new Button();
            lbTimeExp = new Label();
            txExpCounter = new TextBox();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            groupBox3 = new GroupBox();
            lbOnOff = new Label();
            label13 = new Label();
            label14 = new Label();
            txTempLow = new TextBox();
            label10 = new Label();
            label9 = new Label();
            label11 = new Label();
            label12 = new Label();
            txSetpoint = new TextBox();
            txTempHigh = new TextBox();
            comboBox1 = new ComboBox();
            bindingSource1 = new BindingSource(components);
            label15 = new Label();
            button4 = new Button();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // txVoltage
            // 
            txVoltage.Font = new Font("Segoe UI", 16F);
            txVoltage.Location = new Point(15, 72);
            txVoltage.Name = "txVoltage";
            txVoltage.Size = new Size(150, 50);
            txVoltage.TabIndex = 0;
            txVoltage.TextAlign = HorizontalAlignment.Right;
            // 
            // txCurrent
            // 
            txCurrent.Font = new Font("Segoe UI", 16F);
            txCurrent.Location = new Point(15, 132);
            txCurrent.Name = "txCurrent";
            txCurrent.Size = new Size(150, 50);
            txCurrent.TabIndex = 1;
            txCurrent.TextAlign = HorizontalAlignment.Right;
            // 
            // txPower
            // 
            txPower.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            txPower.Location = new Point(15, 192);
            txPower.Name = "txPower";
            txPower.Size = new Size(150, 50);
            txPower.TabIndex = 2;
            txPower.TextAlign = HorizontalAlignment.Right;
            // 
            // txFrequency
            // 
            txFrequency.Font = new Font("Segoe UI", 16F);
            txFrequency.Location = new Point(15, 252);
            txFrequency.Name = "txFrequency";
            txFrequency.Size = new Size(150, 50);
            txFrequency.TabIndex = 3;
            txFrequency.TextAlign = HorizontalAlignment.Right;
            // 
            // txImpPower
            // 
            txImpPower.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            txImpPower.ForeColor = Color.Red;
            txImpPower.Location = new Point(101, 74);
            txImpPower.Name = "txImpPower";
            txImpPower.Size = new Size(150, 50);
            txImpPower.TabIndex = 4;
            txImpPower.TextAlign = HorizontalAlignment.Right;
            // 
            // txExpPower
            // 
            txExpPower.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            txExpPower.ForeColor = Color.Green;
            txExpPower.Location = new Point(101, 134);
            txExpPower.Name = "txExpPower";
            txExpPower.Size = new Size(150, 50);
            txExpPower.TabIndex = 5;
            txExpPower.TextAlign = HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lbFrequency);
            groupBox2.Controls.Add(lbPower);
            groupBox2.Controls.Add(lbCurrent);
            groupBox2.Controls.Add(blVoltage);
            groupBox2.Controls.Add(txVoltage);
            groupBox2.Controls.Add(txCurrent);
            groupBox2.Controls.Add(txPower);
            groupBox2.Controls.Add(txFrequency);
            groupBox2.Location = new Point(40, 27);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(382, 327);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "House Consume";
            // 
            // lbFrequency
            // 
            lbFrequency.AutoSize = true;
            lbFrequency.Font = new Font("Segoe UI", 14F);
            lbFrequency.Location = new Point(171, 258);
            lbFrequency.Name = "lbFrequency";
            lbFrequency.Size = new Size(50, 38);
            lbFrequency.TabIndex = 8;
            lbFrequency.Text = "Hz";
            // 
            // lbPower
            // 
            lbPower.AutoSize = true;
            lbPower.Font = new Font("Segoe UI", 14F);
            lbPower.Location = new Point(171, 198);
            lbPower.Name = "lbPower";
            lbPower.Size = new Size(52, 38);
            lbPower.TabIndex = 7;
            lbPower.Text = "Kw";
            // 
            // lbCurrent
            // 
            lbCurrent.AutoSize = true;
            lbCurrent.Font = new Font("Segoe UI", 14F);
            lbCurrent.Location = new Point(171, 138);
            lbCurrent.Name = "lbCurrent";
            lbCurrent.Size = new Size(35, 38);
            lbCurrent.TabIndex = 7;
            lbCurrent.Text = "A";
            // 
            // blVoltage
            // 
            blVoltage.AutoSize = true;
            blVoltage.Font = new Font("Segoe UI", 14F);
            blVoltage.Location = new Point(171, 78);
            blVoltage.Name = "blVoltage";
            blVoltage.Size = new Size(59, 38);
            blVoltage.TabIndex = 6;
            blVoltage.Text = "Vac";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F);
            label5.Location = new Point(257, 140);
            label5.Name = "label5";
            label5.Size = new Size(52, 38);
            label5.TabIndex = 10;
            label5.Text = "Kw";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F);
            label4.Location = new Point(257, 80);
            label4.Name = "label4";
            label4.Size = new Size(52, 38);
            label4.TabIndex = 9;
            label4.Text = "Kw";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(lbTimeImp);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txImpCounter);
            groupBox1.Controls.Add(BtResetExpCounter);
            groupBox1.Controls.Add(lbTimeExp);
            groupBox1.Controls.Add(txExpCounter);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txImpPower);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txExpPower);
            groupBox1.Location = new Point(540, 40);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(343, 454);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Grid - Import / Export";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(15, 333);
            label8.Name = "label8";
            label8.Size = new Size(92, 25);
            label8.TabIndex = 22;
            label8.Text = "Started at:";
            // 
            // lbTimeImp
            // 
            lbTimeImp.AutoSize = true;
            lbTimeImp.Location = new Point(115, 333);
            lbTimeImp.Name = "lbTimeImp";
            lbTimeImp.Size = new Size(50, 25);
            lbTimeImp.TabIndex = 21;
            lbTimeImp.Text = "Time";
            // 
            // button3
            // 
            button3.Location = new Point(15, 384);
            button3.Name = "button3";
            button3.Size = new Size(67, 34);
            button3.TabIndex = 20;
            button3.Text = "Reset";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14F);
            label7.Location = new Point(257, 390);
            label7.Name = "label7";
            label7.Size = new Size(52, 38);
            label7.TabIndex = 19;
            label7.Text = "Kw";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F);
            label6.Location = new Point(257, 274);
            label6.Name = "label6";
            label6.Size = new Size(52, 38);
            label6.TabIndex = 18;
            label6.Text = "Kw";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 217);
            label3.Name = "label3";
            label3.Size = new Size(92, 25);
            label3.TabIndex = 17;
            label3.Text = "Started at:";
            // 
            // txImpCounter
            // 
            txImpCounter.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            txImpCounter.ForeColor = Color.Red;
            txImpCounter.Location = new Point(100, 384);
            txImpCounter.Name = "txImpCounter";
            txImpCounter.Size = new Size(150, 50);
            txImpCounter.TabIndex = 16;
            txImpCounter.TextAlign = HorizontalAlignment.Right;
            // 
            // BtResetExpCounter
            // 
            BtResetExpCounter.Location = new Point(15, 268);
            BtResetExpCounter.Name = "BtResetExpCounter";
            BtResetExpCounter.Size = new Size(67, 34);
            BtResetExpCounter.TabIndex = 15;
            BtResetExpCounter.Text = "Reset";
            BtResetExpCounter.UseVisualStyleBackColor = true;
            BtResetExpCounter.Click += BtResetExpCounter_Click;
            // 
            // lbTimeExp
            // 
            lbTimeExp.AutoSize = true;
            lbTimeExp.Location = new Point(115, 217);
            lbTimeExp.Name = "lbTimeExp";
            lbTimeExp.Size = new Size(50, 25);
            lbTimeExp.TabIndex = 14;
            lbTimeExp.Text = "Time";
            // 
            // txExpCounter
            // 
            txExpCounter.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            txExpCounter.ForeColor = Color.Green;
            txExpCounter.Location = new Point(100, 268);
            txExpCounter.Name = "txExpCounter";
            txExpCounter.Size = new Size(150, 50);
            txExpCounter.TabIndex = 13;
            txExpCounter.TextAlign = HorizontalAlignment.Right;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 134);
            label2.Name = "label2";
            label2.Size = new Size(63, 25);
            label2.TabIndex = 12;
            label2.Text = "Export";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 74);
            label1.Name = "label1";
            label1.Size = new Size(67, 25);
            label1.TabIndex = 11;
            label1.Text = "Import";
            // 
            // button1
            // 
            button1.Location = new Point(771, 653);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 8;
            button1.Text = "Run";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(639, 653);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 9;
            button2.Text = "Stop";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(button4);
            groupBox3.Controls.Add(label15);
            groupBox3.Controls.Add(comboBox1);
            groupBox3.Controls.Add(lbOnOff);
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(label14);
            groupBox3.Controls.Add(txTempLow);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(label12);
            groupBox3.Controls.Add(txSetpoint);
            groupBox3.Controls.Add(txTempHigh);
            groupBox3.Location = new Point(40, 360);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(489, 327);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "Immersion Heater";
            // 
            // lbOnOff
            // 
            lbOnOff.AutoSize = true;
            lbOnOff.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbOnOff.Location = new Point(243, 150);
            lbOnOff.Name = "lbOnOff";
            lbOnOff.Size = new Size(110, 65);
            lbOnOff.TabIndex = 23;
            lbOnOff.Text = "OFF";
            lbOnOff.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(15, 228);
            label13.Name = "label13";
            label13.Size = new Size(151, 25);
            label13.TabIndex = 22;
            label13.Text = "Temperature Low:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 14F);
            label14.Location = new Point(171, 270);
            label14.Name = "label14";
            label14.Size = new Size(45, 38);
            label14.TabIndex = 21;
            label14.Text = "°C";
            // 
            // txTempLow
            // 
            txTempLow.Font = new Font("Segoe UI", 16F);
            txTempLow.Location = new Point(15, 258);
            txTempLow.Name = "txTempLow";
            txTempLow.Size = new Size(150, 50);
            txTempLow.TabIndex = 20;
            txTempLow.TextAlign = HorizontalAlignment.Right;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(15, 135);
            label10.Name = "label10";
            label10.Size = new Size(157, 25);
            label10.TabIndex = 19;
            label10.Text = "Temperature High:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(15, 44);
            label9.Name = "label9";
            label9.Size = new Size(144, 25);
            label9.TabIndex = 18;
            label9.Text = "Current setpoint:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 14F);
            label11.Location = new Point(171, 177);
            label11.Name = "label11";
            label11.Size = new Size(45, 38);
            label11.TabIndex = 7;
            label11.Text = "°C";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 14F);
            label12.Location = new Point(171, 78);
            label12.Name = "label12";
            label12.Size = new Size(45, 38);
            label12.TabIndex = 6;
            label12.Text = "°C";
            // 
            // txSetpoint
            // 
            txSetpoint.Font = new Font("Segoe UI", 16F);
            txSetpoint.Location = new Point(15, 72);
            txSetpoint.Name = "txSetpoint";
            txSetpoint.Size = new Size(150, 50);
            txSetpoint.TabIndex = 0;
            txSetpoint.TextAlign = HorizontalAlignment.Right;
            // 
            // txTempHigh
            // 
            txTempHigh.Font = new Font("Segoe UI", 16F);
            txTempHigh.Location = new Point(15, 165);
            txTempHigh.Name = "txTempHigh";
            txTempHigh.Size = new Size(150, 50);
            txTempHigh.TabIndex = 1;
            txTempHigh.TextAlign = HorizontalAlignment.Right;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "15", "20", "25", "30", "35", "40", "45", "50", "55", "60", "65", "70" });
            comboBox1.Location = new Point(243, 72);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(73, 33);
            comboBox1.TabIndex = 11;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(243, 44);
            label15.Name = "label15";
            label15.Size = new Size(83, 25);
            label15.TabIndex = 24;
            label15.Text = "Setpoint:";
            // 
            // button4
            // 
            button4.Location = new Point(332, 70);
            button4.Name = "button4";
            button4.Size = new Size(112, 34);
            button4.TabIndex = 25;
            button4.Text = "Set";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(929, 712);
            Controls.Add(groupBox3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Name = "Form1";
            Text = "DDSU Monitor";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TextBox txVoltage;
        private TextBox txCurrent;
        private TextBox txPower;
        private TextBox txFrequency;
        private TextBox txImpPower;
        private TextBox txExpPower;
        private GroupBox groupBox2;
        private Label blVoltage;
        private Label label4;
        private Label lbFrequency;
        private Label lbPower;
        private Label lbCurrent;
        private Label label5;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        private TextBox txExpCounter;
        private Label lbTimeExp;
        private Button BtResetExpCounter;
        private TextBox txImpCounter;
        private Label label3;
        private Button button3;
        private Label label7;
        private Label label6;
        private Label label8;
        private Label lbTimeImp;
        private GroupBox groupBox3;
        private Label label10;
        private Label label9;
        private Label label11;
        private Label label12;
        private TextBox txSetpoint;
        private TextBox txTempHigh;
        private Label label13;
        private Label label14;
        private TextBox txTempLow;
        private Label lbOnOff;
        private ComboBox comboBox1;
        private BindingSource bindingSource1;
        private Button button4;
        private Label label15;
    }
}

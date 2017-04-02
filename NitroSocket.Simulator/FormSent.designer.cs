namespace NitroSocket.Simulator
{
    partial class frmNitroSocketSimulator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        */
        
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.t1 = new System.Windows.Forms.TextBox();
            this.ta = new System.Windows.Forms.ListBox();
            this.m_tbServerAddress = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonAddLine = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.comboBoxProtocol = new System.Windows.Forms.ComboBox();
            this.btnReadAndProcess = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.interval = new System.Windows.Forms.NumericUpDown();
            this.numberFromFile = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxAuto = new System.Windows.Forms.CheckBox();
            this.numericUpDownLoop = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.labelTotalProbe = new System.Windows.Forms.Label();
            this.listBoxACK = new System.Windows.Forms.ListBox();
            this.checkBoxACK = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.interval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberFromFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLoop)).BeginInit();
            this.SuspendLayout();
            // 
            // t1
            // 
            this.t1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t1.Location = new System.Drawing.Point(12, 38);
            this.t1.Name = "t1";
            this.t1.Size = new System.Drawing.Size(605, 20);
            this.t1.TabIndex = 3;
            this.t1.Text = "NITRO0101234567890";
            // 
            // ta
            // 
            this.ta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ta.IntegralHeight = false;
            this.ta.Location = new System.Drawing.Point(12, 98);
            this.ta.Name = "ta";
            this.ta.ScrollAlwaysVisible = true;
            this.ta.Size = new System.Drawing.Size(604, 151);
            this.ta.TabIndex = 14;
            // 
            // m_tbServerAddress
            // 
            this.m_tbServerAddress.Location = new System.Drawing.Point(71, 11);
            this.m_tbServerAddress.Name = "m_tbServerAddress";
            this.m_tbServerAddress.Size = new System.Drawing.Size(86, 20);
            this.m_tbServerAddress.TabIndex = 1;
            this.m_tbServerAddress.Text = "10.152.28.85";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Location = new System.Drawing.Point(473, 10);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(71, 23);
            this.buttonConnect.TabIndex = 17;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(192, 11);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(71, 20);
            this.textBoxPort.TabIndex = 2;
            this.textBoxPort.Text = "11000";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "IP Server";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(164, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "Port ";
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Enabled = false;
            this.buttonSend.Location = new System.Drawing.Point(541, 355);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 21;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonAddLine
            // 
            this.buttonAddLine.Enabled = false;
            this.buttonAddLine.Location = new System.Drawing.Point(15, 69);
            this.buttonAddLine.Name = "buttonAddLine";
            this.buttonAddLine.Size = new System.Drawing.Size(75, 23);
            this.buttonAddLine.TabIndex = 19;
            this.buttonAddLine.Text = "Add";
            this.buttonAddLine.Click += new System.EventHandler(this.buttonAddLine_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Enabled = false;
            this.buttonClear.Location = new System.Drawing.Point(108, 69);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 22;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.Filter = "Text File|*.txt";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpen.Location = new System.Drawing.Point(473, 69);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(71, 23);
            this.buttonOpen.TabIndex = 15;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(548, 69);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(72, 22);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisconnect.Location = new System.Drawing.Point(548, 10);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(72, 22);
            this.buttonDisconnect.TabIndex = 18;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // comboBoxProtocol
            // 
            this.comboBoxProtocol.FormattingEnabled = true;
            this.comboBoxProtocol.Items.AddRange(new object[] {
            "TCP",
            "UDP"});
            this.comboBoxProtocol.Location = new System.Drawing.Point(269, 11);
            this.comboBoxProtocol.Name = "comboBoxProtocol";
            this.comboBoxProtocol.Size = new System.Drawing.Size(80, 21);
            this.comboBoxProtocol.TabIndex = 24;
            this.comboBoxProtocol.Text = "TCP";
            this.comboBoxProtocol.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnReadAndProcess
            // 
            this.btnReadAndProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadAndProcess.Location = new System.Drawing.Point(482, 387);
            this.btnReadAndProcess.Name = "btnReadAndProcess";
            this.btnReadAndProcess.Size = new System.Drawing.Size(134, 23);
            this.btnReadAndProcess.TabIndex = 25;
            this.btnReadAndProcess.Text = "Read And Process File";
            this.btnReadAndProcess.UseVisualStyleBackColor = true;
            this.btnReadAndProcess.Click += new System.EventHandler(this.btnReadAndProcess_Click);
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(124, 348);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(64, 13);
            this.label23.TabIndex = 26;
            this.label23.Text = "Interval (ms)";
            // 
            // interval
            // 
            this.interval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.interval.Location = new System.Drawing.Point(117, 366);
            this.interval.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.interval.Name = "interval";
            this.interval.Size = new System.Drawing.Size(76, 20);
            this.interval.TabIndex = 27;
            // 
            // numberFromFile
            // 
            this.numberFromFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numberFromFile.Location = new System.Drawing.Point(20, 366);
            this.numberFromFile.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numberFromFile.Name = "numberFromFile";
            this.numberFromFile.Size = new System.Drawing.Size(65, 20);
            this.numberFromFile.TabIndex = 28;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(18, 397);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(72, 13);
            this.label25.TabIndex = 29;
            this.label25.Text = "0 = All Probes";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 348);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Probes Number";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 394);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "0 = No interval";
            // 
            // checkBoxAuto
            // 
            this.checkBoxAuto.AutoSize = true;
            this.checkBoxAuto.Location = new System.Drawing.Point(355, 14);
            this.checkBoxAuto.Name = "checkBoxAuto";
            this.checkBoxAuto.Size = new System.Drawing.Size(91, 17);
            this.checkBoxAuto.TabIndex = 32;
            this.checkBoxAuto.Text = "Auto Connect";
            this.checkBoxAuto.UseVisualStyleBackColor = true;
            this.checkBoxAuto.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // numericUpDownLoop
            // 
            this.numericUpDownLoop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownLoop.Location = new System.Drawing.Point(228, 366);
            this.numericUpDownLoop.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownLoop.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLoop.Name = "numericUpDownLoop";
            this.numericUpDownLoop.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownLoop.TabIndex = 34;
            this.numericUpDownLoop.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(235, 348);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Loop";
            // 
            // labelTotalProbe
            // 
            this.labelTotalProbe.AutoSize = true;
            this.labelTotalProbe.Location = new System.Drawing.Point(323, 368);
            this.labelTotalProbe.Name = "labelTotalProbe";
            this.labelTotalProbe.Size = new System.Drawing.Size(104, 13);
            this.labelTotalProbe.TabIndex = 35;
            this.labelTotalProbe.Text = "Total Probes Sent: 0";
            // 
            // listBoxACK
            // 
            this.listBoxACK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxACK.IntegralHeight = false;
            this.listBoxACK.Location = new System.Drawing.Point(12, 272);
            this.listBoxACK.Name = "listBoxACK";
            this.listBoxACK.ScrollAlwaysVisible = true;
            this.listBoxACK.Size = new System.Drawing.Size(604, 69);
            this.listBoxACK.TabIndex = 37;
            // 
            // checkBoxACK
            // 
            this.checkBoxACK.AutoSize = true;
            this.checkBoxACK.Enabled = false;
            this.checkBoxACK.Location = new System.Drawing.Point(12, 255);
            this.checkBoxACK.Name = "checkBoxACK";
            this.checkBoxACK.Size = new System.Drawing.Size(86, 17);
            this.checkBoxACK.TabIndex = 38;
            this.checkBoxACK.Text = "Waiting ACK";
            this.checkBoxACK.UseVisualStyleBackColor = true;
            this.checkBoxACK.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // frmNitroSocketSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 416);
            this.Controls.Add(this.checkBoxACK);
            this.Controls.Add(this.listBoxACK);
            this.Controls.Add(this.labelTotalProbe);
            this.Controls.Add(this.numericUpDownLoop);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxAuto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.numberFromFile);
            this.Controls.Add(this.interval);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.btnReadAndProcess);
            this.Controls.Add(this.comboBoxProtocol);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonAddLine);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.t1);
            this.Controls.Add(this.ta);
            this.Controls.Add(this.m_tbServerAddress);
            this.Name = "frmNitroSocketSimulator";
            this.Text = "NitroSocket Send Simulator";
            this.Load += new System.EventHandler(this.FormClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.interval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberFromFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLoop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox t1;
        private System.Windows.Forms.ListBox ta;
        private System.Windows.Forms.TextBox m_tbServerAddress;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonAddLine;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.ComboBox comboBoxProtocol;
        private System.Windows.Forms.Button btnReadAndProcess;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.NumericUpDown interval;
        private System.Windows.Forms.NumericUpDown numberFromFile;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxAuto;
        private System.Windows.Forms.NumericUpDown numericUpDownLoop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelTotalProbe;
        private System.Windows.Forms.ListBox listBoxACK;
        private System.Windows.Forms.CheckBox checkBoxACK;
    }
}

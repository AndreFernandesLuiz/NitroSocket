namespace NitroSocket.SimulatorReceive
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
            this.lstReceive = new System.Windows.Forms.ListBox();
            this.m_tbServerAddress = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.comboBoxProtocol = new System.Windows.Forms.ComboBox();
            this.checkBoxACK = new System.Windows.Forms.CheckBox();
            this.txtAck = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lstReceive
            // 
            this.lstReceive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstReceive.IntegralHeight = false;
            this.lstReceive.Location = new System.Drawing.Point(3, 39);
            this.lstReceive.Name = "lstReceive";
            this.lstReceive.ScrollAlwaysVisible = true;
            this.lstReceive.Size = new System.Drawing.Size(601, 131);
            this.lstReceive.TabIndex = 14;
            // 
            // m_tbServerAddress
            // 
            this.m_tbServerAddress.Enabled = false;
            this.m_tbServerAddress.Location = new System.Drawing.Point(71, 11);
            this.m_tbServerAddress.Name = "m_tbServerAddress";
            this.m_tbServerAddress.Size = new System.Drawing.Size(86, 20);
            this.m_tbServerAddress.TabIndex = 1;
            this.m_tbServerAddress.Text = "10.152.28.85";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Location = new System.Drawing.Point(458, 11);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(71, 23);
            this.buttonConnect.TabIndex = 17;
            this.buttonConnect.Text = "Listen";
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
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "IP Client";
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
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.Filter = "Text File|*.txt";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(532, 168);
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
            this.buttonDisconnect.Location = new System.Drawing.Point(533, 11);
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
            "UDP",
            "SNMP"});
            this.comboBoxProtocol.Location = new System.Drawing.Point(269, 11);
            this.comboBoxProtocol.Name = "comboBoxProtocol";
            this.comboBoxProtocol.Size = new System.Drawing.Size(80, 21);
            this.comboBoxProtocol.TabIndex = 24;
            this.comboBoxProtocol.Text = "TCP";
            // 
            // checkBoxACK
            // 
            this.checkBoxACK.AutoSize = true;
            this.checkBoxACK.Location = new System.Drawing.Point(8, 181);
            this.checkBoxACK.Name = "checkBoxACK";
            this.checkBoxACK.Size = new System.Drawing.Size(75, 17);
            this.checkBoxACK.TabIndex = 38;
            this.checkBoxACK.Text = "Send ACK";
            this.checkBoxACK.UseVisualStyleBackColor = true;
            this.checkBoxACK.CheckedChanged += new System.EventHandler(this.checkBoxACK_CheckedChanged);
            // 
            // txtAck
            // 
            this.txtAck.Location = new System.Drawing.Point(8, 205);
            this.txtAck.Name = "txtAck";
            this.txtAck.Size = new System.Drawing.Size(596, 20);
            this.txtAck.TabIndex = 39;
            // 
            // frmNitroSocketSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 268);
            this.Controls.Add(this.txtAck);
            this.Controls.Add(this.checkBoxACK);
            this.Controls.Add(this.comboBoxProtocol);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.lstReceive);
            this.Controls.Add(this.m_tbServerAddress);
            this.Name = "frmNitroSocketSimulator";
            this.Text = "NitroSocket Receive Simulator";
            this.Load += new System.EventHandler(this.FormClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lstReceive;
        private System.Windows.Forms.TextBox m_tbServerAddress;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.ComboBox comboBoxProtocol;
        private System.Windows.Forms.CheckBox checkBoxACK;
        private System.Windows.Forms.TextBox txtAck;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Threading;
using System.Net;

namespace NitroSocket.Simulator
{

    public delegate void mudaTexto(ListBox lista, String texto);


    public partial class frmNitroSocketSimulator : Form
    {
        public StreamReader Reader;
        public StreamWriter Writer;

        public Socket client;
        static object BigLock = new object();
        Boolean waitAck = false;


        public void changeText(ListBox lista, String text)
        {
            if (lista.InvokeRequired)
            {
                mudaTexto a = new mudaTexto(changeText);
                lista.Invoke(a, new object[] { lista, text });
            }
            else
            {
                lista.Items.Add(text);
            }
        }

        public frmNitroSocketSimulator()
        {
            InitializeComponent();
        }


        public void DisconnectCallback(IAsyncResult ar)
        {
            try
            {
                // Get the socket that handles the client request.
                Socket client = (Socket)ar.AsyncState;
                Reader.Close();
                Writer.Close();
                if (!checkBoxAuto.Checked)
                {
                    MessageBox.Show("Desconectado...");
                }
                client.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private bool Connect()
        {
            if ((comboBoxProtocol.Text != "TCP" | client != null) && (!checkBoxAuto.Checked)) return true;
            int Port = 0;
                try
                {
                    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    int.TryParse(textBoxPort.Text, out Port);
                    client.Connect(m_tbServerAddress.Text, Port);
                    MessageBox.Show("Conectando...");

                    Reader = new StreamReader(new NetworkStream(client, false));
                    Writer = new StreamWriter(new NetworkStream(client, true));
                    // new Thread(ClientLoop).Start();
                    if (!checkBoxAuto.Checked)
                    {
                        MessageBox.Show("Connected!");
                        buttonAddLine.Enabled = true;
                        buttonOpen.Enabled = true;
                        checkBoxACK.Enabled = true;
                    }
                    return true;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Connect Fail :" + ee.ToString());
                    return false;
                }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }


         public string GetMyIpAddress()
        {
            String strHostName = Dns.GetHostName();
            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostByName(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[0].ToString();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            m_tbServerAddress.Text = GetMyIpAddress();
        }

        private void SendProbe(string header, string probe)
        {
            if (comboBoxProtocol.Text.Equals("UDP"))
            {
                UdpClient client = new UdpClient();
                int Port = 0;
                int.TryParse(textBoxPort.Text, out Port);
                client.Connect(m_tbServerAddress.Text, Port);
                Byte[] bytCommand = Encoding.ASCII.GetBytes(probe);
                int pRet = client.Send(bytCommand, bytCommand.Length);
            }
            else
            {
                Writer.Write(header);
                Writer.Write(probe);
                Writer.Flush();
            }
            if (interval.Value > 0)
            {
                Thread.Sleep((int)(interval.Value));
            }
            Application.DoEvents();
        }

        private void btnReadAndProcess_Click(object sender, EventArgs e)
        {
            if (!Connect())
            {
                return;
            }
            int probesToSend = (int)numberFromFile.Value;
            int probesSent = 0;
            Stream myStream = null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        StreamReader reader = new StreamReader(myStream);
                        while (!reader.EndOfStream & (probesToSend == 0 | probesSent < probesToSend))
                        {
                            String header = "";
                            SendProbe(header, reader.ReadLine());
                            probesSent++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
                MessageBox.Show("Finished...");
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!Connect())
            {
                MessageBox.Show("Erro no Send - Connect return FALSE!");
                return;
            }
            int probesToSend = (int)numberFromFile.Value;
            int probesSent = 0;
            int probesSentTotal = 0;
            try
            {
                for (int x = 1; x <= numericUpDownLoop.Value; x++)
                {
                    probesSent = 0;                    
                    for (int i = 0; i < ta.Items.Count; i++)
                    {
                        if (checkBoxAuto.Checked)
                        {
                            Connect();
                        }

                        System.Text.Encoding ascii = System.Text.Encoding.ASCII;

                        String msg = ta.Items[i].ToString();
                        string header = "";                       
                        SendProbe(header, ta.Items[i].ToString());
                        
                        probesSent++;
                        probesSentTotal++;
                        labelTotalProbe.Text = "Total Probes Sent: ";
                        Application.DoEvents();
                        if (checkBoxAuto.Checked)
                        {
                            Disconnect();
                        }                        
                        if (probesToSend > 0 & probesSent >= probesToSend)
                        {
                            MessageBox.Show("Send OK.");
                            return;
                        }

                    }
                }
                
            }
            catch (Exception ex)
            {
                if (client.Connected == false)
                {
                    Disconnect();
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
            MessageBox.Show("Send ok.");
        }

        private void buttonAddLine_Click(object sender, EventArgs e)
        {
            ta.Items.Add(t1.Text);
            buttonSend.Enabled = true;
            buttonClear.Enabled = true;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ta.Items.Clear();
            buttonSend.Enabled = false;
            buttonClear.Enabled = false;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        StreamReader reader = new StreamReader(myStream);
                        while (!reader.EndOfStream)
                        {
                            ta.Items.Add(reader.ReadLine());
                        }
                        buttonSend.Enabled = true;
                        buttonClear.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.FilterIndex = 1;
            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = sfd.OpenFile()) != null)
                {
                    StreamWriter writer = new StreamWriter(myStream);
                    for (int i = 0; i < ta.Items.Count; i++)
                    {
                        writer.WriteLine(ta.Items[i].ToString());
                    }
                    writer.Flush();
                    writer.Close();
                    // Code to write the stream goes here.
                    myStream.Close();
                }
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void Disconnect()
        {
            try
            {
                client.Disconnect(false);
                Reader.Close();
                Writer.Close();
                client.Close();
                buttonAddLine.Enabled = false;
                buttonOpen.Enabled = false;
                buttonClear.Enabled = false;
                checkBoxACK.Enabled = false;
                //buttonSend.Enabled = false;
                //ta.Items.Clear();
                client = null;
                if (!checkBoxAuto.Checked)
                {
                    MessageBox.Show("Disconnected");
                }
            }
            catch
            {
                MessageBox.Show("Disconnected with problem");

            }
        }

         private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProtocol.Text.Equals("TCP"))
            {
                buttonAddLine.Enabled = false;
                buttonOpen.Enabled = false;
                buttonDisconnect.Enabled = true;
                buttonConnect.Enabled = true;

            }
            else
            {
                buttonAddLine.Enabled = true;
                buttonOpen.Enabled = true;
                buttonDisconnect.Enabled = false;
                buttonConnect.Enabled = false;

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAuto.Checked)
            {
                buttonAddLine.Enabled = true;
                buttonOpen.Enabled = true;
                buttonDisconnect.Enabled = false;
                buttonConnect.Enabled = false;
                checkBoxACK.Enabled = false;
            }
            else
            {
                buttonAddLine.Enabled = false;
                buttonOpen.Enabled = false;
                buttonDisconnect.Enabled = true;
                checkBoxACK.Enabled = true;
                buttonConnect.Enabled = true;
            }

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if ((checkBoxACK.Checked) && (Reader != null))
            {
                listBoxACK.Items.Clear();
                waitAck = true;
                Thread threadSocket = new Thread(WaitAck);
                threadSocket.Start();
            }
            else
            {
                waitAck = false;
            }
        }

        private void WaitAck()
        {
            char[] buffer = new char[1024];
            string ack = string.Empty;
            int readCount = 0;
            while (waitAck)
            {
                //readCount = Reader.Read();                
                readCount = client.Available;
                if (readCount > 0 )
                {
                    Reader.Read(buffer, 0, readCount);
                    ack = new string(buffer, 0, readCount);
                    //ack = Reader.ReadLine();
                
                    if (!string.IsNullOrEmpty(ack))
                    {
                        SetListBox(frmNitroSocketSimulator.ActiveForm,ack);
                    }   
                }
                Application.DoEvents();
                Thread.Sleep(1000);
                
            }
        
        }




        delegate void SetListBoxDelegate(object sender, string e);

        void SetListBox(object sender, string ack)
        {

            if (this.InvokeRequired)
            {

                this.Invoke(new SetListBoxDelegate(SetListBox), sender, ack);

            }

            else
            {
                listBoxACK.Items.Add(ack);
            }

        }

        #region Copy
        public static int Copy(byte[] src, int srcIndex, byte[] dst, int dstIndex, int count)
        {
            if (src == null || srcIndex < 0 ||
                dst == null || dstIndex < 0 || count < 0)
            {
                throw new System.ArgumentException();
            }

            int srcLen = src.Length;
            int dstLen = dst.Length;
            if (srcLen - srcIndex < count || dstLen - dstIndex < count)
            {
                throw new System.ArgumentException();
            }

            int s = srcIndex;
            int d = dstIndex;

            for (int i = 0; i < count; i++)
            {
                dst[d] = src[s];
                d++;
                s++;
            }

            return d;
        }
        #endregion // Copy

        #region ByteToString
        public static string ByteToString(byte[] bbuff, int offset, int count)
        {
            char[] cbuff = new char[count];

            for (int i = 0; i < count; i++)
            {
                cbuff[i] = (char)bbuff[i + offset];
            }
            return new string(cbuff);
        }
        #endregion // ByteToString

     }
}





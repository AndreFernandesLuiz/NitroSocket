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
using NitroSocket.Connection;
using Library.Logging;
using Library.PerformaceMonitor;

namespace NitroSocket.SimulatorReceive

{

    
    public partial class frmNitroSocketSimulator : Form
    {
        public StreamReader Reader;
        public StreamWriter Writer;

        public Socket client;
        static object BigLock = new object();
        Boolean stop = false;

        IConnection messsageConnectionListener;
        IMessageCutter messageCutterObject = new SampleMessageCutter();

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
                MessageBox.Show("Disconnect...");
                client.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

   
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void Connect()
        {
            messsageConnectionListener =  CreateInputConnection(comboBoxProtocol.Text);
            Thread threadSocket = new Thread(PortListening);
            threadSocket.Start();

        }

        private IConnection CreateInputConnection(string inputConnectionName)
        {
            int Port = 0;
            int.TryParse(textBoxPort.Text, out Port);
            if (inputConnectionName == "TCP")
            {
                return new TCPConnection(Port, 1500);
            }
            else if (inputConnectionName == "UDP")
            {
                return new UDPConnection(Port, 1500);
            }
            else if (inputConnectionName == "SNMP")
            {
                //Boolean filter = false;
                
                //return new SNMPConnection(filter);
            }

            return null;
        }

        /// <summary>
        /// start port listening loop
        /// main function to process messages from an specific message processor
        /// tip: #1ars
        /// </summary>
        public void PortListening()
        {
            Log.Information("NitroSocket Client thread started.");
            List<object> messages = new List<object>();
            //PerformaceScore firstStepPerformanceCounter = new PerformaceScore("NitroSocket");
            try
            {
                messsageConnectionListener.Start();
                long messagesCounter = 0;
                while (!stop)
                {
                    //VerifyConfigurationChanged();
                    try
                    {
                        messages.Clear();
                        try
                        {
                            messages.AddRange(messsageConnectionListener.Receive(messageCutterObject));
                        }
                        catch (Exception except)
                        {
                            Log.Error(2, "Message receive(cutter) error: " + except.ToString());
                        }

                        if (messages != null && messages.Count > 0)
                        {
                            foreach (object message in messages)
                            {
                                messagesCounter++;
              //                  firstStepPerformanceCounter.startProc();
                                if (message != null) // Any test in message, like type, size, etc
                                {
                                    SetListBox(frmNitroSocketSimulator.ActiveForm, message.ToString());
                                    Log.Information("NitroSocket R;" + message);
                                    if (checkBoxACK.Enabled)
                                    {
                                        messageCutterObject.hasAck = true;
                                        if (String.IsNullOrEmpty(txtAck.Text))
                                            messageCutterObject.messagesAck.Add("Received: " + message);
                                        else
                                            messageCutterObject.messagesAck.Add(txtAck.Text);

                                        messsageConnectionListener.Send(messageCutterObject.messagesAck);
                                        messageCutterObject.messagesAck.Clear();
                                    }

                                    else
                                    {
                                        messageCutterObject.hasAck = false;
                                        messageCutterObject.messagesAck.Clear();

                                    }

                                    //                  firstStepPerformanceCounter.endProc();
                                    //                    firstStepPerformanceCounter.MeasureBasicScore();
                                }
                                else
                                {
                                    Log.BusinessError(1001, "Error in message received");
                                    
                                }
                            }
                        }
                    }
                    catch (Exception except)
                    {
                        Log.Error(3, "StartPortListeningLoop Error: " + except.ToString());
                    }
                    Thread.Sleep(1);
                }
                messsageConnectionListener.Stop();
            }
            catch (Exception except)
            {
                Log.Error(4, "Start Problem Error: " + except.Message  );
                Stop();
            }
        }


        public void Stop()
        {
            stop = true;
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
     


        private void buttonClear_Click(object sender, EventArgs e)
        {
            lstReceive.Items.Clear();
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
                    for (int i = 0; i < lstReceive.Items.Count; i++)
                    {
                        writer.WriteLine(lstReceive.Items[i].ToString());
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
                stop = false;
                client.Disconnect(false);
                Reader.Close();
                Writer.Close();
                client.Close();
                checkBoxACK.Enabled = false;
                //buttonSend.Enabled = false;
                //ta.Items.Clear();
                client = null;
                MessageBox.Show("Disconnected");
            }
            catch
            {
                MessageBox.Show("Disconnected with problem");

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
                lstReceive.Items.Add(ack);
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

        private void checkBoxACK_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}





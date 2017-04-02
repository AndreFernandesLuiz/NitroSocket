using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Configuration;
using Library.Logging;

namespace NitroSocket.Connection
{
    public class TCPConnection : BaseConnection
    {

       // Thread signal.
        public  ManualResetEvent allDone = new ManualResetEvent(false);

        private Boolean continueAcceptLoop = true;

        private int bufferSize;
        private int port;
        private List<StateObject> stateReceive = new List<StateObject>();
        private List<StateObject> stateReceived = new List<StateObject>();

        // State object for reading client data asynchronously
        internal class StateObject
        {
            // Client  socket.
            internal Socket workSocket = null;
            // Receive buffer.
            internal byte[] buffer;

            internal Semaphore semaphore = new Semaphore(1, 1);

            internal StateObject(int bufferSize)
            {
                buffer = new byte[bufferSize];
            }
            // Received data string.
            internal StringBuilder sb = new StringBuilder();
        }

        public TCPConnection(int portParam, int bufferSizeParam)
        {
            bufferSize = bufferSizeParam;
            port = portParam;
            stateReceive = new List<StateObject>();
        }

        #region BaseMessageConnection override Members

        /*
        public override string GetBuffer()
        {
            string buffer = state.sb.ToString();
            state.sb.Remove(0, state.sb.Length);
            _hasBuffer = false;
            canWriteBuffer = true;
            return buffer;

        }
         */

        public override List<object> Receive(IMessageCutter messageCutter)
        {
            List<object> messagesList = new List<object>();
            if (stateReceive != null)
            {
               // try
                //{
                
                    stateReceived.Clear();
                    stateReceived.AddRange(stateReceive);
                    for (int i = 0; i < stateReceived.Count; i++)
                    {
                        try
                        {
                            #region receive_handler
                            if (stateReceived[i] != null)
                            {
                                if (stateReceived[i].sb.Length > 0)
                                {
                                    Log.Trace("TCPReceive handler.semaphore.WaitOne");
                                    stateReceived[i].semaphore.WaitOne();
                                    try
                                    {
                                        try
                                        {
                                            messagesList.AddRange(messageCutter.Cut(ref stateReceived[i].sb));
                                        }
                                        catch (Exception e)
                                        {                                        
                                        Log.Error(1017, String.Format("Problem with TCP messageCutter, Original message was discarded: {0} - Error: {1}", stateReceived[i].sb, e.ToString()));
                                        }

                                        if (messageCutter.hasAck)
                                        {
                                            if (messageCutter.messagesAck != null)
                                            {
                                                for (int z = 0; z <= messageCutter.messagesAck.Count - 1; z++)
                                                {
                                                    Send(stateReceived[i].workSocket, messageCutter.messagesAck[z].ToString());
                                                }
                                                messageCutter.messagesAck.Clear();
                                            }
                                            messageCutter.hasAck = false;
                                        }
                                        Log.Trace("TCPReceive message received: " + stateReceived[i].sb.ToString());
                                        _hasBuffer = false;
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(1017, String.Format("Problem with TCP messageCutter, Message Format maybe wrong! Original message was discarded: {0} - Error: {1}", stateReceived[i].sb, e.ToString()));
                                        stateReceived[i].sb.Remove(0, stateReceived[i].sb.Length);
                                    }
                                    stateReceived[i].semaphore.Release();
                                    Log.Trace("TCPReceive handler.semaphore.Release");
                                }
                                else
                                {
                                    Log.Trace("Zero Bytes in TCP handler to be read!");
                                }
                            }
                            else
                            {
                                try
                                {
                                    stateReceive.Remove(stateReceived[i]);
                                }
                                catch (Exception e)
                                {
                                    Log.Error(1017, String.Format("Problem with TCP messageCutter, stateReceive Remove : {0}", e.ToString()));
                                }

                            }
                        }
                            #endregion
                        catch (Exception e)
                        {
                            Log.Error(1017, String.Format("Problem with TCP Receive MessageCutter : {0}", e.ToString()));
                        }


                    }                   
                //}                
                //catch (Exception except)
                //{
                  //  Log.Error("TCPConnection: Problem with receive method" + except.ToString());
                   // throw new UnexpectedErrorException("TCPConnection: Problem with receive method" + except.ToString());
                //}
            }
            else
            {
                Log.Trace("No sockts to be read!");
            }
            
            return messagesList;   
        }

        private void StartConnectLoop()
        {

            try
            {
                using (Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    listener.Bind(new IPEndPoint(IPAddress.Any, port ));
                    listener.Listen(0);
                    Log.Information(string.Format("TCP Socket listenning at Port {0}", port));

                    while (continueAcceptLoop)
                    {
                        // Set the event to nonsignaled state.
                        allDone.Reset();

                        // Start an asynchronous socket to listen for connections.
                        Log.Trace("Waiting for a new connection...");

                        listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                        // Wait until a connection is made before continuing.
                        allDone.WaitOne();
                    }
                    Log.Information("listener.Shutdown...");
                    listener.Close();
                }

            }
            catch (Exception e)
            {
                Log.Error(1018, string.Format("Listener Error: {0}", e.ToString()));
            }
            
        }



        private void DisconnectCallbackSock(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;

                Socket handler = state.workSocket;
                Log.Information("DisconnectCallback Socket: " + handler.LocalEndPoint.ToString() + "  " + handler.RemoteEndPoint.ToString());
                //handler.Close();
                stateReceive.Remove(state);

                handler.EndDisconnect(ar);

            }
            catch (Exception e)
            {
                Log.Warning(3003, string.Format("DisconnectCallback Socket Callback Error: {0}", e.Message, EventLogEntryType.Error));
            }
        }                   



        public override void Start()
        {
            continueAcceptLoop = true;
            Thread threadSocket = new Thread(StartConnectLoop);
            threadSocket.Start();
        }

        public override void Stop()
        {
            Log.Trace("Socket Stop Listening");
            continueAcceptLoop = false;
            allDone.Set();
        }

        #endregion

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                if (ar != null)
                {
                    // Signal the main thread to continue.
                    allDone.Set();

                    // Get the socket that handles the client request.
                    Socket listener = (Socket)ar.AsyncState;
                    Socket handler;
                    try
                    {
                        handler = listener.EndAccept(ar);
                    }
                    catch (Exception e)
                    {
                        Log.Information("AcceptCallback: The Tcp conection was Shutdown. - Message:" + e.Message);
                        return;
                    }
                  
                    StateObject handler2 = new StateObject(bufferSize);
                    handler2.workSocket = handler;
                    lock (stateReceive)
                    {
                        stateReceive.Add(handler2);
                    }
                    Log.Information("New connection has accepted and ready: " + handler.LocalEndPoint.ToString() + "  " + handler.RemoteEndPoint.ToString() + " - Total Clients: " + stateReceive.Count.ToString());


                   //handler.BeginDisconnect(false, new AsyncCallback(DisconnectCallbackSock),  handler2);                    

                    handler.BeginReceive(handler2.buffer, 0, bufferSize, 0,
                        new AsyncCallback(ReadCallback), handler2);

                   // handler.BeginAccept(new AsyncCallback(AcceptCallback), listener);


//                    Log.Information("AcceptCallback: New Connection was Accept:");
                }
            }
            catch (Exception e)
            {
                Log.Error(1019,string.Format("Accept Callback Error: {0}", e.Message, EventLogEntryType.Error));
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.

            StateObject state = (StateObject)ar.AsyncState;


            Socket handler = state.workSocket;

            try
            {

                String content = String.Empty;



                // Read data from the client socket. 
                int bytesRead = handler.EndReceive(ar);

                Log.Trace("New connection has accepted and ready!");

                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.
                    Log.Trace("TCPConnection readcallbak state.semaphore.WaitOne");
                    state.semaphore.WaitOne();
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    state.semaphore.Release();
                    Log.Trace("TCPConnection readcallbak  state.semaphore.Release ");

                    // Check for end-of-file tag. If it is not there, read 
                    // more data.                   

                    // All the data has been read from the 
                    //content = state.sb.ToString();

                    // client. Display it on the console.
                    Log.Information(string.Format("Socket: {0} Read {1} bytes. Data : {2}, Bytes should be read {3}", handler.RemoteEndPoint.ToString(), state.sb.Length, state.sb.ToString().Replace('\0',(char)('\0' + 1)), bytesRead.ToString()));

                    Log.Information("SOCKETREADLOG", handler.LocalEndPoint.ToString() + "\"||\"" + handler.RemoteEndPoint.ToString() + "\"||\"" + Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    _hasBuffer = true;

                    // Not all data received. Get more.        
                    Thread.Sleep(1);
                    handler.BeginReceive(state.buffer, 0, bufferSize, 0, new AsyncCallback(ReadCallback), state);
                }
                else
                {

                    int waitTimes = 0;
                    while (state.sb.Length > 0 && waitTimes < 10)
                    {
                        Thread.Sleep(1);
                        waitTimes++;
                    }
                    if (handler.Connected)
                    {
                        handler.Close();
                        //Log.Information("ReadCallback Connection closed!");

                    }
                    /*
                    if (!handler.Connected)
                    {
                        stateReceive.Remove(state);
                        state.workSocket = null;
                        //Log.Information("Socket Client Removed");

                    }
                     */
                    state.workSocket = null;
                    lock (stateReceive)
                    {
                        if (stateReceive.Contains(state))
                            stateReceive.Remove(state);
                    }
                    Log.Information(string.Format("Socket Client Closed and Removed  - Total Clients: {0}", stateReceive.Count.ToString()));
                    //  allDone.Set();
                }
            }
            catch (InvalidOperationException se)
            {
                if (handler.Connected)
                {
                    handler.Close();
                }

                if (!handler.Connected)
                {
                    state.workSocket = null;
                    lock (stateReceive)
                    {
                        if (stateReceive.Contains(state))
                            stateReceive.Remove(state);
                    }
                }
                Log.Warning(3004, string.Format("Warning in Socket ReadCallbak, Client connection lost and removed : {0}  - Total Clients: {1}", se.Message, stateReceive.Count.ToString()));
                //    allDone.Set();

            }
            catch (SocketException se)
            {
                if (!handler.Connected)
                {
                    lock (stateReceive)
                    {
                        if (stateReceive.Contains(state))
                            stateReceive.Remove(state);
                    }
                    state.workSocket = null;                    
                }
                Log.Warning(3004, string.Format("Warning in Socket ReadCallbak, Client connection lost and removed : {0}  - Total Clients: {1}", se.Message, stateReceive.Count.ToString()));
                //  allDone.Set();
            }

            catch (Exception se)
            {
                Log.Warning(3005, string.Format("Warning in Socket ReadCallbak, Client connection may be lost : {0}  - Total Clients: {1}", se.Message, stateReceive.Count.ToString()));
            }


        }


        private void Send(Socket handler, String data)
        {
            try
            {
                // Convert the string data to byte data using ASCII encoding.
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                // Begin sending the data to the remote device.
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), handler);

                Log.Information(string.Format("Sent {0} bytes from {1} to client {2} Data: {3}.", data.Length, handler.LocalEndPoint.ToString(), handler.RemoteEndPoint.ToString(), data));
                Log.Information("SOCKETWRITELOG", handler.LocalEndPoint.ToString() + "\"||\"" + handler.RemoteEndPoint.ToString() + "\"||\"" + data);


            }
            catch (Exception e)
            {
                Log.Error(1020, "Send Error: " + e.Message);
            }

        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);                               


            }
            catch (Exception e)
            {
                Log.Error(1020,"SendCallback Error: " + e.Message);
            }
        }

        public override string Read()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

}

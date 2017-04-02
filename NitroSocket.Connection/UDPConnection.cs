using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Library.Logging;

namespace NitroSocket.Connection
{
    public class UDPConnection : BaseConnection
    {

        private int port;
        private int bufferSize;
        private UdpClient client;
        private IPEndPoint remoteIpEndPoint;

        public UDPConnection(int portParam, int bufferSizeParam)
        {
            port = portParam;
            bufferSize = bufferSizeParam;
        }

        #region BaseConnection Members

        public override void Start()
        {
            client = new UdpClient(port);
            // IPEndPoint object will allow us to read datagrams sent from any source.
            remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Log.Information(string.Format("UDP Socket listennig at Port {0}", port));
        }

        public override void Stop()
        {
            //Nothing to do
            //throw new Exception("The method or operation is not implemented.");
        }

        public override List<object> Receive(IMessageCutter messageCutter)
        {
            List<object> messages = new List<object>();
            byte[] receivedBytes;
            while (client.Available > 0)
            {
                receivedBytes = client.Receive(ref remoteIpEndPoint); //UdpClient.Receive blocks until a message is received from a remote host.
                Log.Information("SOCKETREADLOG", remoteIpEndPoint.Address.ToString() + ":" + port + "\"||\"" + remoteIpEndPoint.ToString() + "\"||\"" + Encoding.ASCII.GetString(receivedBytes));
                messages.Add(Encoding.ASCII.GetString(receivedBytes));
            }
            return messages;
        }

        public override string Read()
        {
            // IPEndPoint object will allow us to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            //UdpClient.Receive blocks until a message is received from a remote host.

            byte[] receivedBytes;
            receivedBytes = client.Receive(ref remoteIpEndPoint);
            return Encoding.ASCII.GetString(receivedBytes);
        }

        public override void Write(string buffer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /*
        public string GetBuffer()
        {
            _hasBuffer = false;
            canWriteBuffer = true;
            return buffer;
        }
        */
        #endregion
    }
}

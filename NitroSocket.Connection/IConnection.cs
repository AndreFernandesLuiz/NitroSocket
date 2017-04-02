using System;
using System.Collections.Generic;
using System.Text;

namespace NitroSocket.Connection
{
    public interface IConnection
    {
        void Start();
        void Stop();
        string Read();
        List<object> Receive(IMessageCutter messageCutter);
        Boolean Send(List<object> msgs);
        void Write(string buffer);
        Boolean hasBuffer { get; set;}
        string GetBuffer();
    }
}

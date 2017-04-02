using System;
using System.Collections.Generic;
using System.Text;

namespace NitroSocket.Connection
{
    public interface IMessageCutter
    {
        List<object> Cut(ref StringBuilder sbBuffer);
        List<object> messagesAck { get; set;}
        Boolean hasAck { get; set;}
    }
}

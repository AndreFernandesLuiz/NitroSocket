using System;
using System.Collections.Generic;
using System.Text;

namespace NitroSocket.Connection
{
    public abstract class BaseMessageCutter : IMessageCutter
    {
        protected Boolean _hasAck = false;
        protected List<object> _messagesAck = new List<object>();
        #region IMessageCutter Members

        public abstract List<object> Cut(ref StringBuilder sbBuffer);

        public bool hasAck
        {
            get
            {
                return _hasAck;
            }
            set
            {
                _hasAck = value;
            }
        }

        public List<object> messagesAck
        {
            get
            {
                return _messagesAck;
            }
            set
            {
                _messagesAck = value;
            }
        }


        #endregion
    }
}

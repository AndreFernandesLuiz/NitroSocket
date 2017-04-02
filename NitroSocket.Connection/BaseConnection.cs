using System;
using System.Collections.Generic;
using System.Text;

namespace NitroSocket.Connection
{
	public abstract class BaseConnection: IConnection
	{
        protected Boolean canWriteBuffer = true;
        protected Boolean _hasBuffer = false;
        #region IMessageConnection Members

        public abstract void Start();
        public abstract void Stop();
        public abstract string Read();
        public abstract List<object> Receive(IMessageCutter messageCutter);


        public virtual Boolean Send(List<object> msgs)
        {
            return true;
        }

        public virtual void Write(string buffer) { }
        public Boolean hasBuffer
        {
            get
            {
                return _hasBuffer;
            }
            set
            {
                _hasBuffer = value;
            }
        }
        public virtual string GetBuffer() {
            throw new Exception("The method or operation is not implemented.");
        }


        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Library.Logging;
using NitroSocket.Connection;

namespace NitroSocket.SimulatorReceive
{
    public class SampleMessageCutter : BaseMessageCutter
    {
        private const string Header = "NITRO";
        public override List<object> Cut(ref StringBuilder sbBuffer)
        {
            string resto = string.Empty;
            string buffer = sbBuffer.ToString(0, sbBuffer.Length);
            List<object> messages = new List<object>();

            while ((buffer.IndexOf(Header) > -1) && (buffer.Length > 7))
            {
                if (buffer.IndexOf(Header) != 0)
                {
                    Log.Error(9,string.Format("Message not begins with correct header. Invalid piece removed: {0}", buffer.Substring(0, buffer.IndexOf(Header))));
                    int remove = buffer.ToString().IndexOf(Header) - 3;
                    if (remove < 1)
                    {
                        remove = buffer.Length;
                    }
                    buffer = buffer.Remove(0, remove);
                    continue;
                }
                int iTamanhoMessage = 0;
                iTamanhoMessage = buffer.Length;

                string sTamanho = buffer.Substring(5, 3);
                int iTamanhoInformado = 0;
                if (Int32.TryParse(sTamanho, out iTamanhoInformado))
                {
                    if (((iTamanhoMessage - 8) >= iTamanhoInformado))
                    {
                        messages.Add(buffer.Substring(0, iTamanhoInformado + 3));
                        buffer = buffer.Remove(0, iTamanhoInformado + 8);
                    }
                    else
                    {
                        if ((buffer.ToString().IndexOf(Header) != (buffer.ToString().LastIndexOf(Header))))
                        {
                            Log.Error(9,string.Format("Length field incorrect : {0}", buffer));
                            Log.Warning(1,string.Format("String remove from buffer: {0}", buffer.Substring(0, iTamanhoMessage)));
                            buffer = buffer.Remove(0, iTamanhoMessage);
                        }
                        else
                        {
                            resto = buffer;
                            break;
                        }
                    }
                }
                else
                {
                    Log.Error(9,string.Format("Second message found without finish the first : {0}", buffer.ToString()));
                    if (buffer.ToString().IndexOf(Header, 3) == -1)
                    {
                        Log.Warning(1,string.Format("String remove from buffer: {0}", buffer));
                        buffer = buffer.Remove(0, buffer.Length);
                    }
                    else
                    {
                        Log.Warning(1,string.Format("String remove from buffer: {0}", buffer.Substring(0, buffer.ToString().IndexOf(Header, 3))));
                        buffer = buffer.Remove(0, buffer.ToString().IndexOf(Header, 3));
                    }
                }
            }
            sbBuffer.Remove(0, sbBuffer.Length);
            sbBuffer.Insert(0, resto);
            return messages;
        }

    }
}

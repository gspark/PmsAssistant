using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluorineFx.AMF3;

namespace PmsAssistant
{
    class AbstractMessageExt : IExternalizable
    {
        private const long serialVersionUID = -834697863344344313L;

        // Serialization constants
        protected internal const short HAS_NEXT_FLAG = 128;
        protected internal const short BODY_FLAG = 1;
        protected internal const short CLIENT_ID_FLAG = 2;
        protected internal const short DESTINATION_FLAG = 4;
        protected internal const short HEADERS_FLAG = 8;
        protected internal const short MESSAGE_ID_FLAG = 16;
        protected internal const short TIMESTAMP_FLAG = 32;
        protected internal const short TIME_TO_LIVE_FLAG = 64;
        protected internal const short CLIENT_ID_BYTES_FLAG = 1;
        protected internal const short MESSAGE_ID_BYTES_FLAG = 2;

        protected internal object clientId;
        protected internal string destination;
        protected internal string messageId;
        protected internal long timestamp;
        protected internal long timeToLive;

        protected internal Object headers;
        protected internal object body;

        protected internal byte[] clientIdBytes;
        protected internal byte[] messageIdBytes;

        public virtual void ReadExternal(IDataInput input)
        {
            short[] flagsArray = readFlags(input);

            for (int i = 0; i < flagsArray.Length; i++)
            {
                short flags = flagsArray[i];
                short reservedPosition = 0;

                if (i == 0)
                {
                    if ((flags & BODY_FLAG) != 0)
                    {
                        readExternalBody(input);
                    }

                    if ((flags & CLIENT_ID_FLAG) != 0)
                    {
                        clientId = input.ReadObject();
                    }

                    if ((flags & DESTINATION_FLAG) != 0)
                    {
                        destination = (string)input.ReadObject();
                    }

                    if ((flags & HEADERS_FLAG) != 0)
                    {
                        headers = (Object)input.ReadObject();
                    }

                    if ((flags & MESSAGE_ID_FLAG) != 0)
                    {
                        messageId = (string)input.ReadObject();
                    }

                    if ((flags & TIMESTAMP_FLAG) != 0)
                    {
                        timestamp = (long)((double)input.ReadObject());
                    }

                    if ((flags & TIME_TO_LIVE_FLAG) != 0)
                    {
                        timeToLive = (long)((double)input.ReadObject());
                    }

                    reservedPosition = 7;
                }
                else if (i == 1)
                {
                    if ((flags & CLIENT_ID_BYTES_FLAG) != 0)
                    {
                        clientIdBytes = ((ByteArray)input.ReadObject()).ToArray();
                        clientId = (new Guid(clientIdBytes)).ToString("D");
                    }

                    if ((flags & MESSAGE_ID_BYTES_FLAG) != 0)
                    {
                        messageIdBytes = ((ByteArray)input.ReadObject()).ToArray();
                        messageId = (new Guid(messageIdBytes)).ToString("D");
                    }

                    reservedPosition = 2;
                }

                // For forwards compatibility, read in any other flagged objects to
                // preserve the integrity of the input stream...
                if ((flags >> reservedPosition) != 0)
                {
                    for (short j = reservedPosition; j < 6; j++)
                    {
                        if (((flags >> j) & 1) != 0)
                        {
                            input.ReadObject();
                        }
                    }
                }
            }
        }

        protected internal virtual void readExternalBody(IDataInput input)
        {
            body = input.ReadObject();
        }

        protected internal virtual short[] readFlags(IDataInput input)
        {
            bool hasNextFlag = true;
            short[] flagsArray = new short[2];
            int i = 0;

            while (hasNextFlag)
            {
                short flags = (short)input.ReadUnsignedByte();
                if (i == flagsArray.Length)
                {
                    short[] tempArray = new short[i * 2];
                    Array.Copy(flagsArray, 0, tempArray, 0, flagsArray.Length);
                    flagsArray = tempArray;
                }

                flagsArray[i] = flags;

                if ((flags & HAS_NEXT_FLAG) != 0)
                {
                    hasNextFlag = true;
                }
                else
                {
                    hasNextFlag = false;
                }

                i++;
            }

            return flagsArray;
        }

        protected internal virtual void writeExternalBody(IDataOutput output)
        {
            output.WriteObject(body);
        }

        public void WriteExternal(IDataOutput output)
        {

        }
    }
}

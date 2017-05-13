using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluorineFx.AMF3;
using FluorineFx.Messaging.Messages;

namespace PmsAssistant
{
    class AcknowledgeMessageExt : AsyncMessageExt, IExternalizable
    {
        private const long serialVersionUID = -8764729006642310394L;
        public const string CLASS_ALIAS = "DSK";

        public AcknowledgeMessageExt()
            : this(null)
        {
        }

        public AcknowledgeMessageExt(AcknowledgeMessage message)
            : base()
        {
            _message = message;
        }

        public virtual string Alias
        {
            get
            {
                return CLASS_ALIAS;
            }
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void writeExternal(java.io.ObjectOutput output) throws java.io.IOException
        public void WriteExternal(IDataOutput output)
        {
            /*if (_message != null)
            {
                _message.writeExternal(output);
            }
            else
            {
                base.writeExternal(output);
            }*/
        }

        public void ReadExternal(IDataInput input)
        {
            base.ReadExternal(input);

            short[] flagsArray = readFlags(input);
            for (int i = 0; i < flagsArray.Length; i++)
            {
                short flags = flagsArray[i];
                short reservedPosition = 0;

                // For forwards compatibility, read in any other flagged objects
                // to preserve the integrity of the input stream...
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

        private AcknowledgeMessage _message;
    }
}

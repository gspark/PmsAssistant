using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluorineFx.AMF3;

namespace PmsAssistant
{
    class AsyncMessageExt : AbstractMessageExt, IExternalizable
    {
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

        public void WriteExternal(IDataOutput output)
        {
            /*base.writeExternal(output);
 
            short flags = 0;
            output.WriteByte(flags);*/
        }
    }
}

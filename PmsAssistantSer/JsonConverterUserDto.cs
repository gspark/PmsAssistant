using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluorineFx.Json;

namespace PmsAssistant
{
    class JsonConverterUserDto : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value)
        {
            if (value is IConvertible)
            {
                IConvertible convertible  = value as IConvertible;

                switch (convertible.GetTypeCode())
                {
                    case TypeCode.DateTime:
                        writer.WriteValue(((DateTime)convertible).AddHours(8));
                        break;
                }
            }
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType.Name.Equals("DateTime"))
            {
                return true;
            }
            return false;
        }
    }
}

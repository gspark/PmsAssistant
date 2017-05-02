using Microsoft.VisualStudio.TestTools.UnitTesting;
using PmsAssistant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluorineFX.Serialization;
using Xstream.Core;

namespace PmsAssistant.Tests
{
    [AmfObject("namespase.of.your.object")]
    public class CustomAmfObject
    {
        [AmfMember("bit_prop")]
        public bool BooleanProperty { get; set; } = true;

        [AmfMember]
        public sbyte UnsignedByteProperty { get; set; } = 2;

        public string StringProperty { get; set; } = "test";

        [AmfMember("bit_fld")] public bool booleanField = false;
        [AmfMember] public float singleField = -5.00065f;
        public string stringField = "test2";

        public CustomAmfObject() { }
    }

    [TestClass()]
    public class IhotelTests
    {
        [TestMethod()]
        public async Task loginTest()
        {
            var ihotel = new Ihotel();
            var ret = await ihotel.Login();
        }


        [TestMethod()]
        public void loginAMF()
        {
            string aaa = "00 03 00 00 00 01 00 04 6e 75 6c 6c 00 02 2f 31"
                         + "00 00 00 e0 0a 00 00 00 01 11 0a 81 13 4d 66 6c"
                         + "65 78 2e 6d 65 73 73 61 67 69 6e 67 2e 6d 65 73"
                         + "73 61 67 65 73 2e 43 6f 6d 6d 61 6e 64 4d 65 73"
                         + "73 61 67 65 13 6f 70 65 72 61 74 69 6f 6e 1b 63"
                         + "6f 72 72 65 6c 61 74 69 6f 6e 49 64 13 74 69 6d"
                         + "65 73 74 61 6d 70 11 63 6c 69 65 6e 74 49 64 17"
                         + "64 65 73 74 69 6e 61 74 69 6f 6e 13 6d 65 73 73"
                         + "61 67 65 49 64 15 74 69 6d 65 54 6f 4c 69 76 65"
                         + "09 62 6f 64 79 0f 68 65 61 64 65 72 73 04 05 06"
                         + "01 04 00 01 06 01 06 49 30 46 43 35 43 43 36 36"
                         + "2d 43 37 38 38 2d 42 30 30 41 2d 31 46 30 39 2d"
                         + "42 46 32 39 42 38 44 43 38 37 46 35 04 00 0a 0b"
                         + "01 01 0a 05 25 44 53 4d 65 73 73 61 67 69 6e 67"
                         + "56 65 72 73 69 6f 6e 04 01 09 44 53 49 64 06 07"
                         + "6e 69 6c 01";
            //string aaa = "00 03 00 00 00 01 00 04 6E 75 6C 6C 00 02 2F 33 " +

            //             "00 00 01 28 0A 00 00 00 01 11 0A 81 13 4F 66 6C " +

            //             "65 78 2E 6D 65 73 73 61 67 69 6E 67 2E 6D 65 73 " +

            //             "73 61 67 65 73 2E 52 65 6D 6F 74 69 6E 67 4D 65 " +

            //             "73 73 61 67 65 13 6F 70 65 72 61 74 69 6F 6E 0D " +

            //             "73 6F 75 72 63 65 13 74 69 6D 65 73 74 61 6D 70 " +

            //             "17 64 65 73 74 69 6E 61 74 69 6F 6E 11 63 6C 69 " +

            //             "65 6E 74 49 64 15 74 69 6D 65 54 6F 4C 69 76 65 " +

            //             "13 6D 65 73 73 61 67 65 49 64 0F 68 65 61 64 65 " +

            //             "72 73 09 62 6F 64 79 06 0F 64 6F 4C 6F 67 69 6E " +

            //             "06 0B 4C 6F 67 69 6E 04 00 06 0D 61 6D 66 70 68 " +

            //             "70 06 49 35 30 44 33 34 31 34 34 2D 34 46 45 32 " +

            //             "2D 33 42 32 38 2D 39 32 43 41 2D 30 30 30 30 31 " +

            //             "33 30 30 39 30 42 41 04 00 06 49 36 39 42 41 37 " +

            //             "41 38 43 2D 36 41 46 38 2D 30 30 44 44 2D 33 35 " +

            //             "30 31 2D 45 43 44 32 39 41 44 32 31 44 41 31 0A " +

            //             "0B 01 15 44 53 45 6E 64 70 6F 69 6E 74 01 09 44 " +

            //             "53 49 64 06 07 6E 69 6C 01 09 03 01 09 0B 01 04 " +

            //             "88 61 04 00 06 09 75 75 79 78 06 11 72 75 61 6E " +

            //             "62 6F 5F 31 06 0D 31 32 33 34 35 36 ";
            byte[] bbb = this.strToToHexByte(aaa);
            FluorineFx.IO.AMFDeserializer ad = new FluorineFx.IO.AMFDeserializer(new MemoryStream(bbb));
            FluorineFx.AMF3.ByteArray br = new FluorineFx.AMF3.ByteArray(new MemoryStream(bbb));
            XStream ox = new XStream();
            try
            {
                string xml = ox.ToXml(ad);
                StreamWriter sw = new StreamWriter("Z:\\temp\\AMF3.xml", false);
                sw.WriteLine(xml);
                sw.Close();
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        /// <summary>  

        /// 16进制字符串转字节数组  

        /// </summary>  

        /// <param name="hexString"></param>  

        /// <returns></returns>  

        public byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;

        }

        [TestMethod()]
        public void testAMF()
        {
            CustomAmfObject customObject = new CustomAmfObject();

            byte[] serializedBuffer = customObject.SerializeToAmf();

            string hex = BitConverter.ToString(serializedBuffer).Replace("-", " ");

            CustomAmfObject deserializedObject = serializedBuffer.DeserializeFromAmf<CustomAmfObject>();
        }
    }
}
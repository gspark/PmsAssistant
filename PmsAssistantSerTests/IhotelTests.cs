using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluorineFx;
using FluorineFx.AMF3;
using FluorineFx.IO;
using FluorineFx.Messaging.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PmsAssistant;

namespace PmsAssistantSerTests
{

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
            //parse(Ihotel.AMFHexString, "amf0");
            parse(Ihotel.AMFHexString, "amf1");
        }

        private void parse(string amf, string name)
        {
            byte[] bbb = StrToToHexByte(amf);

            AMFDeserializer ad = new AMFDeserializer(new MemoryStream(bbb));
            AMFMessage message = ad.ReadAMFMessage();

            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(message);
            //Write that JSON to txt file
            File.WriteAllText(@"z:\temp\hotel\" + name + @".json", json);

            foreach (var body in message.Bodies)
            {
                object[] content = body.Content as object[];
                RemotingMessage rm = content[0] as RemotingMessage;

                rm.messageId = Guid.NewGuid().ToString("D");

                object[] bodys = rm.body as object[];
                ASObject ab = bodys[2] as ASObject;

                ASObject masterBase = ab["masterBase"] as ASObject;
                masterBase["dep"] = DateTime.Now;
                masterBase["arr"] = DateTime.Now;
                masterBase["rsvMan"] = "马一一";
                masterBase["cutoffDate"] = DateTime.Now;

                ASObject masterGuest = ab["masterGuest"] as ASObject;
                masterGuest["name"] = "马一一";
                masterGuest["name2"] = "Ma Yi Yi";
                masterGuest["sex"] = "1";

                ArrayCollection rsvSrcList = ab["rsvSrcList"] as ArrayCollection;
                ASObject rsvObject = rsvSrcList[0] as ASObject;
                rsvObject["arrDate"] = DateTime.Now;
                rsvObject["depDate"] = DateTime.Now;
                rsvObject["rsvArrDate"] = DateTime.Now;
                rsvObject["rsvDepDate"] = DateTime.Now;
                rsvObject["negoRate"] = 268;
                rsvObject["oldRate"] = 268;
                rsvObject["realRate"] = 268;
                rsvObject["rackRate"] = 268;
            }
        }

        /// <summary>  
        /// 16进制字符串转字节数组  
        /// </summary>  
        /// <param name="hexString"></param>  
        /// <returns></returns>  
        public byte[] StrToToHexByte(string hexString)
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
        public async Task SaveReserveTest()
        {
            var ihotel = new Ihotel();
            var ret = await ihotel.Login();
            if (ret)
            {
                ret = await ihotel.SaveReserve("马四",
                    Convert.ToDateTime("2017-05-11 06:00:00"),
                    Convert.ToDateTime("2017-05-10 10:00:00"),
                    Convert.ToDateTime("2017-05-10 12:00:00"));
            }
        }

        [TestMethod()]
        public async Task RateQueryTest()
        {
            var ihotel = new Ihotel();
            var ret = await ihotel.Login();
            if (ret)
            {
                ret = await ihotel.RateQuery(Convert.ToDateTime("2017-05-10 18:00:00"));
            }

        }
    }
}
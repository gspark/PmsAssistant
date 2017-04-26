using Microsoft.VisualStudio.TestTools.UnitTesting;
using PmsAssistant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmsAssistant.Tests
{
    [TestClass()]
    public class IhotelTests
    {
        [TestMethod()]
        public void loginTest()
        {
            var ihotel = new Ihotel();
            ihotel.login().Wait();
        }
    }
}
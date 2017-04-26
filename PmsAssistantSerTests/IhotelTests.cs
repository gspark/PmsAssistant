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
        public async Task loginTest()
        {
            var ihotel = new Ihotel();
            var ret = await ihotel.login();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;


namespace PmsAssistant
{
    public partial class PmsAssSrv : ServiceBase
    {
        public PmsAssSrv()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var ihotel = new Ihotel();
            Task<bool> ret = ihotel.login();
        }

        protected override void OnStop()
        {
        }
    }
}

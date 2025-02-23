using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Quartz.Logging.OperationName;

namespace AndonWatchDog
{
    public class RefreshJob : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            //await Task.Run(() =>
            //{
            SendKeys.SendWait("^{F5}");
            Logger.Info("Ctrl+F5 has been sent.");
            //});
        }
    }
}

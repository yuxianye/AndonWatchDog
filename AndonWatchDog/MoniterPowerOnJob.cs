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
    public class MoniterPowerOnJob : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            //await Task.Run(() =>
            //{
            WinAPI.SetBrightness(WinAPI.MONITOR_ON);
            Logger.Info("MoniterPowerOnJob has been sent.");

            //});
        }
    }
}

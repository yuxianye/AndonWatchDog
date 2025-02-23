using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Quartz.Logging.OperationName;

namespace AndonWatchDog
{
    public class MoniterPowerOffJob : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            //await Task.Run(() =>
            //{

            Logger.Info("Monitor is being shut off");
            //锁屏+关屏    
            //WinAPI.LockWorkStation();
            //WinAPI.SendMessage(this.Handle, (uint)0x0112, (IntPtr)0xF170, (IntPtr)2);

            //Thread.Sleep(2000);

            //WinAPI.SetBrightness(WinAPI.MONITOR_ON);
            //Logger.Info("Monitor is turned on");
            //SendKeys.SendWait("^{F5}");
            Logger.Info("MoniterPowerOffJob has been sent.");


            //});
        }
    }
}

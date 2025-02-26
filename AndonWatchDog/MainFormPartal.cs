using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace AndonWatchDog
{
    public partial class MainForm : Form
    {

        //int interval = 1000 * 60 * 1;

        /// <summary>
        /// 默认5分钟
        /// </summary>
        public int Interval
        {
            get
            {
                int interval = 5 * 60 * 1000;
                try
                {
                    int.TryParse(AndonWatchDog.ConfigHelper.GetAppSetting("Interval"), out interval);
                    return 1000 * 60 * interval;

                }
                catch
                {
                    return 1000 * 60 * interval;
                }


            }

        }

        /// <summary>
        /// 默认7
        /// </summary>
        public int MonitorPowerOn
        {
            get
            {
                int tmp = 7;
                try
                {
                    int.TryParse(AndonWatchDog.ConfigHelper.GetAppSetting("MonitorPowerOn"), out tmp);
                    return tmp;

                }
                catch
                {
                    return tmp;
                }


            }

        }

        /// <summary>
        /// 默认19
        /// </summary>
        public int MonitorPowerOff
        {
            get
            {
                int tmp = 7;
                try
                {
                    int.TryParse(AndonWatchDog.ConfigHelper.GetAppSetting("MonitorPowerOff"), out tmp);
                    return tmp;

                }
                catch
                {
                    return tmp;
                }


            }

        }



    }
}

using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
//using Quartz;
namespace AndonWatchDog
{
    public partial class MainForm : Form
    {

        int interval = 1000 * 60 * 1;
        System.Threading.Timer timer;

        string webTitle = "百度一下，你就知道";
        bool isRefresh = false;
        bool isMoniterOn = true;

        //创建调度单元
        //Task<IScheduler> tsk = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
        //IScheduler scheduler;


        public MainForm()
        {
            InitializeComponent();
            try
            {
                int.TryParse(AndonWatchDog.ConfigHelper.GetAppSetting("Interval"), out int tmpInterval);
                interval = 1000 * 60 * tmpInterval;
                nud_interval.Value = tmpInterval;

                textBox1.Text = ConfigHelper.GetAppSetting("WebTitle");
                webTitle = textBox1.Text;

                var autoStartup = ConfigHelper.GetAppSetting("AutoStartup").ToLower();
                if (autoStartup == "true")
                {
                    cb_Startup.Checked = true;
                }
                else
                {
                    cb_Startup.Checked = false;
                }

                Logger.Info($"Interval:{tmpInterval}");
                Logger.Info($"WebTitle:{textBox1.Text}");
                Logger.Info($"AutoStartup:{autoStartup}");

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);

            }

        }


        //private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //if (MessageBox.Show("确定要关闭程序吗？\r\n[OK]关闭程序\r\n[Cancel]取消关闭\r\n提示：可最小化窗口并显示在通知栏！", "Andon WatchDog", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //{
        //    //this.notifyIcon1.Visible = true;

        //    //e.Cancel = true;
        //}
        //else
        //{
        //    e.Cancel = true;
        //    //if (notifyIcon1.Visible)
        //    //{
        //    //    this.WindowState = FormWindowState.Minimized;
        //    //    this.notifyIcon1.Visible = true;

        //    //}

        //}

        //}

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {

                this.ShowInTaskbar = false;
                //this.notifyIcon1.Visible = true;

            }

            if (this.WindowState == FormWindowState.Normal)
            {

                this.ShowInTaskbar = true;
                //this.notifyIcon1.Visible = false;

            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.notifyIcon1.Visible = false;
            this.Close();
            this.Dispose();

        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int.TryParse(ConfigHelper.GetAppSetting("IdleInterval"), out int tmpIdleInterval);

            var result = timer.Change(1000 * 10, tmpIdleInterval * 60 * 1000);


            Debug.Print("stopToolStripMenuItem_Click:" + result.ToString());

            btn_Start.Enabled = true;
            btn_Stop.Enabled = false;
            this.startToolStripMenuItem.Enabled = true;
            this.stopToolStripMenuItem.Enabled = false;
            Logger.Info($"Stop");

            var status = WinAPI.GetTaskbarState();
            Logger.Info($"TaskbarState:{status.ToString()}");

            if (status != WinAPI.AppBarStates.AlwaysOnTop)
            {
                WinAPI.SetTaskbarState(WinAPI.AppBarStates.AlwaysOnTop);
                Logger.Info($"set TaskbarState:AlwaysOnTop");
            }

        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = timer.Change(interval, interval);
            Debug.Print("startToolStripMenuItem_Click:" + result.ToString());

            btn_Start.Enabled = false;
            btn_Stop.Enabled = true;
            this.startToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Enabled = true;
            Logger.Info($"Start");

            var status = WinAPI.GetTaskbarState();
            Logger.Info($"TaskbarState:{status.ToString()}");

            if (status != WinAPI.AppBarStates.AutoHide)
            {
                WinAPI.SetTaskbarState(WinAPI.AppBarStates.AutoHide);
                Logger.Info($"set TaskbarState:AutoHide");
            }
        }

        private void showMainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
            //this.Focus();

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            //this.WindowState = FormWindowState.Normal;
            //this.ShowInTaskbar = true;

            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;

            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;

            }
            //this.notifyIcon1.Visible = false;

        }

        private void nud_interval_ValueChanged(object sender, EventArgs e)
        {
            this.interval = (int)nud_interval.Value * 1000 * 60;

            //File.WriteAllText("interval.txt", ((int)nud_interval.Value).ToString());

            ConfigHelper.AddAppSetting("Interval", ((int)nud_interval.Value).ToString());

            if (btn_Stop.Enabled == true && btn_Start.Enabled == false)
            {
                var result = timer?.Change(interval, interval);
                Debug.Print("nud_interval_ValueChanged:" + result.ToString());

            }
            Logger.Info($"interval changed to {interval}");

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer = new System.Threading.Timer(timer_Callback, null, interval, interval);
            this.WindowState = FormWindowState.Minimized;
            Logger.Info($"Timer start {interval}");
            //toolStripStatusLabel1.Text = "program start";

            var status = WinAPI.GetTaskbarState();
            Logger.Info($"TaskbarState:{status.ToString()}");

            if (status != WinAPI.AppBarStates.AutoHide)
            {
                WinAPI.SetTaskbarState(WinAPI.AppBarStates.AutoHide);
                Logger.Info($"SetTaskbarState:AutoHide");
            }


            //WinAPI.SendMessage(this.Handle, (uint)0x0112, (IntPtr)0xF170, (IntPtr)2);


            //SetBrightness(MONITOR_ON);
            //scheduler = tsk.Result;
            ////创建具体的作业，具体的job需要单独在一个执行文件中执行
            //IJobDetail Job = JobBuilder.Create<RefreshJob>().WithIdentity("yuxianye").Build();
            ////IJobDetail Job2 = JobBuilder.Create<TestJob>().WithIdentity("奇偶比JOB2").Build();
            ////创建并配置一个触发器
            //ITrigger _ctroTrigger = TriggerBuilder.Create().
            //    WithIdentity("yuxianye")
            //    .WithCronSchedule(ConfigHelper.GetAppSetting("RefreshCron")) //"0 0 9 * * ?"
            //    .StartNow().Build() as ITrigger;
            ////将job和trigger加入到作业调度中
            //scheduler.ScheduleJob(Job, _ctroTrigger);



            //IJobDetail moniterPowerOffJob = JobBuilder.Create<MoniterPowerOffJob>().WithIdentity("MoniterPowerOffJob").Build();

            //ITrigger moniterPowerOffJobTrigger = TriggerBuilder.Create().
            //   WithIdentity("MoniterPowerOffJob")
            //   .WithCronSchedule(ConfigHelper.GetAppSetting("MoniterPowerOffCron")) //"0 0 9 * * ?"
            //   .StartNow().Build() as ITrigger;
            //scheduler.ScheduleJob(moniterPowerOffJob, moniterPowerOffJobTrigger);

            //IJobDetail moniterPowerOnJob = JobBuilder.Create<MoniterPowerOnJob>().WithIdentity("MoniterPowerOnJob").Build();

            //ITrigger moniterPowerOnJobTrigger = TriggerBuilder.Create().
            //   WithIdentity("MoniterPowerOnJob")
            //   .WithCronSchedule(ConfigHelper.GetAppSetting("MoniterPowerOnCron")) //"0 0 9 * * ?"
            //   .StartNow().Build() as ITrigger;
            //scheduler.ScheduleJob(moniterPowerOnJob, moniterPowerOnJobTrigger);



            //开启调度
            //scheduler.Start();

        }


        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.notifyIcon1.Visible = false;
            Logger.Info($"Main Form Closed");


            var status = WinAPI.GetTaskbarState();
            Logger.Info($"TaskbarState:{status.ToString()}");

            if (status != WinAPI.AppBarStates.AlwaysOnTop)
            {
                WinAPI.SetTaskbarState(WinAPI.AppBarStates.AlwaysOnTop);
                Logger.Info($"set TaskbarState:AlwaysOnTop");
            }
        }



        private void txt_WebTitle_TextChanged(object sender, EventArgs e)
        {
            //File.Create("webTitle.txt").Close();
            //File.WriteAllText("webTitle.txt", textBox1.Text.Trim());
            ConfigHelper.AddAppSetting("WebTitle", textBox1.Text.Trim());
            webTitle = textBox1.Text.Trim();
            Logger.Info($"Web Title Text Changed to {textBox1.Text.Trim()}");


        }

        private void cb_Startup_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Startup.Checked)
            {
                ShortcutManagement.CreateShort();
                Logger.Info($"auto Startup created");

            }
            else
            {
                ShortcutManagement.DeleteShort();
                Logger.Info($"auto Startup delete");

            }

        }


        private void timer_Callback(object state)
        {

            //停止状态，
            if (btn_Stop.Enabled == false && btn_Start.Enabled == true)
            {
                int.TryParse(ConfigHelper.GetAppSetting("IdleInterval"), out int tmpIdleInterval);
                //检查是长时间无人操作
                if (WinAPI.GetLastInputTime() > tmpIdleInterval * 60 * 1000)
                {

                    //长时间无人操作，自动开始
                    if (btn_Start.InvokeRequired)
                    {
                        // 使用 Invoke 方法将操作委托给 UI 线程
                        btn_Start.Invoke(new MethodInvoker(delegate
                        {
                            btn_Start.PerformClick();
                            Thread.Sleep(3000);
                            Logger.Info("btn_Start.PerformClick()");
                        }));
                    }
                    else
                    {
                        // 如果当前线程是 UI 线程，则直接更新控件
                        btn_Start.PerformClick();
                        Logger.Info("btn_Start.PerformClick()");

                    }
                }
                else
                {
                    Logger.Info("status is stop ，not auto start ");
                    return;
                }

            }

            /////////////////////////////////////////////////////////////


            int.TryParse(ConfigHelper.GetAppSetting("MoniterPowerOff"), out int offHour);
            int.TryParse(ConfigHelper.GetAppSetting("MoniterPowerOn"), out int onHour);
            Logger.Info($"MoniterPowerOff:{offHour} MoniterPowerOn{onHour}");

            if (DateTime.Now.Hour >= offHour && DateTime.Now.Hour < onHour)
            {
                // 启动屏幕保护程序，但不锁定工作站
                //bool r = SystemParametersInfo(0x0021, 0, "scrnsave.scr", 0);
                StartScreenSaver();

                // 使用SystemParametersInfo来启动屏保，这里的"scrnsave.scr"应为完整路径或确保系统能找到的路径名。
                //Logger.Info($"screen save :{r}");
                Logger.Info($"screen save ");

                //isMoniterOn = false;
                return;
            }
            else
            {

                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                Logger.Info("mouse move for screen wakeup");

                //isMoniterOn = true;

            }


            //////////////////////////////////////////////////


            //1、检查edge是否打开
            //2、打开edge浏览器设置全屏，设置网址
            //3、设置焦点
            //4、鼠标点击


            //var edgeProcess = Process.GetProcessesByName("msedge");
            bool isSet = WinAPI.SetPageGetFocusByTitle(webTitle);

            Logger.Info($"SetPageGetFocusByTitle:{isSet}");

            if (!isSet)
            {
                Logger.Info($"没找到【{webTitle}】");
                //toolStripStatusLabel1.Text = $"没找到【{webTitle}】";

                var url = ConfigHelper.GetAppSetting("AutoOpenUrl");
                url = string.Format(url, DomainHelper.GetFullMachineName());
                Debug.Print("url:" + url);
                Logger.Info($"启动新Edge, {url}");

                EdgeHelper.StartFullScreenEdge(url);
                Logger.Info($"新Edge启动完成");

            }



            //if (!edgeProcess.Any())
            //{
            //    //1、检查edge是否打开
            //    //2、打开edge浏览器设置全屏，设置网址
            //    var url = ConfigHelper.GetAppSetting("AutoOpenUrl");
            //    url = string.Format(url, DomainHelper.GetFullMachineName());
            //    Debug.Print("url:" + url);
            //    EdgeHelper.StartFullScreenEdge(url);
            //}
            //else
            //{


            //    bool isOpenedPage = WinAPI.IsPageSetFocus(edgeProcess, ConfigHelper.GetAppSetting("WebTitle"));
            //    if (isOpenedPage)
            //    {


            //    }
            //    else
            //    {
            //        var url = ConfigHelper.GetAppSetting("AutoOpenUrl");
            //        url = string.Format(url, DomainHelper.GetFullMachineName());
            //        Debug.Print("url:" + url);
            //        EdgeHelper.StartFullScreenEdge(url);

            //    }


            //foreach (var edge in edgeProcess)
            //{
            //    if (edge.MainWindowHandle == IntPtr.Zero)
            //    {
            //        //WinAPI.ShowWindow(edge.MainWindowHandle, 1);
            //        continue;
            //    }
            //    AutomationElementCollection roots = AutomationElement.RootElement.FindAll(TreeScope.Element | TreeScope.Children,
            //new AndCondition(new PropertyCondition(AutomationElement.ProcessIdProperty, edge.Id),
            //new PropertyCondition(AutomationElement.ClassNameProperty, "Chrome_WidgetWin_1")));
            //    Debug.WriteLine("roots:" + roots.Count);

            //    foreach (AutomationElement rootElement in roots)
            //    {
            //        Debug.WriteLine("rootElement:" + rootElement.ToString());

            //        AutomationElement address = rootElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, webTitle));

            //        if (address == null)
            //        {

            //            address = rootElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, webTitle));
            //            if (address == null)
            //            {
            //                continue;
            //            }
            //        }
            //        // 最大化窗口
            //        //ShowWindow(process.MainWindowHandle, 3);
            //        address.SetFocus();

            //    }
            //}

            //string url = WinAPI.GetURL2(edge);
            //Debug.Print("GetChromeUrl:" + url);

            //string url = WinAPI.GetURL2();
            //foreach (var edge in edgeProcess)
            //{
            //    string url=WinAPI.GetURL2(edge);
            //    Debug.Print("GetChromeUrl:" + url);

            //}
            //var geturl = WinAPI.GetURL2();
            //Debug.Print("url:" + geturl);

            ////浏览器打开了，但是网页没打开，打开网页
            //WinAPI.SetWindowsTop(ConfigHelper.GetAppSetting("WebTitle"));

            //}






            //4、鼠标点击
            int.TryParse(ConfigHelper.GetAppSetting("ClickPositionX"), out int X);
            int.TryParse(ConfigHelper.GetAppSetting("ClickPositionY"), out int Y);
            WinAPI.SetCursorPos(X, Y);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

            Logger.Info($"鼠标点击 X:{X}  Y:{Y}");
            //toolStripStatusLabel1.Text = $"executed at {DateTime.Now.ToString("yyyy-MM--dd HH:mm:ss")}】";

            Debug.Print(DateTime.Now.ToLongTimeString());
            //timer.Change

            int hour = DateTime.Now.Hour % 4;
            Logger.Info($"hour{hour}");


            //0/4/8/12/16/20 refresh
            if (isRefresh == false && DateTime.Now.Hour % 4 == 0 && DateTime.Now.Minute < nud_interval.Value)
            {
                SendKeys.SendWait("^{F5}");
                Logger.Info("Ctrl+F5 has been sent.");
                isRefresh = true;
            }
            else
            {

                isRefresh = false;

            }



            //////////////////////////////////////
            ///


            //0/4/8/12/16/20  moniter power off
            //if (ConfigHelper.GetAppSetting("MoniterPowerOff").Split(',').AsQueryable().Select(a => a == DateTime.Now.Hour.ToString()).Any() && DateTime.Now.Minute < nud_interval.Value)
            //{
            //    WinAPI.TurnOff();
            //    Logger.Info("TurnOff");

            //    //WinAPI.SendMessage(this.Handle, (uint)0x0112, (IntPtr)0xF170, (IntPtr)2);
            //}
            //else
            //{

            //    //isRefresh = false;

            //}

            //// 防止系统进入休眠状态
            //SystemSleepManagement.PreventSleep();
            //Logger.Info("防止系统进入休眠状态");

            //Logger.Info("start 关闭显示器");
            //// 关闭显示器
            ////DisplayControl.TurnOffDisplay();
            //DisplayControl.TurnLowDisplay();
            //Logger.Info("显示器已关闭");
            // 模拟一些操作
            //System.Threading.Thread.Sleep(50000);
            //// 打开显示器
            //DisplayControl.TurnOnDisplay();
            //Logger.Info("显示器已开启");
            //// 恢复系统休眠策略
            //SystemSleepManagement.RestoreSleep();




            ////打开
            //string open = "08:00:00";
            ////关闭
            //string close = "19:10:00";

            //DateTime now = DateTime.Now;

            //string format1 = now.ToString("HH:mm:ss");
            //Console.WriteLine(format1); // 输出类似 "2023-04-05 13:45:30"
            //if (isMoniterOn)
            //{
            //    //锁屏+关屏    
            //    //LockWorkStation();
            //    //SendMessage(this.Handle, (uint)0x0112, (IntPtr)0xF170, (IntPtr)2);
            //    // MessageBox.Show("打开"+ format1);
            //    //isMoniterOn = false;
            //    //Logger.Info($"isMoniterOn:{isMoniterOn}");

            //    if (this.InvokeRequired)
            //    {
            //        // 使用 Invoke 方法将操作委托给 UI 线程
            //        this.Invoke(new MethodInvoker(delegate
            //        {
            //            SendMessage(this.Handle, (uint)0x0112, (IntPtr)0xF170, (IntPtr)2);
            //            //SendMessage(IntPtr.Zero, (uint)0x0112, (IntPtr)0xF170, (IntPtr)2);
            //            // MessageBox.Show("打开"+ format1);
            //            isMoniterOn = false;
            //            Logger.Info($"set off, isMoniterOn:{isMoniterOn}");
            //        }));
            //    }
            //    else
            //    {
            //        SendMessage(this.Handle, (uint)0x0112, (IntPtr)0xF170, (IntPtr)2);
            //        //SendMessage(IntPtr.Zero, (uint)0x0112, (IntPtr)0xF170, (IntPtr)2);
            //        // MessageBox.Show("打开"+ format1);
            //        isMoniterOn = false;
            //        Logger.Info($"isMoniterOn:{isMoniterOn}");

            //    }



            //}
            //else
            ////if (format1.Equals(open))
            //{
            //    int i = 0;
            //    while (i < 12)
            //    {
            //        i++;
            //        SetBrightness(MONITOR_ON);

            //        SendMessage(IntPtr.Zero, (uint)0x0112, 0xF170, -1);

            //        //WinAPI.SetCursorPos(X, Y);

            //        //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN, x * 65536 / 1024, (i % 17) * 65536 / 768, 0, 0);
            //        //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTUP, (i % 7) * 65536 / 1024, (i % 17) * 65536 / 768, 0, 0);
            //        WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, X, Y, 0, 0);

            //        SendKeys.SendWait("^{F5}");

            //        //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            //        //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);



            //        isMoniterOn = true;

            //    }
            //    Logger.Info($"SetBrightness,isMoniterOn:{isMoniterOn}");


            //}
        }








        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool SystemParametersInfo(uint action, uint uParam, string vParam, uint winIni);




        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        static extern bool SetPowerRequirement(IntPtr hWnd, byte bRequirement, int Timeout);

        const uint WM_SYSCOMMAND = 0x0112;
        const int SC_MONITORPOWER = 0xF175;


        const int MONITOR_ON = -1;
        const int MONITOR_OFF = 2;
        public static void SetBrightness(int brightness)
        {
            IntPtr hWnd = new IntPtr(0xFFFF); // 用一个特殊的窗口句柄代表整个系统
            SendMessage(hWnd, WM_SYSCOMMAND, SC_MONITORPOWER, brightness);
        }

        //关屏       
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        //锁屏       
        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();




        // 屏幕保护程序的路径
        private const string ScreenSaverPath = @"C:\Windows\System32\scrnsave.scr";

        // 启动屏幕保护程序
        public static void StartScreenSaver()
        {
            try
            {
                // 创建一个新的进程启动信息
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = ScreenSaverPath,
                    Arguments = "/s" // /s 参数表示启动屏幕保护程序
                };

                // 创建并启动进程
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"启动屏幕保护程序时出错: {ex.Message}");
            }
        }







    }
}

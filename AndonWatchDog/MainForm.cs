using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace AndonWatchDog
{
    public partial class MainForm : Form
    {

        int interval = 1000 * 60 * 1;
        System.Threading.Timer timer;

        string webTitle = "百度一下，你就知道";

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

        }

        private void showMainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
            this.Focus();

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

        }


        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.notifyIcon1.Visible = false;
            Logger.Info($"Main Form Closed");

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
                    Logger.Info("停止状态，有人一直在操作，不自动开始");
                    return;
                }

            }








            //1、检查edge是否打开
            //2、打开edge浏览器设置全屏，设置网址
            //3、设置焦点
            //4、鼠标点击


            //var edgeProcess = Process.GetProcessesByName("msedge");
            bool isSet = WinAPI.SetPageGetFocusByTitle(webTitle);


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

        }



    }
}

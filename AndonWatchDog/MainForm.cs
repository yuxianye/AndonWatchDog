using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Automation;
using System.Windows.Forms;

namespace AndonWatchDog
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            try
            {
                int.TryParse(AndonWatchDog.ConfigHelper.GetAppSetting("Interval"), out int tmpInterval);
                interval = 1000 * 60 * tmpInterval;
                nud_interval.Value = tmpInterval;

                textBox1.Text = ConfigHelper.GetAppSetting("WebTitle");

                var autoStartup = ConfigHelper.GetAppSetting("AutoStartup").ToLower();
                if (autoStartup == "true")
                {
                    cb_Startup.Checked = true;
                }
                else
                {
                    cb_Startup.Checked = false;
                }

            }
            catch
            {


            }
        }



        int interval = 1000 * 60 * 1;
        System.Threading.Timer timer;
        string webTitle = "百度一下，你就知道";
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
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

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.notifyIcon1.Visible = false;
            this.Close();
            this.Dispose();

        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = timer.Change(int.MaxValue, int.MaxValue);
            Debug.Print("stopToolStripMenuItem_Click:" + result.ToString());

            btn_Start.Enabled = true;
            btn_Stop.Enabled = false;
            this.startToolStripMenuItem.Enabled = true;
            this.stopToolStripMenuItem.Enabled = false;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = timer.Change(interval, interval);
            Debug.Print("startToolStripMenuItem_Click:" + result.ToString());

            btn_Start.Enabled = false;
            btn_Stop.Enabled = true;
            this.startToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Enabled = true;
        }

        private void showMainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
            this.Focus();

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //{

            //    this.ShowInTaskbar = false;
            //    //this.notifyIcon1.Visible = true;

            //}

            //if (this.WindowState == FormWindowState.Normal)
            //{

            //    this.ShowInTaskbar = true;
            //    //this.notifyIcon1.Visible = false;

            //}

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

            ConfigHelper.AddAppSetting("", ((int)nud_interval.Value).ToString());

            if (btn_Stop.Enabled == true && btn_Start.Enabled == false)
            {
                var result = timer?.Change(interval, interval);
                Debug.Print("nud_interval_ValueChanged:" + result.ToString());

            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string c = Environment.GetEnvironmentVariable("computername");
            Debug.Print("GetEnvironmentVariable:" + c);

            string d = Dns.GetHostEntry("localhost").HostName;
            Debug.Print("GetHostEntry:" + d);

            timer = new System.Threading.Timer(timer_Callback, null, interval, interval);
            //this.WindowState = FormWindowState.Minimized;
        }

        private void timer_Callback(object state)
        {
            //1、检查edge是否打开
            //2、打开edge浏览器设置全屏，设置网址
            //3、设置焦点
            //4、鼠标点击
            var edgeProcess = Process.GetProcessesByName("msedge");
            if (!edgeProcess.Any())
            {
                Process process_cmd = new Process();
                process_cmd.StartInfo.FileName = "cmd.exe";//进程打开文件名为“cmd”

                process_cmd.StartInfo.RedirectStandardInput = true;//是否可以输入
                process_cmd.StartInfo.RedirectStandardOutput = true;//是否可以输出

                process_cmd.StartInfo.CreateNoWindow = true;//不创建窗体 也就是隐藏窗体
                process_cmd.StartInfo.UseShellExecute = false;//是否使用系统shell执行，否

                process_cmd.Start();
                var url = ConfigHelper.GetAppSetting("AutoOpenUrl");
                process_cmd.StandardInput.WriteLine($"cd C:/Program Files (x86)/Microsoft/Edge/Application && msedge.exe --kiosk {url} --edge-kiosk-type=fullscreen");
                //process_cmd.StandardInput.WriteLine($"cd C:/Program Files (x86)/Microsoft/Edge/Application && msedge.exe --kiosk {url} --edge-kiosk-type=fullscreen  --kiosk-idle-timeout-minutes=1");
                //process_cmd.StandardInput.WriteLine($"start microsoft-edge:{url} --kiosk --edge-kiosk-type=fullscreen");
                //Thread.Sleep(3000);//延时

            }
            else
            {
                //浏览器打开了，但是网页没打开，打开网页
                WinAPI.SetWindowsTop(ConfigHelper.GetAppSetting("WebTitle"));

            }






            //4、鼠标点击
            int.TryParse(ConfigHelper.GetAppSetting("ClickPositionX"), out int X);
            int.TryParse(ConfigHelper.GetAppSetting("ClickPositionY"), out int Y);
            WinAPI.SetCursorPos(X, Y);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);


            Debug.Print(DateTime.Now.ToLongTimeString());
            //timer.Change

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.notifyIcon1.Visible = false;
        }



        private void txt_WebTitle_TextChanged(object sender, EventArgs e)
        {
            //File.Create("webTitle.txt").Close();
            //File.WriteAllText("webTitle.txt", textBox1.Text.Trim());
            ConfigHelper.AddAppSetting("WebTitle", textBox1.Text.Trim());
            webTitle = textBox1.Text.Trim();

        }

        private void cb_Startup_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Startup.Checked)
            {
                ShortcutManagement.CreateShort();
            }
            else
            {
                ShortcutManagement.DeleteShort();


            }

        }
    }
}

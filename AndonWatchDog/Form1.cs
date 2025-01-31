using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
                if (File.Exists("interval.txt"))
                {
                    int.TryParse(System.IO.File.ReadAllLines("interval.txt")[0], out int tmpInterval);

                    interval = 1000 * 60 * tmpInterval;
                    nud_interval.Value = tmpInterval;
                }
                else
                {
                    File.Create("interval.txt").Close();
                    File.WriteAllText("interval.txt", "10");

                }

                if (File.Exists("webTitle.txt"))
                {
                    webTitle = System.IO.File.ReadAllLines("webTitle.txt")[0];
                    textBox1.Text = webTitle;
                }
                else
                {
                    File.Create("webTitle.txt").Close();
                    File.WriteAllText("webTitle.txt", "百度一下，你就知道");

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
            if (MessageBox.Show("确定要关闭程序吗？\r\n[OK]关闭程序\r\n[Cancel]取消关闭\r\n提示：可最小化窗口并显示在通知栏！", "Andon WatchDog", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //this.notifyIcon1.Visible = true;

                //e.Cancel = true;
            }
            else
            {
                e.Cancel = true;
                //if (notifyIcon1.Visible)
                //{
                //    this.WindowState = FormWindowState.Minimized;
                //    this.notifyIcon1.Visible = true;

                //}

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
        }

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

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;

            //if (this.WindowState == FormWindowState.Normal)
            //{
            //    this.WindowState = FormWindowState.Minimized;
            //    this.ShowInTaskbar = false;

            //}
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    this.WindowState = FormWindowState.Normal;
            //    this.ShowInTaskbar = true;

            //}
            //this.notifyIcon1.Visible = false;

        }

        private void nud_interval_ValueChanged(object sender, EventArgs e)
        {
            this.interval = (int)nud_interval.Value * 1000 * 60;

            File.WriteAllText("interval.txt", ((int)nud_interval.Value).ToString());



            if (btn_Stop.Enabled == true && btn_Start.Enabled == false)
            {
                var result = timer?.Change(interval, interval);
                Debug.Print("nud_interval_ValueChanged:" + result.ToString());

            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            timer = new System.Threading.Timer(timer_Callback, null, interval, interval);
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer_Callback(object state)
        {

            SetWindowsTop();
            Debug.Print(DateTime.Now.ToLongTimeString());
            //timer.Change

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.notifyIcon1.Visible = false;
        }












        private WindowPattern GetWindowPattern(AutomationElement targetControl)
        {
            WindowPattern windowPattern = null;

            try
            {
                windowPattern = targetControl.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
            }
            catch (InvalidOperationException)
            {
                // object doesn't support the WindowPattern control pattern
                return null;
            }
            // Make sure the element is usable.
            if (false == windowPattern.WaitForInputIdle(10000))
            {
                windowPattern.SetWindowVisualState(WindowVisualState.Maximized);

                // Object not responding in a timely manner
                return null;
            }
            return windowPattern;
        }




        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        private void SetWindowsTop()
        {
            Process[] procsEdge = Process.GetProcessesByName("msedge");
            Debug.WriteLine("procsEdge:" + procsEdge.Length);

            foreach (Process process in procsEdge)
            {
                // the chrome process must have a window
                if (process.MainWindowHandle == IntPtr.Zero)
                {
                    ShowWindow(process.MainWindowHandle, 1);

                    continue;
                }

                AutomationElementCollection roots = AutomationElement.RootElement.FindAll(TreeScope.Element | TreeScope.Children,
                    new AndCondition(new PropertyCondition(AutomationElement.ProcessIdProperty, process.Id),
                    new PropertyCondition(AutomationElement.ClassNameProperty, "Chrome_WidgetWin_1")));
                Debug.WriteLine("roots:" + roots.Count);

                foreach (AutomationElement rootElement in roots)
                {
                    Debug.WriteLine("rootElement:" + rootElement.ToString());

                    AutomationElement address = rootElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, webTitle));

                    if (address == null)
                    {
                                            


                        address = rootElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, webTitle));
                        if (address == null)
                        {
                            continue;
                        }
                    }




                    //TextPattern textPattern = (TextPattern)address.GetCurrentPattern(TextPattern.Pattern);
                    //string url = textPattern.DocumentRange.GetText(-1);
                    //Console.WriteLine("Edge Browser URL: " + url);



                    // 最大化窗口
                    ShowWindow(process.MainWindowHandle, 3);
                    address.SetFocus();
                    // var v = address.GetCurrentPattern(ValuePattern.Pattern);
                    //ValuePattern v = (ValuePattern)address.GetCurrentPattern(ValuePattern.Pattern);
                    //Debug.WriteLine("type:" + v.GetType());
                    //if (v.Current.Value != null && v.Current.Value != "")
                    //{

                    //    Debug.WriteLine("URL:" + v.Current.Value);
                    //}

                    // 获取地址栏的文本值
                    //ValuePattern valuePattern = address.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                    //if (valuePattern != null)
                    //{
                    //    string url = valuePattern.Current.Value;
                    //    Console.WriteLine("当前Edge浏览器的URL是: " + url);
                        
                    //}


                }

            }

        }

        private void txt_WebTitle_TextChanged(object sender, EventArgs e)
        {
            File.Create("webTitle.txt").Close();
            File.WriteAllText("webTitle.txt", textBox1.Text.Trim());
            webTitle= textBox1.Text.Trim();
        }
    }
}

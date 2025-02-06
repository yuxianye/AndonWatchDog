using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace AndonWatchDog
{
    public class WinAPI
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        //移动鼠标 
        public const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);





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


        public static void SetWindowsTop(string webTitle)
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



        public static void FindEdgePageByTitle(string webTitle)
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




    }


    //MouseHelper.SetCursorPos(Form1.point.X, Form1.point.Y);
    //MouseHelper.mouse_event(MouseHelper.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
    //MouseHelper.mouse_event(MouseHelper.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

















}

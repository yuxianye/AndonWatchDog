using AndonWatchDog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AndonWatchDog
{
    public class DisplayControl
    {
        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint wMsg, int wParam, int lParam);
        const uint WM_SYSCOMMAND = 0x0112;
        const int SC_MONITORPOWER = 0xF175;
        const int MONITOR_ON = -1;
        const int MONITOR_OFF = 2;
        const int MONITOR_LOW = 1;

        public static void TurnOnDisplay()
        {
            int r = SendMessage((IntPtr)0xFFFF, WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_ON);
            Logger.Info($"TurnOnDisplay{r}");
        }
        public static void TurnOffDisplay()
        {
            int r = SendMessage((IntPtr)0xFFFF, WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_OFF);
            Logger.Info($"TurnOffDisplay{r}");
        }
        public static void TurnLowDisplay()
        {
            int r = SendMessage((IntPtr)0xFFFF, WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_LOW);

            Logger.Info($"TurnLowDisplay{r}");
        }
    }

}


[Flags]
enum ExecutionState : uint
{
    SystemRequired = 0x01,
    DisplayRequired = 0x02,
    Continuous = 0x80000000
}
public class SystemSleepManagement
{
    [DllImport("kernel32.dll")]
    static extern uint SetThreadExecutionState(ExecutionState flags);
    public static void PreventSleep()
    {
        SetThreadExecutionState(ExecutionState.SystemRequired | ExecutionState.Continuous);
    }
    public static void RestoreSleep()
    {
        SetThreadExecutionState(ExecutionState.Continuous);
    }
}
//class Program
//{
//    static void Main(string[] args)
//    {
//        // 防止系统进入休眠状态
//        SystemSleepManagement.PreventSleep();
//        // 关闭显示器
//        DisplayControl.TurnOffDisplay();
//        Console.WriteLine("显示器已关闭");
//        // 模拟一些操作
//        System.Threading.Thread.Sleep(5000);
//        // 打开显示器
//        DisplayControl.TurnOnDisplay();
//        Console.WriteLine("显示器已开启");
//        // 恢复系统休眠策略
//        SystemSleepManagement.RestoreSleep();
//    }
//}
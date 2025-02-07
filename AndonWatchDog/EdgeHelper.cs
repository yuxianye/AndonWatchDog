using System.Diagnostics;

namespace AndonWatchDog
{
    public class EdgeHelper
    {

        public static void StartFullScreenEdge(string url)
        {

            Process process_cmd = new Process();
            process_cmd.StartInfo.FileName = "cmd.exe";//进程打开文件名为“cmd”

            process_cmd.StartInfo.RedirectStandardInput = true;//是否可以输入
            process_cmd.StartInfo.RedirectStandardOutput = true;//是否可以输出

            process_cmd.StartInfo.CreateNoWindow = true;//不创建窗体 也就是隐藏窗体
            process_cmd.StartInfo.UseShellExecute = false;//是否使用系统shell执行，否

            process_cmd.Start();
            process_cmd.StandardInput.WriteLine($"cd C:/Program Files (x86)/Microsoft/Edge/Application && msedge.exe --kiosk {url} --edge-kiosk-type=fullscreen");
            //process_cmd.StandardInput.WriteLine($"cd C:/Program Files (x86)/Microsoft/Edge/Application && msedge.exe --kiosk {url} --edge-kiosk-type=fullscreen  --kiosk-idle-timeout-minutes=1");
            //process_cmd.StandardInput.WriteLine($"start microsoft-edge:{url} --kiosk --edge-kiosk-type=fullscreen");
            System.Threading.Thread.Sleep(3000);//延时
        }

    }
}

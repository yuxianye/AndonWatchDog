using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AndonWatchDog
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            createShort();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }


        private static void createShort()
        {
            string shortName = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\AndonWatchDog.exe.lnk";

            if (!File.Exists(shortName))
            {
                //string sourceName = AppDomain.CurrentDomain.BaseDirectory +"AndonWatchDog.exe";
                string sourceName = Assembly.GetExecutingAssembly().Location;

                ShortcutCreator.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "AndonWatchDog.exe", sourceName);

            }

        }













    }
}

﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AndonWatchDog
{
    internal static class Program
    {
        /// <summary>
        /// Name of mutex to use. Should be unique for all applications.
        /// </summary>
        public const string MutexName = "FormsApplicationMutex";

        /// <summary>
        /// Sets the foreground window.
        /// </summary>
        /// <param name="hWnd">Window handle to bring to front.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);


        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Try to grab mutex
            bool createdNew;
            using (var mutex = new Mutex(true, MutexName, out createdNew))
            {
                if (createdNew)
                {
                    ShortcutManagement.CreateShort();
                    // We got the mutex and start the application
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new AndonWatchDog.MainForm());
                }
                else
                {
                    // Bring other instance to front and exit.
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            ShowWindow(process.MainWindowHandle, 5);
                            
                            SetForegroundWindow(process.MainWindowHandle);
                            break;
                        }
                    }
                }
            }
        }















    }
}

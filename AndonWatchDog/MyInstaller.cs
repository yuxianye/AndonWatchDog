using IWshRuntimeLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AndonWatchDog
{
    [RunInstaller(true)]
    public partial class MyInstaller : System.Configuration.Install.Installer
    {
        public MyInstaller()
        {
            InitializeComponent();
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            //var p = Process.GetProcessesByName("AndonWatchDog");
            //if (p != null && p.Any())
            //{
            //    foreach (var v in p)
            //    {
            //        v.Kill();
            //        Logger.Info($"kill process {v.ProcessName}");

            //    }
            //}


            base.OnBeforeInstall(savedState);


            var p = Process.GetProcessesByName("AndonWatchDog");
            if (p != null && p.Any())
            {
                foreach (var v in p)
                {
                    v.Kill();
                    Logger.Info($"kill process {v.ProcessName}");

                }
            }

        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            ShortcutManagement.DeleteShort();

            base.OnAfterInstall(savedState);
            string path = this.Context.Parameters["targetdir"];

            Process.Start(@"C:\AndonWatchDog_Assembly\AndonWatchDog.exe");



            Logger.Info("start process ");

        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);

        }
        protected override void OnAfterUninstall(IDictionary savedState)
        {
            base.OnAfterUninstall(savedState);
            //System.IO.Directory.Delete(@"C:\AndonWatchDog_Assembly\", true);/
            ShortcutManagement.DeleteShort();
            var p = Process.GetProcessesByName("AndonWatchDog");
            if (p != null && p.Any())
            {
                foreach (var v in p)
                {
                    v.Kill();
                    Logger.Info($"kill process {v.ProcessName}");

                }
            }
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
            //System.IO.Directory.Delete(@"C:\AndonWatchDog_Assembly\", true);
        }

    }
}

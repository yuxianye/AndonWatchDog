﻿using IWshRuntimeLibrary;
using System;
using System.IO;
using System.Reflection;
namespace AndonWatchDog
{



    /// <summary>
    /// 快捷方式管理的类
    /// </summary>
    /// <remarks></remarks>
    public class ShortcutManagement
    {
        //需要引入IWshRuntimeLibrary，搜索Windows Script Host Object Model

        /// <summary>
        /// 创建快捷方式
        /// </summary>
        /// <param name="directory">快捷方式所处的文件夹</param>
        /// <param name="shortcutName">快捷方式名称</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="description">描述</param>
        /// <param name="iconLocation">图标路径，格式为"可执行文件或DLL路径, 图标编号"，
        /// 例如System.Environment.SystemDirectory + "\\" + "shell32.dll, 165"</param>
        /// <remarks></remarks>
        public static void CreateShortcut(string directory, string shortcutName, string targetPath,
            string description = null, string iconLocation = null)
        {
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);//创建快捷方式对象
            shortcut.TargetPath = targetPath;//指定目标路径
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);//设置起始位置
            shortcut.WindowStyle = 1;//设置运行方式，默认为常规窗口
            shortcut.Description = description;//设置备注
            shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;//设置图标路径
            shortcut.Save();//保存快捷方式
        }

        /// <summary>
        /// 创建桌面快捷方式
        /// </summary>
        /// <param name="shortcutName">快捷方式名称</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="description">描述</param>
        /// <param name="iconLocation">图标路径，格式为"可执行文件或DLL路径, 图标编号"</param>
        /// <remarks></remarks>
        public static void CreateShortcutOnDesktop(string shortcutName, string targetPath,
            string description = null, string iconLocation = null)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//获取桌面文件夹路径
            CreateShortcut(desktop, shortcutName, targetPath, description, iconLocation);
        }


        public static void CreateShort()
        {
            string shortName = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\AndonWatchDog.exe.lnk";

            if (!System.IO.File.Exists(shortName))
            {
                //string sourceName = AppDomain.CurrentDomain.BaseDirectory +"AndonWatchDog.exe";
                string sourceName = Assembly.GetExecutingAssembly().Location;

                ShortcutManagement.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "AndonWatchDog.exe", sourceName);

            }

        }

        public static void DeleteShort()
        {
            string shortName = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\AndonWatchDog.exe.lnk";

            if (System.IO.File.Exists(shortName))
            {
                System.IO.File.Delete(shortName);

            }

        }




    }

}

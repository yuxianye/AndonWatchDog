using System.Configuration;

namespace AndonWatchDog
{
    public class ConfigHelper
    {
        //private static readonly System.Configuration.Configuration config =
        //   ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        /// <summary>
        /// 增加AppSetting配置节的配置内容，如果存在同名key,则覆盖
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddAppSetting(string key, string value)
        {

            System.Configuration.Configuration config =
         ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }
            config.Save();
            ConfigurationManager.RefreshSection(@"appSettings");// 刷新命名节，在下次检索它时将从磁盘重新读取它。记住应用程序要刷新节点
        }

        /// <summary>
        /// 取得AppSetting配置节的配置内容
        /// </summary>
        /// <param name="key">配置节的key</param>
        /// <returns>string类型的配置内容</returns>
        public static string GetAppSetting(string key)
        {
            ConfigurationManager.RefreshSection(@"appSettings");// 刷新命名节，在下次检索它时将从磁盘重新读取它。记住应用程序要刷新节点

            return ConfigurationManager.AppSettings[key]?.ToString();
            //return ConfigurationManager.AppSettings.Get(key);
        }


    }
}

using System;
using System.DirectoryServices.ActiveDirectory;

namespace AndonWatchDog
{
    public class DomainHelper
    {

        public static string GetFullMachineName()
        {
            string result = string.Empty;
            try
            {
                if (IsComputerJoinedToDomain())
                {
                    result = Environment.MachineName + "." + Domain.GetComputerDomain().Name;
                }
                else
                {
                    result = Environment.MachineName;
                }

                //string dn = Domain.GetComputerDomain().Name;    //dn=abc.com
                //Console.WriteLine("GetComputerDomain: " + dn);

                //string na = System.Net.Dns.GetHostEntry("LocalHost").HostName;
                //Console.WriteLine("GetHostEntry: " + na);

                //string computerName = Environment.MachineName;
                //Console.WriteLine("本地计算机名称: " + computerName);
                //string c = Environment.GetEnvironmentVariable("computername");
                //Debug.Print("GetEnvironmentVariable:" + c);

                //string d = Dns.GetHostEntry("localhost").HostName;
                //Debug.Print("GetHostEntry:" + d);


            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return result;

        }


        public static bool IsComputerJoinedToDomain()
        {
            try
            {
                // 获取当前计算机所在的域
                Domain currentDomain = Domain.GetComputerDomain();
                return true;
            }
            catch (ActiveDirectoryObjectNotFoundException)
            {
                // 如果计算机未加入域，会抛出 ActiveDirectoryObjectNotFoundException 异常
                return false;
            }
            catch (Exception ex)
            {
                // 处理其他可能的异常
                Console.WriteLine($"发生错误: {ex.Message}");
                return false;
            }
        }

    }
}

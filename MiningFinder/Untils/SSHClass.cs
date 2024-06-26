using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiningFinder.Untils
{
    public class SSHClass
    {
        public delegate void GetSSHLog(string log);
        public static event GetSSHLog getSSHLog;
        /// <summary>
        /// SSH登录远程Linux服务器，并运行指令
        /// </summary>
        /// <param name="host">远程Linux服务器IP或域名</param>
        /// <param name="username">账号名</param>
        /// <param name="password">账号密码</param>
        /// <param name="command">命令</param>
        /// <returns></returns>
        public static void RunSSHCommands(string host, string username, string password, string command)
        {
            if (command == null || command.Length == 0)
            {
                getSSHLog("指令为空!");
            }
            try
            {
                using (var client = new SshClient(host, username, password))
                {
                    try
                    {
                        client.Connect();
                        string result = client.RunCommand(command).Execute();
                        var topCPU=LinuxConsolHelper.ClearPsResult(result);
                        
                        if(null!=topCPU)
                        {
                            getSSHLog($"服务器：{host};CPU占用率:{topCPU.Cpu};占用进程:{topCPU.Command}");
                        }
                        client.Disconnect();
                    }
                    catch (Exception e)
                    {
                        getSSHLog($"服务器：{host}出现错误：{e.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                getSSHLog($"服务器：{host}出现错误：{e.Message}");
            }
           
        }
    }
}

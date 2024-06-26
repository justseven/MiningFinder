using MiningFinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiningFinder.Untils
{
    public class LinuxConsolHelper
    {
        public static PSInfo ClearPsResult(string output)
        {
            // 解析输出
            var lines = output.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(1);
            var processes = lines.Select(line =>
            {
                var columns = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return new PSInfo
                {
                    User = columns[0],
                    Pid = columns[1],
                    Cpu = double.Parse(columns[2]),
                    Command = string.Join(" ", columns.Skip(10))
                };
            });

            // 找到占用 CPU 最高的程序
            var topProcess = processes.OrderByDescending(p => p.Cpu).FirstOrDefault();
            if (topProcess != null)
            {
                return topProcess;
            }
            return null;
        }
    }
}

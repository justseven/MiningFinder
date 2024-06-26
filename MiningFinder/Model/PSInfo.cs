using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiningFinder.Model
{
    public class PSInfo
    {
        string user;
        string pid;
        double cpu;
        string command;

        public string User { get => user; set => user = value; }
        public string Pid { get => pid; set => pid = value; }
        public double Cpu { get => cpu; set => cpu = value; }
        public string Command { get => command; set => command = value; }
    }
}

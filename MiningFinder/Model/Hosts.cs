using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiningFinder.Model
{
    public class Hosts
    {
        private string iP;
        private int port;
        private string userName;
        private string password;
        private string priveKey;
        private string description;

        public string IP { get => iP; set => iP = value; }
        public int Port { get => port; set => port = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string PriveKey { get => priveKey; set => priveKey = value; }
        public string Description { get => description; set => description = value; }
    }

    public class ShowItem
    {
        private string iP;
        private string description;
        private Hosts hosts;

        public string IP { get => iP; set => iP = value; }
        public string Description { get => description; set => description = value; }
        public Hosts HostsItem { get => hosts; set => hosts = value; }
    }
}

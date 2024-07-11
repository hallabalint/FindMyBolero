using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyBolero
{
     class Antenna
        {
        static int count = 0;
        private int id;
        private string ip;
        private string name;
        public bool IsOnline = false;
        
        public Antenna(string ip, string name)
        {
            this.ip = ip;
            this.name = name;
            this.id = ++count;
        }

        public int Id { get { return id; } }
        public string Name
        {
            get { return name; }
            set
            {
                Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("FindMyBolero").OpenSubKey("Antennas").DeleteSubKey(name);
                name = value;
                Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("FindMyBolero").OpenSubKey("Antennas").SetValue(name, ip);
            }
        }

        public string Ip
        {
            get { return ip; } 
            set
            {
                ip = value;
                //set registry
                Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("FindMyBolero").OpenSubKey("Antennas").SetValue(name, ip);
            }
        }

        public string Status
        {
            get { if (IsOnline && this == Caller.Active) {
                    return "Available (Selected)";
                } else if (IsOnline) { return "Available"; }
                return "Not available";
            }
        }
        
    }
}

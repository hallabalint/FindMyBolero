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
                using (var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FindMyBolero\Antennas"))
                {
                    if (key != null)
                    {
                        key.DeleteValue(this.name, false);
                        this.name = value;
                        key.SetValue(this.name, this.ip);
                    }
                }
            }
        }

        public string Ip
        {
            get { return ip; }
            set
            {
                this.ip = value;
                using (var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FindMyBolero\Antennas"))
                {
                    if (key != null)
                    {
                        key.SetValue(this.name, this.ip);
                    }
                }
            }
        }

        public string Status
        {
            get
            {
                if (IsOnline && this == Caller.Active)
                {
                    return "Available (Selected)";
                }
                else if (IsOnline)
                {
                    return "Available";
                }
                return "Not available";
            }
        }
    }
}
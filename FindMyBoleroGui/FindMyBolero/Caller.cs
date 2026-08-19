using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindMyBolero
{
    internal static class Caller
    {
        public static AutoResetEvent pingSuccessfulEvent = new AutoResetEvent(false);
        public static AutoResetEvent close = new AutoResetEvent(false);
        public static List<Antenna> antennas;
        public static Antenna Active;
        public static ControllForm cf;
        public static bool successfulUpdate = false;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "FindMyBolero", out createdNew))
            {
                if (createdNew)
                {
                    antennas = new List<Antenna>();
                    var icon = new NotifyIcon();

                    try
                    {
                        using (var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FindMyBolero\Antennas"))
                        {
                            if (key != null)
                            {
                                string[] keys = key.GetValueNames();
                                foreach (string name in keys)
                                {
                                    string ip = key.GetValue(name)?.ToString();
                                    if (!string.IsNullOrEmpty(ip))
                                    {
                                        antennas.Add(new Antenna(ip, name));
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Parser error");
                        Debug.WriteLine(e.Message);
                    }

                    // To customize application configuration such as set high DPI settings or default font,
                    // see https://aka.ms/applicationconfiguration.
                    ApplicationConfiguration.Initialize();
                    cf = new ControllForm();
                    Task.Factory.StartNew(() => HttpServer.startServer());
                    Application.Run(cf);
                }
                else
                {
                    string message = "One instance is already running";
                    string title = "FindMyBolero";
                    MessageBox.Show(message, title);
                    // Opcionális: előtérbe hozhatod a már futó folyamatot
                }
            }
        }

        public static void PingAntennas()
        {
            Task.Run(() => Ping.makePings());
            pingSuccessfulEvent.WaitOne();

            if (Active == null || !Active.IsOnline)
            {
                Active = antennas.Find(x => x.IsOnline);
            }
            cf.DataRefreh();
        }

        public static void updateCachedAntennas()
        {
            if (successfulUpdate) return;
            try
            {
                string url = string.Empty;

              
                using (var baseKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FindMyBolero"))
                {
                    url = baseKey.GetValue("AntennaDB")?.ToString();
                }

                if (string.IsNullOrEmpty(url))
                {
                    Debug.WriteLine("No AntennaDB URL found in Registry.");
                    return;
                }

                
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);

                    var dict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    if (dict == null || dict.Count == 0) return;

                   
                    List<Antenna> newAntennas = dict
                        .Select(kvp => new Antenna(ip: kvp.Value, name: kvp.Key))
                        .ToList();

                    if (newAntennas.Count == 0) return;

                   
                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FindMyBolero\Antennas"))
                    {
                        foreach (string subkey in key.GetValueNames())
                        {
                            key.DeleteValue(subkey, false);
                        }

                       
                        foreach (Antenna antenna in newAntennas)
                        {
                            key.SetValue(antenna.Name, antenna.Ip);
                        }
                    }
                    antennas = newAntennas;
                }

                successfulUpdate = true;
                cf.DataRefreh();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error updating cached antennas");
                Debug.WriteLine(e.Message);
            }
        }
    }
}
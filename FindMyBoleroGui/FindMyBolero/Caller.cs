using Microsoft.Win32;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using System.Resources;
using System.Runtime.InteropServices;

namespace FindMyBolero
{

   
    internal static class Caller
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        static public AutoResetEvent pingSuccessfulEvent = new AutoResetEvent(false);
        static public AutoResetEvent close = new AutoResetEvent(false);
        static public List<Antenna> antennas;
        static public Antenna Active;
        static public ControllForm cf;
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
                        string[] keys = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("FindMyBolero").OpenSubKey("Antennas").GetValueNames();
                        foreach (string key in keys)
                        {
                            string ip = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("FindMyBolero").OpenSubKey("Antennas").GetValue(key).ToString();
                            antennas.Add(new Antenna(ip, key));

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
                    Process current = Process.GetCurrentProcess();
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




    }
}
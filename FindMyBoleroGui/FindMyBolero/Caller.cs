using Microsoft.Win32;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using System.Resources;

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
            antennas = new List<Antenna>();
            var icon = new NotifyIcon();
            //set window icon

            

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

        public static void PingAntennas()
        {
            Task.Run(() => Ping.makePings());
            pingSuccessfulEvent.WaitOne();
            if (Active == null || !Active.IsOnline)
            {
                Active = antennas.Find(x => x.IsOnline);
            }
            

        }




    }
}
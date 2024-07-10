// See https://aka.ms/new-console-template for more information
using Microsoft.Win32;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Threading;
using FindMyBolero;

class Program
{
    static ManualResetEvent pingSuccessfulEvent = new ManualResetEvent(false);
    static List<Thread> threads = new List<Thread>();
    static IPAddress boleroIP;

    static void Main(string[] args)
    {
        string[] keys = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("FindMyBolero").GetValueNames();
        foreach (string key in keys)
        {
            string sip = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("FindMyBolero").GetValue(key).ToString();
            IPAddress ip = IPAddress.Parse(sip);
            Thread thread = new Thread(() => PingAndHandle(ip));
            threads.Add(thread);
            thread.Start();
        }

        // Wait for the pingSuccessfulEvent to be set or timeout 5s
        pingSuccessfulEvent.WaitOne(5000);

        Thread.Sleep(1000);
        // Clean up all threads
        foreach (var thread in threads)
        {
            if (thread.IsAlive)
            {
                //thread abort not supported on all platform

                thread.Interrupt();
            }
        }

        HttpServer.startServer(boleroIP.ToString());
    }

    static void PingAndHandle(IPAddress ip)
    {
        Ping ping = new Ping();
        PingReply reply = ping.Send(ip);

        if (reply.Status == IPStatus.Success)
        {

            // Set the event to indicate a successful ping
            pingSuccessfulEvent.Set();
            boleroIP = ip;
            // Open the address in the browser
            string url = "http://" + ip.ToString();
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }
    }
}

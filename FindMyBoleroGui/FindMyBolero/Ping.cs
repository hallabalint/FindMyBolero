// See https://aka.ms/new-console-template for more information
using Microsoft.Win32;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Threading;
using FindMyBolero;

static class Ping
{



    static public void makePings()
    {
        List<Task> tasks = new List<Task>();

        foreach (Antenna antenna in Caller.antennas)
        {
            tasks.Add(Task.Run(() => PingAndHandle(antenna)));
         }
        Task.WaitAll(tasks.ToArray());
        Caller.pingSuccessfulEvent.Set();
    }

     static void PingAndHandle(Antenna antenna)
    {
        System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
        PingReply reply = ping.Send(antenna.Ip);

        if (reply.Status == IPStatus.Success)
        {
            antenna.IsOnline = true;
        } else
        {
            antenna.IsOnline = false;
        }
    }
}

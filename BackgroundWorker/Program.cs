using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

namespace BackgroundWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                WebClient client = new WebClient();
                client.DownloadString("http://metermaid.apphb.com/execute.aspx");

                Thread.Sleep(60 * 1000);
            }
        }
    }
}

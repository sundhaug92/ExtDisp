using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace conServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener tl = new TcpListener(1957);
            tl.Start();
            var tc = tl.AcceptTcpClient();
            StreamReader sr = new StreamReader(tc.GetStream());
            while (true) Console.WriteLine("Got:"+sr.ReadLine());
        }
    }
}

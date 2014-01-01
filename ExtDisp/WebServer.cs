using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ExtDisp
{
    class WebServer
    {
        TcpListener tl;
        internal void Listen(int p)
        {
            tl = new TcpListener(new IPAddress(new byte[]{0,0,0,0}),p);
            tl.Start();
            Thread t = new Thread(worker);
            t.Start();
        }
        public void worker()
        {
            while (true)
            {
                new Thread(clientHandler).Start(tl.AcceptTcpClient());
            }
        }
        public void clientHandler(object _tc)
        {
            TcpClient tc = (TcpClient)_tc;
            NetworkStream ns = new NetworkStream(tc.Client);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            StreamWriters.Add(sw);
        }
        List<StreamWriter> StreamWriters = new List<StreamWriter>();
        internal void Broadcast(string p)
        {
            foreach (StreamWriter sw in StreamWriters)
            {
                sw.WriteLine(p);
            }
        }
    }
}

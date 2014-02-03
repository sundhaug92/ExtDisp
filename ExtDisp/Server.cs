using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using UnityEngine;

namespace ExtDisp
{
    class Server
    {
        TcpListener tl;
        Thread workerThread;
        internal void Listen(int p)
        {
            MonoBehaviour.print("ExtDisp: Listening");
            workerThread = new Thread(worker);
            tl = new TcpListener(new IPAddress(new byte[] { 0, 0, 0, 0 }), p);
            tl.Start();
            workerThread.Start();
        }
        public void worker()
        {
            MonoBehaviour.print("ExtDisp: Worker");
            while (true)
            {
                new Thread(clientHandler).Start(tl.AcceptTcpClient());
            }
        }
        public void clientHandler(object _tc)
        {
            MonoBehaviour.print("ExtDisp: Got client");
            TcpClient tc = (TcpClient)_tc;
            NetworkStream ns = new NetworkStream(tc.Client);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            StreamReaders.Add(sr);
            StreamWriters.Add(sw);
        }
        List<StreamReader> StreamReaders = new List<StreamReader>();
        List<StreamWriter> StreamWriters = new List<StreamWriter>();
        internal void Broadcast(string p)
        {
            //MonoBehaviour.print("ExtDisp: Broadcasting");
            foreach (StreamWriter sw in StreamWriters)
            {
                sw.WriteLine(p);
            }
        }

        internal void Close()
        {
            MonoBehaviour.print("ExtDisp: Closing");
            tl.Stop();
            workerThread.Abort();
            foreach(StreamWriter sw in StreamWriters)sw.Close();
            foreach(StreamReader sr in StreamReaders)sr.Close();
        }
    }
}
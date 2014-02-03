using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP;
using System.Xml.Serialization;
using System.IO;

namespace ExtDisp
{
    public class ExtDisp : KSP.Testing.UnitTest
    {
        public ExtDisp()
        {
            GameObject ghost=new GameObject("ExtDispGhost",typeof(ExtDispGhost));
            GameObject.DontDestroyOnLoad(ghost);
        }
    }
    public class ExtDispGhost:MonoBehaviour
    {
        public static void WriteDebug(object obj) { print("ExtDispGhost: "+obj); }
        Server ws;
        public void Awake()
        {
            ws = new Server();
            ws.Listen(0);
        }
        
        public void Update()
        {
            if (FlightGlobals.ready && FlightGlobals.ActiveVessel != null)
            {
                XmlSerializer writer = new XmlSerializer(typeof(State));
                StringWriter sw = new StringWriter();
                writer.Serialize(sw, State.Current);
                ws.Broadcast(sw.ToString());
            }
        }
        public void OnApplicationQuit()
        {
            ws.Close();
        }
    }
}

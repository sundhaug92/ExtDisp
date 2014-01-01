using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP;

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
        WebServer ws;
        public void Awake()
        {
            ws = new WebServer();
            ws.Listen(1992);
        }
        
        public void Update()
        {
            if (FlightGlobals.ready && FlightGlobals.ActiveVessel != null)
            {
                Vessel vessel = FlightGlobals.ActiveVessel;

                StringBuilder sb = new StringBuilder();

                sb.Append("{");
                sb.AppendFormat("mainThrottle:{0},", vessel.ctrlState.mainThrottle);
                sb.AppendFormat("X:{0},", vessel.ctrlState.X);
                sb.AppendFormat("Y:{0},", vessel.ctrlState.Y);
                sb.AppendFormat("Z:{0},", vessel.ctrlState.Z);
                sb.AppendFormat("pitch:{0},", vessel.ctrlState.pitch);
                sb.AppendFormat("yaw:{0},", vessel.ctrlState.yaw);
                sb.AppendFormat("roll:{0},", vessel.ctrlState.roll);
                sb.AppendFormat("ApA:{0},", vessel.orbit.ApA);
                sb.AppendFormat("PeA:{0},", vessel.orbit.PeA);
                sb.AppendFormat("Vel:{0}", vessel.orbit.vel);
                sb.Append("}");

                ws.Broadcast(sb.ToString());
                //print(sb);
            }
            //else print("Ready: " + FlightGlobals.ready + " & ActiveVessel:" + FlightGlobals.ActiveVessel);
        }
        public void OnApplicationQuit()
        {
            ws.Stop();
        }
    }
}

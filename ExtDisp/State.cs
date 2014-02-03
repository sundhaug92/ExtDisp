using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP;

namespace ExtDisp
{
    public class State
    {
        public State() { }
        public static State Current
        {
            get
            {
                return new State(FlightGlobals.ActiveVessel);
            }
        }
        public double mainThrottle
        {
            get
            {
                if (vessel == null) return double.NaN;
                else return vessel.ctrlState.mainThrottle;
            }
        }
        public double X
        {
            get
            {
                if (vessel == null) return double.NaN;
                else return vessel.ctrlState.X;
            }
        }
        public double Y
        {
            get
            {
                if (vessel == null) return double.NaN;
                else return vessel.ctrlState.Y;
            }
        }
        public double Z
        {
            get
            {
                if (vessel == null) return double.NaN;
                else return vessel.ctrlState.Z;
            }
        }
        public double Roll
        {
            get
            {
                if (vessel == null) return double.NaN;
                else return vessel.ctrlState.roll;
            }
        }
        public double Pitch
        {
            get
            {
                if (vessel == null) return double.NaN;
                else return vessel.ctrlState.pitch;
            }
        }
        public double Yaw
        {
            get
            {
                if (vessel == null) return double.NaN;
                else return vessel.ctrlState.yaw;
            }
        }
        public double ApA
        {
            get
            {
                if (vessel == null) return double.NaN;
                else return vessel.orbit.ApA;
            }
        }
        public double PeA
        {
            get
            {
                if (vessel == null) return double.NaN;
                else return vessel.orbit.PeA;
            }
        }
        private Vessel vessel;

        public State(Vessel vessel)
        {
            
            this.vessel = vessel;
        }
    }
}

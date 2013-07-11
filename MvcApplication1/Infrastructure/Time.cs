using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudScheduler.Infrastructure
{
    public class Time
    {
        public Time(int h, int m)
        {
            Hour = h;
            Minute = m;
        }
        public Time(double t)
        {
            string time = ""+Math.Round(t, 2, MidpointRounding.AwayFromZero);
            Hour = int.Parse((time).Split('.')[0]);
            Minute = int.Parse((time).Split('.')[1]);

        }
        public Time(string t)
        {
            Hour = int.Parse(t.Split(':')[0]);
            Minute = int.Parse(t.Split(':')[1]);
        }
        public int Hour { get; set; }
        
        public int Minute { get; set; }

        double GetTime()
        {
            return Double.Parse((string)(Hour + "." + Minute));
        }
        public override string  ToString()
        {
            string s="";;
            s += Hour + ":";
            if (Minute / 10 == 0)
                s += "0";
            s += Minute;
            return s;
        }
    }
}
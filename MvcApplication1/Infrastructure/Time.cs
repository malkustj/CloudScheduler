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
            try
            {
                Hour = Math.Abs(int.Parse(t.Split(':')[0]));
                Minute = Math.Abs(int.Parse(t.Split(':')[1]));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Hour = 0;
                Minute = 0;
            }
            if (Hour > 23)
            {
                Hour = 23;
            }
            if (Minute > 59)
            {
                Minute = 59;
            }
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
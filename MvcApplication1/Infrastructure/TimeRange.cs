using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudScheduler.Infrastructure
{
    public class TimeRange
    {
        public TimeRange()
        {
        }
        public TimeRange(String time)
        {
            StartTime = new Time(time.Split('|')[0]);
            StopTime = new Time(time.Split('|')[1]);
        }
        public Time StartTime { get; set; }
        public Time StopTime { get; set; }
        public override string  ToString()
        {
            return StartTime + "|" + StopTime;
        }
    }
}
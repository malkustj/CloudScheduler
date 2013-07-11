using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudScheduler.Infrastructure
{
    public class Schedule
    {
        public Schedule() { }
        public Schedule(String code)
        {
            Days = new Dictionary<DayOfWeek,TimeRange>();
            var codeList = code.Split(',');
            Days.Add(DayOfWeek.Monday, new TimeRange(codeList[0]));
            Days.Add(DayOfWeek.Tuesday, new TimeRange(codeList[1]));
            Days.Add(DayOfWeek.Wednesday, new TimeRange(codeList[2]));
            Days.Add(DayOfWeek.Thursday, new TimeRange(codeList[3]));
            Days.Add(DayOfWeek.Friday, new TimeRange(codeList[4]));
            Days.Add(DayOfWeek.Saturday, new TimeRange(codeList[5]));
            Days.Add(DayOfWeek.Sunday, new TimeRange(codeList[6]));
            if (codeList[7].Length == 0)
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }
            ElasticIp = "";
            if (codeList[8].Length > 0)
            {
                ElasticIp = codeList[8];
            }
        }
        public Dictionary<DayOfWeek, TimeRange> Days{ get; set; }
        public bool IsActive { get; set; }
        public string ElasticIp { get; set; }
        public  override string ToString()
        {
            string s = "";
            foreach (var t in Days)
            {
                s += t.Value + ",";
            }
            if(!IsActive)
            {
                s += "#";
            }
            return s + "," + ElasticIp;
        }
    }
}
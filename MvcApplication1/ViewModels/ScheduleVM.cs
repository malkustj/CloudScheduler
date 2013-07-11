using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CloudScheduler.Infrastructure;
namespace CloudScheduler.ViewModels
{
    public class ScheduleVM
    {
        public ScheduleVM() { }
        public ScheduleVM(Instance i)
        {
            InstanceId = i.Id;
            Schedule s = i.Schedule;
            ElasticIp = s.ElasticIp;
            MonStart = s.Days[DayOfWeek.Monday].StartTime.ToString();
            MonEnd = s.Days[DayOfWeek.Monday].StopTime.ToString();
            TuesStart = s.Days[DayOfWeek.Tuesday].StartTime.ToString();
            TuesEnd = s.Days[DayOfWeek.Tuesday].StopTime.ToString();
            WedStart = s.Days[DayOfWeek.Wednesday].StartTime.ToString();
            WedEnd = s.Days[DayOfWeek.Wednesday].StopTime.ToString();
            ThurStart = s.Days[DayOfWeek.Thursday].StartTime.ToString();
            ThurEnd = s.Days[DayOfWeek.Thursday].StopTime.ToString();
            FriStart = s.Days[DayOfWeek.Friday].StartTime.ToString();
            FriEnd = s.Days[DayOfWeek.Friday].StopTime.ToString();
            SatStart = s.Days[DayOfWeek.Saturday].StartTime.ToString();
            SatEnd = s.Days[DayOfWeek.Saturday].StopTime.ToString();
            SunStart = s.Days[DayOfWeek.Sunday].StartTime.ToString();
            SunEnd = s.Days[DayOfWeek.Sunday].StopTime.ToString();
        }
        public string InstanceId { get; set; }

        public string ElasticIp { get; set; }

        public string MonStart { get; set; }

        public string MonEnd { get; set; }

        public string TuesStart { get; set; }

        public string TuesEnd { get; set; }

        public string WedStart { get; set; }

        public string WedEnd { get; set; }

        public string ThurStart { get; set; }

        public string ThurEnd { get; set; }

        public string FriStart { get; set; }

        public string FriEnd { get; set; }

        public string SatStart { get; set; }

        public string SatEnd { get; set; }

        public string SunStart { get; set; }

        public string SunEnd { get; set; }

        public string InstanceString()
        {
            string inst = MonStart + "|" + MonEnd + "," + TuesStart + "|" + TuesEnd + "," + WedStart + "|" + WedEnd + "," + ThurStart + "|" + ThurEnd + "," + FriStart + "|" + FriEnd + "," + SatStart + "|" + SatEnd + "," + SunStart + "|" + SunEnd + ",," + ElasticIp;
            return inst;
        }

    }
}
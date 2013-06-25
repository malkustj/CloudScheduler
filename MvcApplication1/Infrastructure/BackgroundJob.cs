using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CloudScheduler.ViewModels;
namespace CloudScheduler.Infrastructure
{
    public class BackgroundJob
    {
        AmazonService aws = new AmazonService();
        public BackgroundJob()
        {
            execute();
        }
        public void execute()
        {
            var aws = new AmazonService();
            List<Instance> Instances = aws.getInstanceList();
            foreach (Instance i in Instances)
            {
                var instanceStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i.RunTime[0], i.RunTime[1], DateTime.Now.Second);
                var instanceStopTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i.EndTime[0], i.EndTime[1], DateTime.Now.Second);
                if (instanceStartTime.Hour.CompareTo(instanceStopTime.Hour) > 0)
                {
                    instanceStopTime.AddDays(1);
                }
                if (DateTime.Now.CompareTo(instanceStartTime) > 0 && DateTime.Now.CompareTo(instanceStopTime) <= 0 && i.Days.Contains(DateTime.Now.DayOfWeek))
                {
                    if (i.State != "running")
                    {
                        aws.StartInstance(i.Id);
                    }
                }
                else
                {
                    if (i.State != "stopped")
                    {
                        aws.StopInstance(i.Id);
                    }
                }
            } 
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CloudScheduler.ViewModels;
namespace CloudScheduler.Infrastructure
{
    public class BackgroundJob
    {
        public BackgroundJob()
        {
            Execute();
        }
        public void Execute()
        {
            List<Instance> Instances = AmazonService.GetInstanceList();
            var atr = new AWSTagRepository();
            foreach (Instance i in Instances)
            {
                if (i.State == "running" && AmazonService.ValidateElasticIp(i.Schedule.ElasticIp) != "")
                {
                    if (AmazonService.GetElasticIpInstance(i.Schedule.ElasticIp) == "" || AmazonService.GetElasticIpInstance(i.Schedule.ElasticIp) == i.Id)
                    {
                        AmazonService.AllocateElasticIp(i.Id, i.Schedule.ElasticIp);
                    }
                    else
                    {
                        i.Schedule.ElasticIp = "";
                    }
                }
                if (!(i.Schedule.Days[DateTime.Today.DayOfWeek].StartTime.Equals(i.Schedule.Days[DateTime.Today.DayOfWeek].StopTime) || !i.Schedule.IsActive))
                {
                    var instanceStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i.Schedule.Days[DateTime.Today.DayOfWeek].StartTime.Hour, i.Schedule.Days[DateTime.Today.DayOfWeek].StartTime.Minute, DateTime.Now.Second);
                    var instanceStopTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i.Schedule.Days[DateTime.Today.DayOfWeek].StopTime.Hour, i.Schedule.Days[DateTime.Today.DayOfWeek].StopTime.Minute, DateTime.Now.Second);
                    if (instanceStartTime.Hour.CompareTo(instanceStopTime.Hour) > 0)
                    {
                        instanceStopTime.AddDays(1);
                    }
                    if (DateTime.Now.CompareTo(instanceStartTime) > 0 && DateTime.Now.CompareTo(instanceStopTime) <= 0)
                    {
                        if (i.State == "stopped")
                        {
                            AmazonService.StartInstance(i.Id);
                        }
                    }
                    else
                    {
                        if (i.State == "running")
                        {
                            AmazonService.StopInstance(i.Id);
                        }
                    }
                }
                atr.Encode(i);
            } 
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudScheduler.Infrastructure
{
    public class AWSTagRepository : IScheduleRepository 
    {
        
        public string DefaultSchedule = "8:00|18:00,8:00|18:00,8:00|18:00,8:00|18:00,8:00|18:00,0:00|0:00,0:00|0:00,,";
        public Schedule Default()
        {
            return new Schedule(DefaultSchedule);
        }
        public void Encode(Instance instance)
        {
            AmazonService.WriteToTag(instance.Id, "Schedule", instance.Schedule.ToString(), false);
        }
        public Schedule Decode(Instance instance)
        {
            Schedule schedule;
            if (AmazonService.ReadTag(instance.Id, "Schedule") != "")
            {
                try
                {
                    string originalCode = AmazonService.ReadTag(instance.Id, "Schedule");
                    string scheduleCode = originalCode.Substring(0, originalCode.Length - originalCode.Split(',')[8].Length);
                    if (AmazonService.ValidateElasticIp(originalCode.Split(',')[8]) == "")
                    {
                        schedule = new Schedule(scheduleCode + AmazonService.GetInstanceElasticIp(instance.Id));
                        AmazonService.WriteToTag(instance.Id, "Schedule", schedule.ToString(), false);
                    }
                    else
                    {
                        schedule = new Schedule(scheduleCode + AmazonService.ValidateElasticIp(originalCode.Split(',')[8]));
                        AmazonService.WriteToTag(instance.Id, "Schedule", schedule.ToString(), false);
                    }
                    
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    schedule = new Schedule(DefaultSchedule);
                    AmazonService.WriteToTag(instance.Id, "Schedule", DefaultSchedule, true);
                } 
            }
            else
            {
                schedule = new Schedule(DefaultSchedule);
                AmazonService.WriteToTag(instance.Id, "Schedule", DefaultSchedule, true);
            }
            return schedule;
        }
    }
}
using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using CloudScheduler.Infrastructure;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace CloudScheduler.Infrastructure
{
    public class AmazonService : ICloudService
    {
        private AmazonEC2 ec2 = AWSClientFactory.CreateAmazonEC2Client();
        public List<Instance> getInstanceList()
        {
            List<Instance> currentList = new List<Instance>();
            var instanceList = GetInstances();
            foreach (Reservation reservation in instanceList.Reservation)
            {
                var instance = new Instance
                {
                    //instanceList.Reservation[i].RunningInstance.First<RunningInstance>().InstanceId;
                    Id = reservation.RunningInstance[0].InstanceId,
                    State = reservation.RunningInstance[0].InstanceState.Name,
                    Name = reservation.RunningInstance[0].Tag[0].Value,
                    RunTime = new int[2],
                    EndTime = new int[2],
                    Days = new List <DayOfWeek>(),
                };
                foreach (Tag tag in reservation.RunningInstance[0].Tag)
                {
                    if (tag.Key == "Schedule")
                    {
                        instance.RunTime[0] = int.Parse(tag.Value.Split('@')[0].Split('|')[0].Split(':')[0]);
                        instance.RunTime[1] = int.Parse(tag.Value.Split('@')[0].Split('|')[0].Split(':')[1]);
                        instance.EndTime[0] = int.Parse(tag.Value.Split('@')[0].Split('|')[1].Split(':')[0]);
                        instance.EndTime[1] = int.Parse(tag.Value.Split('@')[0].Split('|')[1].Split(':')[1]);
                        foreach (string day in tag.Value.Split('@')[1].Split(','))
                        {
                            if (String.Compare(day, "S", true) == 0)
                            {
                                instance.Days.Add(DayOfWeek.Sunday);
                            }
                            if (String.Compare(day, "M", true) == 0)
                            {
                                instance.Days.Add(DayOfWeek.Monday);
                            }
                            if (String.Compare(day, "T", true) == 0)
                            {
                                instance.Days.Add(DayOfWeek.Tuesday);
                            }
                            if (String.Compare(day, "W", true) == 0)
                            {
                                instance.Days.Add(DayOfWeek.Wednesday);
                            }
                            if (String.Compare(day, "Th", true) == 0)
                            {
                                instance.Days.Add(DayOfWeek.Thursday);
                            }
                            if (String.Compare(day, "F", true) == 0)
                            {
                                instance.Days.Add(DayOfWeek.Friday);
                            }
                            if (String.Compare(day, "Sa", true) == 0)
                            {
                                instance.Days.Add(DayOfWeek.Saturday);
                            }
                        }
                    }
                    if (tag.Key == "Name")
                    {
                        instance.Name = tag.Value;
                    }
                }
                currentList.Add(instance);
            }
            return currentList;
        }
        private DescribeInstancesResult GetInstances()
        {
            return new AmazonEC2Client(RegionEndpoint.USEast1).DescribeInstances(new DescribeInstancesRequest()).DescribeInstancesResult;
        }
        public void StartInstance(string instanceID)
        {
            ec2.StartInstances(new StartInstancesRequest().WithInstanceId(instanceID));
        }
        public void StopInstance(string instanceID)
        {
            ec2.StopInstances(new StopInstancesRequest().WithInstanceId(instanceID));
        }

    }
}
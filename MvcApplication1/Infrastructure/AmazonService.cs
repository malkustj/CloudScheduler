using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using CloudScheduler.Infrastructure;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
//Talks to AWS and converts
namespace CloudScheduler.Infrastructure
{
    public class AmazonService : ICloudService
    {
        private static AWSTagRepository DataBase = new AWSTagRepository();
        private static AmazonEC2 EC2 = AWSClientFactory.CreateAmazonEC2Client();
        public static List<Instance> GetInstanceList()
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
                    Schedule = new Schedule()
                };
                instance.Name = ReadTag(instance.Id, "Name");
                instance.ElasticIP = GetInstanceElasticIp(instance.Id);
                instance.Schedule = DataBase.Decode(instance);
                currentList.Add(instance);
            }
            return currentList;
        }
        public static string ValidateElasticIp(string elasticIp)
        {
            var eIPList = GetElasticIps();
            foreach (var eIP in eIPList)
            {
                if (eIP == elasticIp)
                {
                    return elasticIp;
                }
            }
            return "";
        }
        public static string GetInstanceElasticIp(string instanceId)
        {
            string s = "";
            foreach (var address in EC2.DescribeAddresses(new DescribeAddressesRequest()).DescribeAddressesResult.Address)
            {
                if (address.InstanceId == instanceId)
                {
                    s = address.PublicIp;
                }
            }
            return s;
        }
        public static string GetElasticIpInstance(string elasticIp)
        {
            string s = "";
            foreach (var address in EC2.DescribeAddresses(new DescribeAddressesRequest()).DescribeAddressesResult.Address)
            {
                if (address.PublicIp == elasticIp)
                {
                    s = address.InstanceId;
                }
            }
            return s;
        }
        public static Instance GetSpecificInstance(string id)
        {
            var instanceList = GetInstanceList();
            foreach (var instance in instanceList)
            {
                if(instance.Id == id)
                {
                    return instance;
                }
            }
            return null;
        }
        public static List<string> GetElasticIps()
        {
            List<string> currentList = new List<string>();
            foreach (var address in EC2.DescribeAddresses(new DescribeAddressesRequest()).DescribeAddressesResult.Address)
            {
                currentList.Add(address.PublicIp);
            }
            return currentList;
        }
        private static DescribeInstancesResult GetInstances()
        {
            return new AmazonEC2Client(RegionEndpoint.USEast1).DescribeInstances(new DescribeInstancesRequest()).DescribeInstancesResult;
        }
        public static  void StartInstance(string instanceID)
        {
            EC2.StartInstances(new StartInstancesRequest().WithInstanceId(instanceID));
        }
        public static void StopInstance(string instanceID)
        {
            EC2.StopInstances(new StopInstancesRequest().WithInstanceId(instanceID));
        }
        public static void AllocateElasticIp(string instanceID, string elasticIP)
        {
            EC2.AssociateAddress(new AssociateAddressRequest().WithInstanceId(instanceID).WithPublicIp(elasticIP));
        }
        public static void WriteToTag(string id, string tagName, string newValue, bool force)
        {
            var instanceList = GetInstances();
            var newTagList = new List<Tag>();
            bool found = false;
            foreach (Reservation reservation in instanceList.Reservation)
            {
                if (reservation.RunningInstance[0].InstanceId == id)
                {
                    foreach (Tag tag in reservation.RunningInstance[0].Tag)
                    {
                        if (tag.Key == tagName)
                        {
                            tag.Value = newValue;
                            found = true;
                        }
                        newTagList.Add(tag);
                    }
                    if (!found && force)
                    {
                        Tag t = new Tag
                        {
                            Key = tagName,
                            Value = newValue
                        };
                        newTagList.Add(t);
                    }
                    CreateTagsRequest ctr = new CreateTagsRequest();
                    ctr.WithTag(newTagList.ToArray());
                    ctr.WithResourceId(id);
                    try
                    {
                        EC2.CreateTags(ctr);
                    }
                    catch (Exception e)
                    {
                        WriteToTag(id, tagName, newValue, force);
                    }
                    break;
                }
            }
        }
        public static string ReadTag(string id, string tagName)
        {
            var instanceList = GetInstances();
            string value = "";
            foreach (Reservation reservation in instanceList.Reservation)
            {
                if (reservation.RunningInstance[0].InstanceId == id)
                {
                    foreach (Tag tag in reservation.RunningInstance[0].Tag)
                    {
                        if (tag.Key == tagName)
                        {
                            value = tag.Value;
                        }
                    }
                }
            }
            return value;
        }
    }
}
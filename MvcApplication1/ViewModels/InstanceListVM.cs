using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CloudScheduler.Infrastructure;

namespace CloudScheduler.ViewModels
{
    public class InstanceListVM
    {
        public InstanceListVM()
        {
            CurrentInstanceList = new List<Instance> { };
        }

        public List<Instance> CurrentInstanceList { get; set; }
    }
}
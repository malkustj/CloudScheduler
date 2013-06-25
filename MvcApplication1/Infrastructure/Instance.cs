using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudScheduler.Infrastructure
{
    public class Instance
    {
        public Instance(int[] run, int[] end)
        {
            RunTime = run;
            EndTime = end;
        }
        public Instance(){ }

        public string Name { get; set; }

        public string Id { get; set; }

        public string State { get; set; }

        public int[] RunTime{ get; set; }

        public int[] EndTime{ get; set; }

        public List<DayOfWeek> Days { get; set; }

    }
}
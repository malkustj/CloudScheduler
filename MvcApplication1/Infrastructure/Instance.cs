using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudScheduler.Infrastructure
{
    public class Instance
    {
        public Instance(){ }

        public Instance(string code) {
            Id = code.Split(';')[0];
            Schedule = new Schedule(code.Split(';')[1]);
        }

        public string Name { get; set; }

        public string Id { get; set; }

        public string State { get; set; }

        public Schedule Schedule { get; set; }

        public string ElasticIP { get; set; }


    }
}
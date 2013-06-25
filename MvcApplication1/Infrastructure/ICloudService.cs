using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScheduler.Infrastructure
{
    interface ICloudService
    {
        public List<Instance> getInstanceList();
        
    }
}

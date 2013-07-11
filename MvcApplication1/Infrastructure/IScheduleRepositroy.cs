using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScheduler.Infrastructure

{
    interface IScheduleRepository
    {
        void Encode(Instance instance);
        Schedule Decode(Instance instance);
        Schedule Default();

    }
}

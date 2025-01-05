using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronJobs.Service
{
    public interface IJobScheduler
    {
        public Task ExecuteAsync();
    }
}

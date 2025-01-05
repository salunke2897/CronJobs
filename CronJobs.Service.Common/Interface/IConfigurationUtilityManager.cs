using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronJobs.Service.Common.Interface
{
    public interface IConfigurationUtilityManager
    {
        string EndPoint { get; set; }
        string Environment { get; set; }


    }
}

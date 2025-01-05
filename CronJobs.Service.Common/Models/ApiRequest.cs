using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronJobs.Service.Common.Models
{
    public class ApiRequest
    {
        public object Data { get; set; }
        public string ApiUrl { get; set; }
    }
}

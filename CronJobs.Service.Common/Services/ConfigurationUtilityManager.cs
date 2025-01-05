using CronJobs.Service.Common.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronJobs.Service.Common.Services
{
    public class ConfigurationUtilityManager : IConfigurationUtilityManager
    {
        public ConfigurationUtilityManager(IConfiguration configuration)
        {
            Console.WriteLine($"BaseUrl: {configuration["BaseUrl"]}");
            EndPoint = configuration["BaseUrl"];
            Environment = configuration["ASPNETCORE_ENVIRONMENT"];

        }
        public string EndPoint { get ; set; }
        public string Environment { get; set; }
    }
}

using CronJobs.Service.Common.Interface;
using CronJobs.Service.Common.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronJobs.Service
{
    public class JobExecutionService : IJob
    {
        private readonly IEndpointService _endpointService;

        public JobExecutionService(IEndpointService endpointService)
        {
            _endpointService = endpointService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var apiUrl = context.JobDetail.JobDataMap.GetString("ApiUrl");
                Console.WriteLine($"Executing job at {DateTime.UtcNow} calling API: {apiUrl}");

                // Make API call
                var apiRequest = new ApiRequest { ApiUrl = apiUrl, Data = "" };
                var response = await _endpointService.GetAsync<object>(apiRequest).ConfigureAwait(false);
                Console.WriteLine($"Job executed successfully. Response: {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing job: {ex.Message}");
            }
        }
    }
}

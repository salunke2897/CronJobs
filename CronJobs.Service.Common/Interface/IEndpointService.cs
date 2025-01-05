using CronJobs.Service.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronJobs.Service.Common.Interface
{
    public interface IEndpointService
    {
        Task<T> GetAsync<T>(ApiRequest apiRequest);
        Task<T> PostAsync<T>(ApiRequest apiRequest);

    }
}

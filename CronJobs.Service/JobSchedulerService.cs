using CronJobs.Service.Common.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Cronos;
using CronJobs.Service.Common;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using CronJobs.Service.Common.Interface;
using Quartz.Impl;
using Quartz;

namespace CronJobs.Service;
public class JobSchedulerService : IJobScheduler
{
    private List<JobSettingsModel> _jobSettings;
    private readonly IEndpointService _endpointService;
    private readonly ISchedulerFactory _schedulerFactory;

    public JobSchedulerService(IEndpointService endpointService, ISchedulerFactory schedulerFactory)
    {
        _endpointService = endpointService;
        // Initialize the scheduler
        _schedulerFactory = schedulerFactory;
    }

    public async Task ExecuteAsync()
    {
        LoadJobSettings();

        // Start the Quartz scheduler
        var _scheduler = await _schedulerFactory.GetScheduler();
        _scheduler.Start().Wait();

        foreach (var jobSetting in _jobSettings)
        {
            foreach (var jobDetail in jobSetting.JobsWithDetails)
            {
                if (jobDetail.IsEnabled)
                {
                    await ScheduleJobAsync(_scheduler,jobDetail);
                }
            }
        }

        // Run until cancellation is requested
        await Task.CompletedTask;
    }

    private void LoadJobSettings()
    {
        string json = File.ReadAllText("jobsettings.json");
        _jobSettings = JsonSerializer.Deserialize<List<JobSettingsModel>>(json);
    }

    private async Task ScheduleJobAsync(IScheduler _scheduler, JobDetails jobDetail)
    {
        try
        {
            // Define the job
            var job = JobBuilder.Create<JobExecutionService>()
                .WithIdentity(jobDetail.Name)
                .UsingJobData("ApiUrl", jobDetail.ApiUrl)
                .Build();

            // Define the cron trigger
            var cronTrigger = TriggerBuilder.Create()
                .WithCronSchedule(jobDetail.CronSchedule)
                .Build();

            // Schedule the job with the trigger
             _scheduler.ScheduleJob(job, cronTrigger).Wait();
            Console.WriteLine($"Scheduled job: {jobDetail.Name} with cron expression: {jobDetail.CronSchedule}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scheduling job {jobDetail.Name}: {ex.Message}");
        }
    }

  
}


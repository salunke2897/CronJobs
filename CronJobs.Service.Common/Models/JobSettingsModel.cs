namespace CronJobs.Service.Common.Models;

public class JobSettingsModel
{
    public string JobType { get; set; }
    public List<JobDetails> JobsWithDetails { get; set; }
}

public class JobDetails
{
    public string Name { get; set; }
    public string CronSchedule { get; set; }
    public string TimeZone { get; set; }
    public bool IsEnabled { get; set; }
    public string ApiUrl { get; set; }
}

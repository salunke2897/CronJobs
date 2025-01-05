
# Job Scheduler Service Using Quartz.NET

This project implements a job scheduling service using Quartz.NET to execute jobs based on cron expressions. The service is designed to read job configurations from a JSON file and execute them at specified intervals by calling specified APIs




## Features

- Quartz.NET Integration: Manages job scheduling with support for complex cron expressions.
- Dependency Injection (DI): Fully integrated with Microsoft's dependency injection system.
- Dynamic Job Configuration: Reads job settings from an external jobsettings.json file.
- Error Handling: Validates cron expressions and handles scheduling errors gracefully.
- API Calls: Executes jobs by calling APIs as defined in the configuration.

## Technologies Used

- .NET 9 
- Quartz.NET
- Microsoft Dependency Injection
- JSON Configuration


## Getting Started

1.Clone the Repository

```bash
git clone <repository-url>
cd <repository-folder>
```
2.Restore NuGet Packages:

```bash
dotnet restore
```
3.Update jobsettings.json:
Add your job configurations to jobsettings.json.
Example:

```bash
[
  {
    "JobType": "WebApiJob",
    "JobsWithDetails": [
      {
        "Name": "GetCustomer",
        "CronSchedule": "0 * * * * ?",
        "TimeZone": "UTC",
        "IsEnabled": true,
        "ApiUrl": "https://localhost:5000/api/customer"
      }
    ]
  }
]
```

4.Run the Application
```bash
dotnet run 
```

## Configuration Details

#### jobsettings.json


| Property | Description                |
| :--------| :------------------------- |
| `JobType`| Type of the job (e.g., WebApiJob). |
| `Name`| Unique identifier for the job. |
| `CronSchedule`| Cron expression defining the schedule. Must use Quartz's 6-field format. |
| `TimeZone`| Time zone in which the job should execute. |
| `IsEnabled`| Boolean flag to enable/disable the job. |
| `ApiUrl`| The API endpoint to be called when the job is executed. |

#### Example Cron Expressions


| Schedule | Cron Expression                |
| :--------| :------------------------- |
| `Every minute`| 0 * * * * ? |
| `Every hour at minute 0`| 0 0 * * * ? |
| `Every day at 12:00 PM`| 0 0 12 * * ? |





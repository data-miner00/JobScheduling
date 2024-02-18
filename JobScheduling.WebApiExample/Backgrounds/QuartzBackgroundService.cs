namespace JobScheduling.WebApiExample.Backgrounds;

using JobScheduling.WebApiExample.Models;
using Quartz;
using System.Threading;
using System.Threading.Tasks;

public class QuartzBackgroundService : BackgroundService
{
    private readonly IScheduler scheduler;
    private readonly IEnumerable<JobDescriptor> jobDescriptors;

    public QuartzBackgroundService(IScheduler scheduler, IEnumerable<JobDescriptor> jobDescriptors)
    {
        this.scheduler = scheduler;
        this.jobDescriptors = jobDescriptors;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scheduleTasks = this.jobDescriptors.Select(x =>
        {
            var jobDetail = JobBuilder.Create()
                .OfType(x.JobType)
                .WithDescription(x.Description)
                .WithIdentity(x.JobType.Name)
                .Build();

            var jobTrigger = TriggerBuilder.Create()
                .WithDescription($"Trigger for {x.JobType.Name}")
                .WithIdentity(x.JobType.Name + ".Trigger")
                .WithCronSchedule(x.CronExpression)
                .Build();

            return this.scheduler.ScheduleJob(jobDetail, jobTrigger, stoppingToken);
        });

        return Task.WhenAll(this.scheduler.Start(stoppingToken), Task.WhenAll(scheduleTasks));
    }
}

namespace JobScheduling.Simplest;

using Quartz;
using Quartz.Impl;

public static class Program
{
    private const string JobName = "job1";
    private const string TriggerName = "trigger1";
    private const string GroupName = "group1";
    private const string CronExpression = "0 * * ? * *"; // Every minute

    private const int IntervalInSeconds = 5;

    public static async Task Main(string[] args)
    {
        var job = CreateJob();
        var trigger = CreateTrigger(CronExpression);
        var scheduler = await CreateSchedulerAsync();

        await scheduler.Start();
        await scheduler.ScheduleJob(job, trigger);

        await Console.Out.WriteLineAsync("Press any key to shutdown.");
        _ = Console.ReadKey();

        await Console.Out.WriteLineAsync("Shutting down...");
        await scheduler.Shutdown();
    }

    private static IJobDetail CreateJob()
    {
        IJobDetail job = JobBuilder.Create<HelloJob>()
            .WithIdentity(JobName, GroupName)
            .WithDescription("An example job")
            .Build();

        return job;
    }

    private static ITrigger CreateTrigger(string? cronExpression = null)
    {
        TriggerBuilder triggerBuilder = TriggerBuilder.Create()
            .WithIdentity(TriggerName, GroupName)
            .StartNow();

        if (string.IsNullOrWhiteSpace(cronExpression))
        {
            triggerBuilder = triggerBuilder.WithSimpleSchedule(
                x => x.WithIntervalInSeconds(IntervalInSeconds).RepeatForever());
        }
        else
        {
            triggerBuilder = triggerBuilder.WithCronSchedule(cronExpression);
        }

        ITrigger trigger = triggerBuilder.Build();

        return trigger;
    }

    private static Task<IScheduler> CreateSchedulerAsync()
    {
        StdSchedulerFactory factory = new();
        return factory.GetScheduler();
    }
}

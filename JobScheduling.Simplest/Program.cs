namespace JobScheduling.Simplest;

using Quartz;
using Quartz.Impl;

public static class Program
{
    private const string JobName = "job1";
    private const string TriggerName = "trigger1";
    private const string GroupName = "group1";

    private const int IntervalInSeconds = 5;

    public static async Task Main(string[] args)
    {
        var job = CreateJob();
        var trigger = CreateTrigger();
        var scheduler = await CreateSchedulerAsync();

        await scheduler.Start();
        await scheduler.ScheduleJob(job, trigger);

        await Console.Out.WriteLineAsync("Press any key to shutdown.");
        _ = Console.ReadKey();

        await Console.Out.WriteLineAsync("Shutting down...");
        await scheduler.Shutdown();
    }

    public static IJobDetail CreateJob()
    {
        IJobDetail job = JobBuilder.Create<HelloJob>()
            .WithIdentity(JobName, GroupName)
            .WithDescription("An example job")
            .Build();

        return job;
    }

    public static ITrigger CreateTrigger()
    {
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity(TriggerName, GroupName)
            .StartNow()
            .WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalInSeconds).RepeatForever())
            .Build();

        return trigger;
    }

    public static Task<IScheduler> CreateSchedulerAsync()
    {
        StdSchedulerFactory factory = new();
        return factory.GetScheduler();
    }
}

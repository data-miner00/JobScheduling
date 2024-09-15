namespace JobScheduling.ConsoleApp;

using Quartz;
using Quartz.Impl;

public static class Program
{
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
            .WithIdentity("job1", "group1")
            .WithDescription("An example job")
            .Build();

        return job;
    }

    public static ITrigger CreateTrigger()
    {
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger1", "group1")
            .StartNow()
            .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
            .Build();
                
        return trigger;
    }

    public static Task<IScheduler> CreateSchedulerAsync()
    {
        StdSchedulerFactory factory = new();
        return factory.GetScheduler();
    }
}

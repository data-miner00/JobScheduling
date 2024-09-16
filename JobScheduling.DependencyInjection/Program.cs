namespace JobScheduling.DependencyInjection;

using Autofac;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;

public static class Program
{
    public static async Task Main(string[] args)
    {
        LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

        using var container = ContainerConfig.Configure();
        using var scope = container.BeginLifetimeScope();

        var scheduler = await CreateSchedulerAsync();
        await scheduler.Start();
        await scope.ScheduleAllJobsAsync(scheduler);

        await Console.Out.WriteLineAsync("Press any key to shutdown.");
        _ = Console.ReadKey();

        await Console.Out.WriteLineAsync("Shutting down...");
        await scheduler.Shutdown();
    }

    public static Task<IScheduler> CreateSchedulerAsync()
    {
        StdSchedulerFactory factory = new();
        return factory.GetScheduler();
    }

    public static async Task ScheduleAllJobsAsync(this ILifetimeScope scope, IScheduler scheduler)
    {
        var jobs = scope.Resolve<Dictionary<Type, string>>();
        foreach (var kvp in jobs)
        {
            var job = JobBuilder.Create(kvp.Key).Build();
            var trigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule(kvp.Value)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}

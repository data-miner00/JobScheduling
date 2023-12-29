using Autofac;
using JobScheduling.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;

LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

using var container = ContainerConfig.Configure();
using var scope = container.BeginLifetimeScope();

StdSchedulerFactory factory = new();

IScheduler scheduler = await factory.GetScheduler();

var jobs = scope.Resolve<Dictionary<Type, string>>();

await scheduler.Start();

foreach (var kvp in jobs)
{
    var job = JobBuilder.Create(kvp.Key).Build();
    var trigger = TriggerBuilder.Create()
        .StartNow()
        .WithCronSchedule(kvp.Value)
        .Build();

    await scheduler.ScheduleJob(job, trigger);
}

await Task.Delay(20_000);

await scheduler.Shutdown();
Console.WriteLine("End");

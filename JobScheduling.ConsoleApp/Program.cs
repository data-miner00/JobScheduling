using JobScheduling.ConsoleApp;
using Quartz;
using Quartz.Impl;

StdSchedulerFactory factory = new();
IJobDetail job = JobBuilder.Create<HelloJob>()
    .WithIdentity("job1", "group1")
    .WithDescription("An example job")
    .Build();
IScheduler scheduler = await factory.GetScheduler();
ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("trigger1", "group1")
    .StartNow()
    .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
    .Build();

// and start it off
await scheduler.Start();

await scheduler.ScheduleJob(job, trigger);

// some sleep to show what's happening
await Task.Delay(TimeSpan.FromSeconds(60));

// and last shut down the scheduler when you are ready to close your program
await scheduler.Shutdown();


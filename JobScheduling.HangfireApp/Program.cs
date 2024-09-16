using Hangfire;
using Hangfire.Storage.SQLite;

GlobalConfiguration.Configuration.UseSQLiteStorage();
using var server = new BackgroundJobServer();
Console.WriteLine("Hangfire Started");

var jobId = BackgroundJob.Enqueue(
    () => Console.WriteLine("Fire-and-forget!"));
var jobId2 = BackgroundJob.Schedule(
    () => Console.WriteLine("Delayed!"),
    TimeSpan.FromDays(7));
BackgroundJob.ContinueJobWith(
    jobId,
    () => Console.WriteLine("Continuation!"));

Console.ReadKey();
server.SendStop();

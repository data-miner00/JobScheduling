using Quartz;

namespace JobScheduling.DependencyInjection.Jobs;

[DisallowConcurrentExecution]
public sealed class FirstJob : IJob
{
    public FirstJob()
    {
        Console.WriteLine();
    }

    public Task Execute(IJobExecutionContext context)
    {
        return Console.Out.WriteLineAsync($"[{DateTime.Now}] First job is running...");
    }
}

using Quartz;

namespace JobScheduling.DependencyInjection.Jobs;

[DisallowConcurrentExecution]
public class FirstJob : IJob
{
    public FirstJob()
    {
        Console.WriteLine();
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync("First job is running...");
    }
}

namespace JobScheduling.DependencyInjection.Jobs;

using Quartz;

public class SecondJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync("Second job is running...");
    }
}

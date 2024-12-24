namespace JobScheduling.DependencyInjection.Jobs;

using Quartz;

public sealed class SecondJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return Console.Out.WriteLineAsync($"[{DateTime.Now}] Second job is running...");
    }
}

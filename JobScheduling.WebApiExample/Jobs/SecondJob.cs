namespace JobScheduling.WebApiExample.Jobs;

using Quartz;

public sealed class SecondJob : IJob
{
    /// <inheritdoc/>
    public Task Execute(IJobExecutionContext context)
    {
        return Console.Out.WriteLineAsync($"[{DateTime.Now}] Executing Second Job");
    }
}

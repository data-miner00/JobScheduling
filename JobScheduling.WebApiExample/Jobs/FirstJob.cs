namespace JobScheduling.WebApiExample.Jobs;

using Quartz;

public sealed class FirstJob : IJob
{
    /// <inheritdoc/>
    public Task Execute(IJobExecutionContext context)
    {
        return Console.Out.WriteLineAsync($"[{DateTime.Now}] Executing First Job");
    }
}

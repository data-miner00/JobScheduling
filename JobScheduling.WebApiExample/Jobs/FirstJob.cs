namespace JobScheduling.WebApiExample.Jobs;

using Quartz;

public class FirstJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return Console.Out.WriteLineAsync($"Executing First Job: {DateTime.Now}");
    }
}

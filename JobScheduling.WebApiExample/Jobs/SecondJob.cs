namespace JobScheduling.WebApiExample.Jobs;

using Quartz;

public class SecondJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return Console.Out.WriteLineAsync($"Executing Second Job: {DateTime.Now}");
    }
}

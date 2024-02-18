using Quartz;

namespace JobScheduling.WebApiExample.Jobs
{
    public class SecondJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Console.Out.WriteLineAsync($"Executing Second Job: {DateTime.Now}");
        }
    }
}

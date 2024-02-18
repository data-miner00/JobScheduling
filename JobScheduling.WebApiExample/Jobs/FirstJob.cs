using Quartz;

namespace JobScheduling.WebApiExample.Jobs
{
    public class FirstJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Console.Out.WriteLineAsync($"Executing First Job: {DateTime.Now}");
        }
    }
}

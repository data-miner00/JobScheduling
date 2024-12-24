namespace JobScheduling.Simplest;

using Quartz;
using System.Threading.Tasks;

public sealed class HelloJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return Console.Out.WriteLineAsync($"[{DateTime.Now}] Hello from HelloJob");
    }
}

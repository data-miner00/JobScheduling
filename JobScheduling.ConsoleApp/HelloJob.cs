namespace JobScheduling.ConsoleApp;

using Quartz;
using System.Threading.Tasks;

public sealed class HelloJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync("Hello from HelloJob");
    }
}

namespace JobScheduling.WebApiExample.Backgrounds;

using JobScheduling.WebApiExample.Options;
using Microsoft.Extensions.Options;

public class LogBackgroundService : BackgroundService
{
    private const string ServiceName = "LogBackgroundService";
    private readonly FirstJobOption option;

    public LogBackgroundService(IOptions<FirstJobOption> options)
    {
        this.option = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        while (!stoppingToken.IsCancellationRequested)
        {
            await Console.Out.WriteLineAsync($"Logging from {ServiceName} - ExecuteAsync - {DateTime.Now} - {this.option.CronExpression}");
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return LogAndStop();

        async Task LogAndStop()
        {
            await Console.Out.WriteLineAsync($"Logging from {ServiceName} - StopAsync - {DateTime.Now}");
            await base.StopAsync(cancellationToken);
        }
    }
}

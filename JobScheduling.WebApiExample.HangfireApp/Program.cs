namespace JobScheduling.WebApiExample.HangfireApp;

using Hangfire;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHangfire(opt =>
        {
            opt
                .UseInMemoryStorage()
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();
        });

        builder.Services.AddHangfireServer(opt =>
        {
            opt.SchedulePollingInterval = TimeSpan.FromSeconds(5);
        });

        var app = builder.Build();

        app.UseHangfireDashboard();

        app.MapGet("job", (IBackgroundJobClient client, IRecurringJobManager manager) =>
        {
            client.Enqueue(() => Console.WriteLine("First job from background"));
            client.Schedule(() => Console.WriteLine("Second job from schedule"), TimeSpan.FromSeconds(5));
            manager.AddOrUpdate("uniqueId-123", () => Console.WriteLine("Third job from recurring"), "*/1 * * * *");
            return Results.Ok("Job placed sucessfully");
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", (HttpContext httpContext) =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                })
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        app.Run();
    }
}

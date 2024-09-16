namespace JobScheduling.WebApiExample;

using JobScheduling.WebApiExample.Backgrounds;
using JobScheduling.WebApiExample.Jobs;
using JobScheduling.WebApiExample.Models;
using JobScheduling.WebApiExample.Options;
using Microsoft.Extensions.Options;
using Quartz.Impl;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder
            .ConfigureSwagger()
            .ConfigureHostedServices()
            .ConfigureQuartzJobs();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    static WebApplicationBuilder ConfigureHostedServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<LogBackgroundService>();
        builder.Services.AddHostedService<QuartzBackgroundService>();

        return builder;
    }

    static WebApplicationBuilder ConfigureQuartzJobs(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<FirstJobOption>(
            builder.Configuration.GetSection(nameof(FirstJobOption)));

        builder.Services.Configure<SecondJobOption>(
            builder.Configuration.GetSection(nameof(SecondJobOption)));

        var scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
        builder.Services.AddSingleton(scheduler);

        builder.Services.AddSingleton<IEnumerable<JobDescriptor>>(provider => new List<JobDescriptor>
        {
            new()
            {
                JobType = typeof(FirstJob),
                Description = "The first job",
                CronExpression = provider.GetRequiredService<IOptions<FirstJobOption>>().Value.CronExpression,
            },
            new()
            {
                JobType = typeof(SecondJob),
                Description = "The second job",
                CronExpression = provider.GetRequiredService<IOptions<SecondJobOption>>().Value.CronExpression,
            },
        });

        return builder;
    }
}

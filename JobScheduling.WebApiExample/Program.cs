namespace JobScheduling.WebApiExample;

using JobScheduling.WebApiExample.Backgrounds;
using JobScheduling.WebApiExample.Jobs;
using JobScheduling.WebApiExample.Models;
using JobScheduling.WebApiExample.Options;
using Microsoft.Extensions.Options;
using Quartz.Impl;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<FirstJobOption>(
            builder.Configuration.GetSection(nameof(FirstJobOption)));

        builder.Services.Configure<SecondJobOption>(
            builder.Configuration.GetSection(nameof(SecondJobOption)));

        builder.Services.AddHostedService<LogBackgroundService>();
        builder.Services.AddHostedService<QuartzBackgroundService>();

        var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
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
}

namespace JobScheduling.DependencyInjection;

using Autofac;
using Autofac.Configuration;
using JobScheduling.DependencyInjection.Jobs;
using JobScheduling.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

internal static class ContainerConfig
{
    private static readonly Dictionary<Type, string> JobMaps = [];

    public static IContainer Configure()
    {
        var builder = new ContainerBuilder();

        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddJsonFile("settings.json");

        var config = configBuilder.Build();

        var firstJobOption = config.GetSection(nameof(FirstJobOption)).Get<FirstJobOption>();
        var secondJobOption = config.GetSection(nameof(SecondJobOption)).Get<SecondJobOption>();

        ValidateOption(firstJobOption);
        ValidateOption(secondJobOption);

        var module = new ConfigurationModule(configBuilder.Build());

        builder.RegisterModule(module);

        AddJobMap(typeof(FirstJob), firstJobOption.CronSchedule);
        AddJobMap(typeof(SecondJob), secondJobOption.CronSchedule);

        builder.RegisterInstance(firstJobOption).SingleInstance();
        builder.RegisterInstance(secondJobOption).SingleInstance();
        builder.RegisterType<FirstJob>().SingleInstance();
        builder.RegisterType<SecondJob>().SingleInstance();
        builder.RegisterInstance(JobMaps).SingleInstance();

        return builder.Build();
    }

    private static void AddJobMap(Type type, string cronSchedule)
    {
        JobMaps.Add(type, cronSchedule);
    }

    private static void ValidateOption<T>(T option)
    {
        var context = new ValidationContext(option, null, null);
        Validator.ValidateObject(option, context, true);
    }
}

namespace JobScheduling.DependencyInjection.Options;

using System.ComponentModel.DataAnnotations;

public sealed class FirstJobOption
{
    [Required]
    [CronExpresssion]
    public string CronSchedule { get; set; }
}

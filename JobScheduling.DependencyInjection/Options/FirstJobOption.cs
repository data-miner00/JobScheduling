namespace JobScheduling.DependencyInjection.Options;

using System.ComponentModel.DataAnnotations;
using JobScheduling.Common;

public sealed class FirstJobOption
{
    [Required]
    [CronExpresssion]
    required public string CronSchedule { get; set; }
}

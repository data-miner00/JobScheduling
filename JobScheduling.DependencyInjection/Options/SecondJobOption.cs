namespace JobScheduling.DependencyInjection.Options;

using System.ComponentModel.DataAnnotations;
using JobScheduling.Common;

public sealed class SecondJobOption
{
    [Required]
    [CronExpresssion]
    required public string CronSchedule { get; set; }

    [Required]
    required public string OptionOne { get; set; }

    [Required]
    required public int OptionTwo { get; set; }
}

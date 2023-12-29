namespace JobScheduling.DependencyInjection.Options;

using System.ComponentModel.DataAnnotations;

public sealed class SecondJobOption
{
    [Required]
    [CronExpresssion]
    public string CronSchedule { get; set; }

    [Required]
    public string OptionOne { get; set; }

    [Required]
    public int OptionTwo { get; set; }
}

namespace JobScheduling.WebApiExample.Options;

using JobScheduling.Common;

public sealed class FirstJobOption
{
    [CronExpresssion]
    required public string CronExpression { get; set; }
}

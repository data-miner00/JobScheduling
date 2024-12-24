namespace JobScheduling.WebApiExample.Options;

using JobScheduling.Common;

public sealed class SecondJobOption
{
    [CronExpresssion]
    required public string CronExpression { get; set; }
}

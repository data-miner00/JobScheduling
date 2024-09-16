namespace JobScheduling.WebApiExample.Models;

public class JobDescriptor
{
    required public Type JobType { get; set; }

    required public string Description { get; set; }

    required public string CronExpression { get; set; }
}

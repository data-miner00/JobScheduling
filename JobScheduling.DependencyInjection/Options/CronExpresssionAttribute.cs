namespace JobScheduling.DependencyInjection.Options;

using Quartz;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class CronExpresssionAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is string strVal)
        {
            return CronExpression.IsValidExpression(strVal);
        }

        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.InvariantCulture, "The cron expression provided is invalid. Name: {0}", name);
    }
}

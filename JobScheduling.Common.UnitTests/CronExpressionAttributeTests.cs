namespace JobScheduling.Common.UnitTests;

using JobScheduling.Common;
using System.ComponentModel.DataAnnotations;

public class CronExpressionAttributeTests
{
    [Fact]
    public void ValidCronExpression_NoException()
    {
        var validCronExpression = "* * * ? * * *";
        var @object = new SampleClass
        {
            CronExpression = validCronExpression,
        };

        ValidateObject(@object);

        Assert.NotNull(@object);
        Assert.Equal(validCronExpression, @object.CronExpression);
    }

    [Fact]
    public void InvalidCronExpression_ThrowsException()
    {
        var invalidCronExpression = "xxx";
        var @object = new SampleClass
        {
            CronExpression = invalidCronExpression,
        };

        Assert.Throws<ValidationException>(() =>
        {
            ValidateObject(@object);
        });
    }

    public static void ValidateObject<T>(T @object)
    {
        var context = new ValidationContext(@object, null, null);
        Validator.ValidateObject(@object, context, true);
    }

    public sealed class SampleClass
    {
        [CronExpresssion]
        required public string CronExpression { get; set; }
    }
}

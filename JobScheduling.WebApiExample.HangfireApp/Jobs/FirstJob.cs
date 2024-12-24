namespace JobScheduling.WebApiExample.HangfireApp.Jobs;

public sealed class FirstJob : IJob
{
    public void AlternateWay()
    {
        throw new NotImplementedException();
    }

    public void Main()
    {
        Console.WriteLine($"[{DateTime.Now}] hello from Main");
    }
}

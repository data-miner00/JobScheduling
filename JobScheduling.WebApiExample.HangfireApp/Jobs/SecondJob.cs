namespace JobScheduling.WebApiExample.HangfireApp.Jobs;

public sealed class SecondJob : IJob
{
    public void AlternateWay()
    {
        Console.WriteLine($"[{DateTime.Now}] Hello from alternate");
    }

    public void Main()
    {
        throw new NotImplementedException();
    }
}

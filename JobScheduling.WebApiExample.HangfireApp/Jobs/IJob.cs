namespace JobScheduling.WebApiExample.HangfireApp.Jobs;

/// <summary>
/// The interface for Hangfire Jobs.
/// </summary>
public interface IJob
{
    /// <summary>
    /// Contains the execution code.
    /// </summary>
    void Main();

    /// <summary>
    /// Contains the alternative execution code.
    /// </summary>
    void AlternateWay();
}

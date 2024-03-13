﻿namespace JobScheduling.ConsoleApp;

using Quartz;
using System.Threading.Tasks;

public sealed class HelloJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return Console.Out.WriteLineAsync("Hello from HelloJob");
    }
}

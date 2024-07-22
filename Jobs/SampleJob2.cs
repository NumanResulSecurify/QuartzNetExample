using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzNetExample.Jobs
{
    public class SampleJob2 : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"SampleJob2 executed at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}

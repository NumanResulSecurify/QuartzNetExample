using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzNetExample.Jobs
{
    public class SampleJob1 : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"SampleJob1 executed at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}

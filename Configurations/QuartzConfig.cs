using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using QuartzNetExample.Jobs;
using QuartzNetExample.Services;

namespace QuartzNetExample.Configurations
{
    public static class QuartzConfig
    {
        public static void AddQuartzServices(this IServiceCollection services)
        {
            // Add Quartz.NET services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add jobs
            services.AddSingleton<SampleJob1>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(SampleJob1),
                cronExpression: "0/10 * * * * ?")); // runs every 10 seconds

            services.AddSingleton<SampleJob2>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(SampleJob2),
                cronExpression: "0/20 * * * * ?")); // runs every 20 seconds

            services.AddHostedService<QuartzHostedService>();
        }
    }
}

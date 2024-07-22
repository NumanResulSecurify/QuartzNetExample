using Quartz;
using System.Collections.Generic;

namespace QuartzNetExample.Models
{
    public class JobDetailsViewModel
    {
        public IReadOnlyCollection<IJobExecutionContext> Jobs { get; set; }
    }
}

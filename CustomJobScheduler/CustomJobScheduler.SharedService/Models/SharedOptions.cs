using System.Collections.Generic;

namespace CustomJobScheduler.SharedService.Models
{
    public class SharedOptions
    {
        public string AppExecutorPath { get; set; }
        public string ExecutionHistoryStorePath { get; set; }
        public List<string> JobDataKey { get; set; }
        public List<string> JobClass { get; set; }
    }
}

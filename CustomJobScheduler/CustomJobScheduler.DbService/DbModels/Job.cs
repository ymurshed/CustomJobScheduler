using System;
using System.Collections.Generic;

namespace CustomJobScheduler.DbService.DbModels
{
    public partial class Job
    {
        public Job()
        {
            Trigger = new HashSet<Trigger>();
        }

        public Guid Id { get; set; }
        public string JobKey { get; set; }
        public string JobName { get; set; }
        public string OldJobName { get; set; }
        public string Group { get; set; }
        public string OldGroup { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool IsCopy { get; set; }
        public bool? IsNew { get; set; }
        public bool? Recovery { get; set; }
        public string JobData { get; set; }

        public ICollection<Trigger> Trigger { get; set; }
    }
}

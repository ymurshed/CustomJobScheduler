using System;

namespace CustomJobScheduler.DbService.DbModels
{
    public partial class Trigger
    {
        public Guid Id { get; set; }
        public string TriggerKey { get; set; }
        public Guid JobId { get; set; }
        public short Type { get; set; }
        public string TriggerName { get; set; }
        public string OldTriggerName { get; set; }
        public string TriggerGroup { get; set; }
        public string OldTriggerGroup { get; set; }
        public string Description { get; set; }
        public bool IsCopy { get; set; }
        public bool? IsNew { get; set; }
        public int? Priority { get; set; }
        public int? PriorityOrDefault { get; set; }
        public string StartTimeUtc { get; set; }
        public string EndTimeUtc { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string DateTimeFormat { get; set; }
        public int? MisFireInstruction { get; set; }
        public string MisFireInstructionsJson { get; set; }

        public Job Job { get; set; }
    }
}

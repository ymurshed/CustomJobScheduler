using System;

namespace CustomJobScheduler.DbService.DbModels
{
    public partial class TriggerType
    {
        public Guid Id { get; set; }
        public string Expression { get; set; }
        public string TimeZone { get; set; }
        public int? RepeatCount { get; set; }
        public bool? RepeatForever { get; set; }
        public int? RepeatInterval { get; set; }
        public short? RepeatUnit { get; set; }
        public bool PreserveHourAcrossDst { get; set; }
        public bool SkipDayIfHourDoesNotExist { get; set; }
        public bool AreOnlyWeekdaysEnabled { get; set; }
        public bool AreOnlyWeekendEnabled { get; set; }
        public bool? Friday { get; set; }
        public bool? Saturday { get; set; }
        public bool? Sunday { get; set; }
        public bool? Monday { get; set; }
        public bool? Tuesday { get; set; }
        public bool? Wednessday { get; set; }
        public bool? Thursday { get; set; }
        public long? StartTime { get; set; }
        public long? EndTime { get; set; }
    }
}

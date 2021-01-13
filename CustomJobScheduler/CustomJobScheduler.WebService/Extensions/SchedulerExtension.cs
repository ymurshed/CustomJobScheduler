using System;
using CustomJobScheduler.DbService.DbModels;
using Quartz;

namespace CustomJobScheduler.WebService.Extensions
{
    public static class SchedulerExtension
    {
        public static TimeSpan GetTimeSpan(this long? time) => time.HasValue ? TimeSpan.FromTicks(time.Value) : TimeSpan.FromTicks(0);
        public static TimeZoneInfo GetTimeZone(this string timeZoneName) => TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
        public static string DateTimeFormat(this Trigger trigger) => trigger.DateFormat + " " + trigger.TimeFormat; 

        public static DateTime? ParseDateTime(this Trigger trigger, string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (DateTime.TryParse(value, out var result) == false)
                return null;

            return result;
        }

        public static CronScheduleBuilder GetCronScheduleBuilder(this CronScheduleBuilder builder, TriggerType triggerType)
        {
            builder.InTimeZone(triggerType.TimeZone.GetTimeZone());
            return builder;
        }

        public static SimpleScheduleBuilder GetSimpleScheduleBuilder(this SimpleScheduleBuilder builder, TriggerType triggerType)
        {
            var unit = triggerType.RepeatUnit ?? 1;
            var count = triggerType.RepeatCount ?? 1;
            var interval = triggerType.RepeatInterval ?? 1;
            var forever = triggerType.RepeatForever ?? true;
            
            switch (unit)
            {
                case (short)IntervalUnit.Millisecond:
                    builder.WithRepeatCount(count).WithInterval(new TimeSpan(0, 0, 0, 0, interval));
                    break;
                case (short)IntervalUnit.Second:
                    builder.WithRepeatCount(count).WithIntervalInSeconds(interval);
                    break;
                case (short)IntervalUnit.Minute:
                    builder.WithRepeatCount(count).WithIntervalInMinutes(interval);
                    break;
                case (short)IntervalUnit.Hour:
                    builder.WithRepeatCount(count).WithIntervalInHours(interval);
                    break;
            }
            
            if (forever) builder.RepeatForever();
            return builder;
        }

        public static CalendarIntervalScheduleBuilder GetCalendarIntervalScheduleBuilder(this CalendarIntervalScheduleBuilder builder, TriggerType triggerType)
        {
            var unit = triggerType.RepeatUnit ?? 1;
            var interval = triggerType.RepeatInterval ?? 1;
            
            switch (unit)
            {
                case (short)IntervalUnit.Millisecond:
                    builder.WithInterval(interval, IntervalUnit.Millisecond);
                    break;
                case (short)IntervalUnit.Second:
                    builder.WithIntervalInSeconds(interval);
                    break;
                case (short)IntervalUnit.Minute:
                    builder.WithIntervalInMinutes(interval);
                    break;
                case (short)IntervalUnit.Hour:
                    builder.WithIntervalInHours(interval);
                    break;
            }

            builder.SkipDayIfHourDoesNotExist(triggerType.SkipDayIfHourDoesNotExist)
                   .PreserveHourOfDayAcrossDaylightSavings(triggerType.PreserveHourAcrossDst)
                   .InTimeZone(triggerType.TimeZone.GetTimeZone());
            return builder;
        }

        public static DailyTimeIntervalScheduleBuilder GetDailyTimeIntervalScheduleBuilder(this DailyTimeIntervalScheduleBuilder builder, TriggerType triggerType)
        {
            var unit = triggerType.RepeatUnit ?? 1;
            var count = triggerType.RepeatCount ?? 0;
            var interval = triggerType.RepeatInterval ?? 1;
            var forever =  triggerType.RepeatForever ?? true;
            
            switch (unit)
            {
                case (short)IntervalUnit.Second:
                    builder.WithRepeatCount(count).WithIntervalInSeconds(interval);
                    break;
                case (short)IntervalUnit.Minute:
                    builder.WithRepeatCount(count).WithIntervalInMinutes(interval);
                    break;
                case (short)IntervalUnit.Hour:
                    builder.WithRepeatCount(count).WithIntervalInHours(interval);
                    break;
            }

            var timeSpan = triggerType.StartTime.GetTimeSpan();
            var startTime = new TimeOfDay(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            timeSpan = triggerType.EndTime.GetTimeSpan();
            var endTime = new TimeOfDay(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            
            var daysOfWeek = new DayOfWeek[7];
            if (triggerType.Friday.HasValue && triggerType.Friday.Value) daysOfWeek[0] = DayOfWeek.Friday;
            if (triggerType.Saturday.HasValue && triggerType.Saturday.Value) daysOfWeek[1] = DayOfWeek.Saturday;
            if (triggerType.Sunday.HasValue && triggerType.Sunday.Value) daysOfWeek[2] = DayOfWeek.Sunday;
            if (triggerType.Monday.HasValue && triggerType.Monday.Value) daysOfWeek[3] = DayOfWeek.Monday;
            if (triggerType.Tuesday.HasValue && triggerType.Tuesday.Value) daysOfWeek[4] = DayOfWeek.Tuesday;
            if (triggerType.Wednessday.HasValue && triggerType.Wednessday.Value) daysOfWeek[5] = DayOfWeek.Wednesday;
            if (triggerType.Thursday.HasValue && triggerType.Thursday.Value) daysOfWeek[6] = DayOfWeek.Thursday;

            builder.StartingDailyAt(startTime)
                   .EndingDailyAt(endTime)
                   .InTimeZone(triggerType.TimeZone.GetTimeZone())
                   .OnDaysOfTheWeek(daysOfWeek);
            return builder;
        }
    }
}

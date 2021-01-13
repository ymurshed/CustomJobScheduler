using System;
using CustomJobScheduler.DbService.DbModels;
using CustomJobScheduler.WebService.Extensions;
using Quartz;

namespace CustomJobScheduler.WebService.Schedulers
{
    public class CustomTriggerBuilder
    {
        public ITrigger CreateTrigger(Trigger trigger, TriggerType triggerType)
        {
            ITrigger newTrigger;
            var startTimeUtc = trigger.ParseDateTime(trigger.StartTimeUtc) ?? DateTime.UtcNow;
            var endTimeUtc = trigger.ParseDateTime(trigger.EndTimeUtc);

            switch (trigger.Type)
            {
                case (short)Quartzmin.Models.TriggerType.Cron:
                    newTrigger = TriggerBuilder.Create()
                                .WithIdentity(trigger.TriggerName, trigger.TriggerGroup)
                                .WithDescription(trigger.Description)
                                .WithCronSchedule(triggerType.Expression, builder => builder.GetCronScheduleBuilder(triggerType))
                                .StartAt(startTimeUtc)
                                .EndAt(endTimeUtc)
                                .Build();
                    break;

                case (short)Quartzmin.Models.TriggerType.Simple:
                    newTrigger = TriggerBuilder.Create()
                                .WithIdentity(trigger.TriggerName, trigger.TriggerGroup)
                                .WithDescription(trigger.Description)
                                .WithSimpleSchedule(builder => builder.GetSimpleScheduleBuilder(triggerType))
                                .StartAt(startTimeUtc)
                                .EndAt(endTimeUtc)
                                .Build();
                    break;

                case (short)Quartzmin.Models.TriggerType.Calendar:
                    newTrigger = TriggerBuilder.Create()
                                .WithIdentity(trigger.TriggerName, trigger.TriggerGroup)
                                .WithDescription(trigger.Description)
                                .WithCalendarIntervalSchedule(builder => builder.GetCalendarIntervalScheduleBuilder(triggerType))
                                .StartAt(startTimeUtc)
                                .EndAt(endTimeUtc)
                                .Build();
                    break;

                case (short)Quartzmin.Models.TriggerType.Daily:
                    newTrigger = TriggerBuilder.Create()
                                .WithIdentity(trigger.TriggerName, trigger.TriggerGroup)
                                .WithDescription(trigger.Description)
                                .WithDailyTimeIntervalSchedule(builder => builder.GetDailyTimeIntervalScheduleBuilder(triggerType))
                                .StartAt(startTimeUtc)
                                .EndAt(endTimeUtc)
                                .Build();
                    break;

                
                default: throw new ArgumentOutOfRangeException();
            }

            return newTrigger;
        }
    }
}

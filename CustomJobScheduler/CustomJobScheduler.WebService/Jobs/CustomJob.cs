using System;
using System.Threading.Tasks;
using CustomJobScheduler.SharedService.Helpers;
using Quartz;

namespace CustomJobScheduler.WebService.Jobs
{
    [DisallowConcurrentExecution, PersistJobDataAfterExecution]
    public class CustomJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                LogHelper.Print($"Executing custom job for job key: {context.JobDetail.Key}");
                await Task.Delay(TimeSpan.FromSeconds(3));
            }
            catch (Exception ex)
            {
                LogHelper.Print(ex, $"Error occurred while executing custom job for job key: {context.JobDetail.Key}. Details: {ex.Message}", true);
            }
        }
    }
}

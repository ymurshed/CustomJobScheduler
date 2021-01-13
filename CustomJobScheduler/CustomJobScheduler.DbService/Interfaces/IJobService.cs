using System.Collections.Generic;
using System.Threading.Tasks;
using CustomJobScheduler.DbService.DbModels;

namespace CustomJobScheduler.DbService.Interfaces
{
    public interface IJobService
    {
        Task SaveJobAsync(Job job);
        Task UpdateJobAsync(Job job);
        Task DeleteJobAsync(string jobKey);
        Task<Job> GetJobAsync(string jobKey);
        Task<List<Job>> GetAllJobAsync();
    }
}

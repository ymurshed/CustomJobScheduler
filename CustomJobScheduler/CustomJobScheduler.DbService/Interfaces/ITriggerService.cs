using System.Collections.Generic;
using System.Threading.Tasks;
using CustomJobScheduler.DbService.DbModels;

namespace CustomJobScheduler.DbService.Interfaces
{
    public interface ITriggerService
    {
        Task SaveTriggerAsync(Trigger trigger, TriggerType triggerType);
        Task UpdateTriggerAsync(Trigger trigger, TriggerType triggerType);
        Task DeleteTriggerAsync(string triggerKey);
        Task<List<Trigger>> GetAllTriggerAsync();
        Task<List<TriggerType>> GetAllTriggerTypeAsync();
    }
}

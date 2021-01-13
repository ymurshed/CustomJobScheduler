using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomJobScheduler.DbService.DbModels;
using CustomJobScheduler.DbService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomJobScheduler.DbService.Services
{
    public class TriggerService : ITriggerService
    {
        private readonly Context _context;

        public TriggerService(Context context)
        {
            _context = context;
        }

        public async Task SaveTriggerAsync(Trigger trigger, TriggerType triggerType)
        {
            await _context.Trigger.AddAsync(trigger);
            await _context.TriggerType.AddAsync(triggerType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTriggerAsync(Trigger trigger, TriggerType triggerType)
        {
            var triggerKey = $"{trigger.OldTriggerGroup}.{trigger.OldTriggerName}";
            var triggerId = await _context.Trigger.AsNoTracking().Where(x => x.TriggerKey == triggerKey).Select(x => x.Id).FirstOrDefaultAsync();
            
            trigger.Id = triggerId;
            triggerType.Id = triggerId;

            _context.Trigger.Update(trigger);
            _context.TriggerType.Update(triggerType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTriggerAsync(string triggerKey)
        {
            var trigger = await _context.Trigger.FirstOrDefaultAsync(x => x.TriggerKey == triggerKey);
            var triggerType = await _context.TriggerType.FirstOrDefaultAsync(x => x.Id == trigger.Id);

            _context.TriggerType.Remove(triggerType);
            _context.Trigger.Remove(trigger);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Trigger>> GetAllTriggerAsync()
        {
            return await _context.Trigger.ToListAsync();
        }

        public async Task<List<TriggerType>> GetAllTriggerTypeAsync()
        {
            return await _context.TriggerType.ToListAsync();
        }
    }
}

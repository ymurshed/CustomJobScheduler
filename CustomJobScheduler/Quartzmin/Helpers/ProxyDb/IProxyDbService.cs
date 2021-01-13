using System.Threading.Tasks;
using Quartzmin.Models;

namespace Quartzmin.Helpers.ProxyDb
{
    public interface IProxyDbService
    {
        Task SaveJob(JobPropertiesViewModel jobPropertiesViewModel, JobDataMapItemBase[] jobDataMapItemBases, bool shouldUpdate = false);
        Task SaveTrigger(TriggerPropertiesViewModel triggerPropertiesViewModel, bool shouldUpdate = false);
        Task DeleteJob(string key);
        Task DeleteTrigger(string key);
    }
}

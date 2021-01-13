namespace CustomJobScheduler.SharedService.Interfaces
{
    public interface IExecutionHistoryService
    {
        object GetExecutionHistory();
        bool SaveExecutionHistory(object entry);
    }
}

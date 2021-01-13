namespace CustomJobScheduler.SharedService.Interfaces
{
    public interface IJsonService
    {
        string GetJsonStringFromObject<T>(T data);
        dynamic GetObjectFromJsonString(string jsonString);
    }
}

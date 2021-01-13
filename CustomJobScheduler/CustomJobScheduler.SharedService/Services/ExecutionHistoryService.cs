using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CustomJobScheduler.SharedService.Helpers;
using CustomJobScheduler.SharedService.Interfaces;
using CustomJobScheduler.SharedService.Models;
using Microsoft.Extensions.Options;

namespace CustomJobScheduler.SharedService.Services
{
    public class ExecutionHistoryService : IExecutionHistoryService
    {
        private readonly IOptions<SharedOptions> _sharedOptions;

        public ExecutionHistoryService(IOptions<SharedOptions> sharedOptions)
        {
            _sharedOptions = sharedOptions;
        }

        public object GetExecutionHistory()
        {
            object entry = null;
            FileStream stream = null;

            try
            {
                var formatter = new BinaryFormatter();
                var path = _sharedOptions.Value.ExecutionHistoryStorePath;
                if (!File.Exists(path)) return null;

                stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                entry = formatter.Deserialize(stream);
                stream.Close();
                LogHelper.Print("ExecutionHistory is fetched successfully.");
            }
            catch (Exception ex)
            {
                stream?.Close();
                LogHelper.Print(ex, "An error has occurred while fetching ExecutionHistory.", true);
            }

            return entry;
        }

        public bool SaveExecutionHistory(object entry)
        {
            FileStream stream = null;

            try
            {
                var formatter = new BinaryFormatter();
                var path = _sharedOptions.Value.ExecutionHistoryStorePath;
                var pathWithoutFile = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(pathWithoutFile) && !Directory.Exists(pathWithoutFile)) Directory.CreateDirectory(pathWithoutFile);
                
                stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, entry);
                stream.Close();
                LogHelper.Print("ExecutionHistory is saved successfully.");
            }
            catch (Exception ex)
            {
                stream?.Close();
                LogHelper.Print(ex, "An error has occurred while saving ExecutionHistory.", true);
                return false;
            }

            return true;
        }
    }
}

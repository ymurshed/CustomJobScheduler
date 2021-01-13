using System;
using System.Collections.Generic;
using CustomJobScheduler.SharedService.Helpers;
using CustomJobScheduler.SharedService.Interfaces;
using CustomJobScheduler.SharedService.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CustomJobScheduler.SharedService.Services
{
    public class JsonService : IJsonService
    {
        private readonly IOptions<SharedOptions> _sharedOptions;

        public JsonService(IOptions<SharedOptions> sharedOptions)
        {
            _sharedOptions = sharedOptions;
        }

        public string GetJsonStringFromObject<T>(T data)
        {
            var jsonString = string.Empty;

            try
            {
                jsonString = JsonConvert.SerializeObject(data);
                LogHelper.Print("json string converted successfully.");
            }
            catch (Exception ex)
            {
                LogHelper.Print(ex, "An error has occurred while converting to json string from dynamic object.", true);
            }

            return jsonString;
        }

        public dynamic GetObjectFromJsonString(string jsonString)
        {
            dynamic data = null;

            try
            {
                data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                LogHelper.Print("Dynamic object converted successfully.");
            }
            catch (Exception ex)
            {
                LogHelper.Print(ex, "An error has occurred while converting to dynamic object from json string.", true);
            }

            return data;
        }
    }
}

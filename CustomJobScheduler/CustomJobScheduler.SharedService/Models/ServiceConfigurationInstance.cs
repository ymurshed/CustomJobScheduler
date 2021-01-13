using System;
using Microsoft.Extensions.Configuration;

namespace CustomJobScheduler.SharedService.Models
{
    public class ServiceConfigurationInstance
    {
        public static IConfiguration Configuration;
        public static IServiceProvider ServiceProvider;

        public static string GetAppVirtualPath => Configuration.GetSection("AppVirtualPath").Value;
    }
}

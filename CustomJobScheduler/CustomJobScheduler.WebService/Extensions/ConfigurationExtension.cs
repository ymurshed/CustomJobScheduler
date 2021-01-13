using AutoMapper;
using CustomJobScheduler.DbService.DbModels;
using CustomJobScheduler.DbService.Interfaces;
using CustomJobScheduler.DbService.Services;
using CustomJobScheduler.SharedService.Interfaces;
using CustomJobScheduler.SharedService.Models;
using CustomJobScheduler.SharedService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartzmin.Helpers.ProxyDb;
using Quartzmin.MappingProfile;
using Serilog;

namespace CustomJobScheduler.WebService.Extensions
{
    public static class ConfigurationExtension
    {
        public static void AddServiceConfigurationInstance(this IServiceCollection services, IConfiguration configuration)
        {
            ServiceConfigurationInstance.Configuration = configuration;
            ServiceConfigurationInstance.ServiceProvider = services.BuildServiceProvider();
        }

        public static void AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var timeoutInSec = configuration.GetValue<int>("DbCommandTimeoutInSec");
            services.Configure<SharedOptions>(configuration.GetSection(nameof(SharedOptions)));
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(Context)),
                                     sqlServerOptions => sqlServerOptions.CommandTimeout(timeoutInSec));
            });
        }

        public static void AddLogging(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information().ReadFrom.Configuration(configuration).CreateLogger();
            services.AddLogging(o => { o.AddSerilog(); });
        }

        public static void AddSharedServices(this IServiceCollection services)
        {
            services.AddTransient<IJsonService, JsonService>();
            services.AddTransient<IProxyDbService, ProxyDbService>();
            services.AddTransient<IExecutionHistoryService, ExecutionHistoryService>();
        }

        public static void AddDbServices(this IServiceCollection services)
        {
            services.AddTransient<IProxyDbService, ProxyDbService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<ITriggerService, TriggerService>();
        }

        public static void AddMappingProfile(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<JobProfile>();
                cfg.AddProfile<TriggerProfile>();
                cfg.AddProfile<CronTriggerProfile>();
                cfg.AddProfile<SimpleTriggerProfile>();
                cfg.AddProfile<DailyTriggerProfile>();
                cfg.AddProfile<CalendarTriggerProfile>();
            });

            services.AddAutoMapper(typeof(JobProfile));
            services.AddAutoMapper(typeof(TriggerProfile));
            services.AddAutoMapper(typeof(CronTriggerProfile));
            services.AddAutoMapper(typeof(SimpleTriggerProfile));
            services.AddAutoMapper(typeof(DailyTriggerProfile));
            services.AddAutoMapper(typeof(CalendarTriggerProfile));
        }
    }
}

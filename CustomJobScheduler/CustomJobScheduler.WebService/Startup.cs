using CustomJobScheduler.SharedService.Models;
using CustomJobScheduler.WebService.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartzmin;

namespace CustomJobScheduler.WebService
{
    public class Startup
    {
        private readonly IConfiguration _configuration; 

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings(_configuration);
            services.AddLogging(_configuration);
            services.AddSharedServices();
            services.AddDbServices();
            services.AddMappingProfile();
            services.AddQuartzmin();
            services.AddServiceConfigurationInstance(_configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UsePathBase("/" + ServiceConfigurationInstance.GetAppVirtualPath);
            app.UseQuartzmin(new QuartzminOptions
            {
                Scheduler = Schedulers.CustomJobScheduler.Create().Result
            });
        }
    }
}

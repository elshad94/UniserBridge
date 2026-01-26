using Project.Core.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Core.Utilities.Extensions
{
    public static class CorsPolicyExtensions
    {
        public static void AddApplicationCorsOrigins(this IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(AppSettings.Settings.CorsPolicyOrigins).AllowAnyHeader().AllowAnyMethod();
            }));
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using TCC_COMP.INFRA.DATA.Repository;
using TCC_COMP.SERVICE.Interfaces.Repository;
using TCC_COMP.SERVICE.Interfaces.Service;
using TCC_COMP.SERVICE.Services;

namespace TCC_COMP.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            #region Repository

            services.AddScoped<IDeviceRepository, DeviceRepository>();

            #endregion

            #region Services

            services.AddScoped<IDeviceService, DeviceService>();

            #endregion

            return services;

        }
    }
}

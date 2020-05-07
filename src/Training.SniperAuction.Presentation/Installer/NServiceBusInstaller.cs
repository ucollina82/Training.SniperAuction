using Microsoft.Extensions.DependencyInjection;
using Radical.Windows.Bootstrap;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Training.SniperAuction.Communication;

namespace Training.SniperAuction.Presentation.Installer
{
    public class NServiceBusInstaller : IDependenciesInstaller
    {
        public void Install(BootstrapConventions conventions, IServiceCollection services, IEnumerable<Type> assemblyScanningResults)
        {
            var endpoint = new NserviceBusEndpoint("sniper-1", services);

            services.AddSingleton(serviceProvider => {
                return new Lazy<NserviceBusEndpoint>(() =>
                {
                    endpoint.Start(serviceProvider);
                    return endpoint;
                });
            });
        }
    }
}

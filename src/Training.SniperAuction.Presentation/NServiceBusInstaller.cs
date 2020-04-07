using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using Radical.Windows.Bootstrap;
using System;
using System.Collections.Generic;

namespace Training.SniperAuction.Presentation
{
    public class NServiceBusInstaller : IDependenciesInstaller
    {
        public void Install(BootstrapConventions conventions, IServiceCollection services, IEnumerable<Type> assemblyScanningResults)
        {            
            var endpointConfiguration = new EndpointConfiguration("sniper-1");
            endpointConfiguration.UseTransport<LearningTransport>();
            var startableEndpoint = EndpointWithExternallyManagedServiceProvider
            .Create(endpointConfiguration, services);
            services.AddSingleton(serviceProvider =>
            {
                return new Lazy<IMessageSession>(() =>
                {
                    return startableEndpoint.Start(serviceProvider)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();
                });
            });          
        }
    }
}

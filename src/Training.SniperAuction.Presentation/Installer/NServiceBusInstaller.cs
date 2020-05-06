using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using Radical.Windows.Bootstrap;
using System;
using System.Collections.Generic;
using Training.SniperAuction.Communication;

namespace Training.SniperAuction.Presentation.Installer
{
    public class NServiceBusInstaller : IDependenciesInstaller
    {
        public void Install(BootstrapConventions conventions, IServiceCollection services, IEnumerable<Type> assemblyScanningResults)
        {
            var endpoint = new NserviceBusEndpoint("sniper-1", services);
            endpoint.StartLazy();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Training.SniperAuction.Communication
{
    public class NserviceBusEndpoint
    {
        private EndpointConfiguration endpointConfiguration;
        private IStartableEndpointWithExternallyManagedContainer startableEndPoint;
        private IEndpointInstance endPointInstance;
        
        public NserviceBusEndpoint(string endPointName, IServiceCollection services)
        {
            endpointConfiguration = new EndpointConfiguration(endPointName);
            endpointConfiguration.UseTransport<LearningTransport>();
            startableEndPoint = EndpointWithExternallyManagedServiceProvider
               .Create(endpointConfiguration, services);
        }

        public async Task<IEndpointInstance> StartAsync(IServiceProvider serviceProvider)
        {
            return this.endPointInstance = await startableEndPoint.Start(serviceProvider);
        }

        public void Start(IServiceProvider serviceProvider)
        {
            this.endPointInstance = startableEndPoint.Start(serviceProvider)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        }

        public async Task PublishAsync(IEvent @event) {
            await this.endPointInstance.Publish(@event);
        }

        public async Task SendAsync(string instanceName, ICommand command)
        {
            await this.endPointInstance.Send(instanceName, command);
        }

        public void Send(string instanceName, ICommand command)
        {
            this.endPointInstance
                .Send(instanceName, command)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public async Task StopAsync()
        {
            await this.endPointInstance.Stop().ConfigureAwait(false);
        }

        public void Stop()
        {
            this.endPointInstance
                .Stop()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

    }
}

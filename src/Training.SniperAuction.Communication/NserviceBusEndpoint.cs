using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using System.Threading.Tasks;

namespace Training.SniperAuction.Communication
{
    public class NserviceBusEndpoint
    {
        private EndpointConfiguration endpointConfiguration;
        private IServiceCollection services;
        private IStartableEndpointWithExternallyManagedContainer startableEndPoint;
        private IEndpointInstance endPointInstance;

        public NserviceBusEndpoint(string endPointName, IServiceCollection services)
        {
            endpointConfiguration = new EndpointConfiguration(endPointName);
            endpointConfiguration.UseTransport<LearningTransport>();
            this.services = services;
            startableEndPoint = EndpointWithExternallyManagedServiceProvider
               .Create(endpointConfiguration, services);
        }

        public void StartLazy()
        {
            services.AddSingleton(serviceProvider =>
            {
                return new System.Lazy<IMessageSession>(() =>
                {
                    this.endPointInstance = startableEndPoint.Start(serviceProvider)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();

                    return this.endPointInstance;
                });
            });
        }

        public async Task Start()
        {
            this.endPointInstance = await startableEndPoint.Start(this.services.BuildServiceProvider())
                       .ConfigureAwait(false);
        }

        public async Task Publish(IEvent @event) {
            await this.endPointInstance.Publish(@event);
        }

        public async Task Stop()
        {
            await this.endPointInstance.Stop().ConfigureAwait(false);
        }
    }
}

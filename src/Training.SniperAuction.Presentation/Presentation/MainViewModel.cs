using Microsoft.Extensions.Logging;
using Radical.Windows.ComponentModel;
using Radical.Windows.Presentation;
using System;
using Training.SniperAuction.Communication;
using Training.SniperAuction.Messages;

namespace Training.SniperAuction.Presentation.Presentation
{
    public class MainViewModel : AbstractViewModel, IExpectViewLoadedCallback
    { 
        private readonly Lazy<NserviceBusEndpoint> endpointInstance;
        private readonly ILogger<MainViewModel> logger;

        public MainViewModel(Lazy<NserviceBusEndpoint> endpointInstance, ILogger<MainViewModel> logger)
        {
            this.endpointInstance = endpointInstance;
            this.logger = logger;
        }

        public string Status
        {
            get { return this.GetPropertyValue(() => this.Status); }
            set { this.SetPropertyValue(() => this.Status, value); }
        }

        public void OnViewLoaded()
        {
            this.logger.LogDebug("MainViewModel OnViewLoaded");
            endpointInstance.Value.Send("AuctionServer", new Join("item-1234"));
            this.logger.LogDebug("Join sent for Sniper");
        }
    }
}

﻿using Microsoft.Extensions.Logging;
using Radical.ComponentModel.Messaging;
using Radical.Windows.ComponentModel;
using Radical.Windows.Presentation;
using System;
using Training.SniperAuction.Communication;
using Training.SniperAuction.Messages;
using Training.SniperAuction.Presentation.Messages;

namespace Training.SniperAuction.Presentation.Presentation
{
    public class MainViewModel : AbstractViewModel, IExpectViewLoadedCallback, IExpectViewClosedCallback
    {
        readonly IMessageBroker broker;
        private readonly Lazy<NserviceBusEndpoint> endpointInstance;
        private readonly ILogger<MainViewModel> logger;

        public MainViewModel(IMessageBroker broker, 
            Lazy<NserviceBusEndpoint> endpointInstance, 
            ILogger<MainViewModel> logger)
        {
            this.broker = broker;
            this.broker.Subscribe<JoinCompleted>(this, OnJoinCompleted);
            this.broker.Subscribe<AuctionClosed>(this, OnAuctionClosed);
            this.endpointInstance = endpointInstance;
            this.logger = logger ?? throw new ArgumentNullException("logger MainViewModel null");
        }

        private void OnAuctionClosed(object arg1, AuctionClosed arg2)
        {
            this.Status = PresentationConstValue.STATUS_LOST;
        }

        public void OnJoinCompleted(object sender, JoinCompleted message)
        {
            this.Status = PresentationConstValue.STATUS_JOINED;
        }

        public void OnViewLoaded()
        {
            this.logger.LogDebug("MainViewModel OnViewLoaded");
            endpointInstance.Value.Send("AuctionServer", new Join("item-1234"));
            this.logger.LogDebug("Join sent");            
        }

        public void OnViewClosed()
        {
            this.logger.LogDebug("MainViewModel OnViewClosed");
            endpointInstance.Value.StopAsync();
        }

        public string Status {
            get { return this.GetPropertyValue(() => this.Status); }
            set { this.SetPropertyValue(() => this.Status, value); }
        }
    }
}

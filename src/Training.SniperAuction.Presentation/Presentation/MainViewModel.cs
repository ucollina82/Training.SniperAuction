using NServiceBus;
using Radical.ComponentModel.Messaging;
using Radical.Windows.ComponentModel;
using Radical.Windows.Presentation;
using System;
using Training.SniperAuction.Messages;

namespace Training.SniperAuction.Presentation.Presentation
{
    public class MainViewModel : AbstractViewModel, IExpectViewLoadedCallback
    {
        readonly IMessageBroker broker;
        private readonly Lazy<IMessageSession> endpointInstance;

        public MainViewModel(IMessageBroker broker, Lazy<IMessageSession> endpointInstance)
        {
            this.broker = broker;

            this.broker.Subscribe<JoinCompleted>(this, OnJoinCompleted);
            this.broker.Subscribe<AuctionClosed>(this, OnAuctionClosed);

            this.endpointInstance = endpointInstance;
        }

        private void OnAuctionClosed(object arg1, AuctionClosed arg2)
        {
            this.Status = PresentationConstValue.STATUS_LOST;
            Console.WriteLine("On Auction Closed");
            Console.WriteLine("Status " + this.Status);
        }

        public void OnJoinCompleted(object sender, JoinCompleted message)
        {
            this.Status = PresentationConstValue.STATUS_JOINING;
            Console.WriteLine("On Join Completed");
            Console.WriteLine("Status " + this.Status);
        }

        public void OnViewLoaded()
        {
            Console.WriteLine("OnViewLoaded");
            var sendTask = endpointInstance.Value.Send("AuctionServer", new Join("item-1234"));
            sendTask.GetAwaiter().GetResult();
        }

        public string Status {
            get { return this.GetPropertyValue(() => this.Status); }
            set { this.SetPropertyValue(() => this.Status, value); }
        }
    }
}

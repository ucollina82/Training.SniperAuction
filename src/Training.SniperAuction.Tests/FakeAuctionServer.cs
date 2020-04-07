using NServiceBus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.SniperAuction.Messages;

namespace Training.SniperAuction.Tests
{
    internal class FakeAuctionServer : IHandleMessages<Join>
    {
        private IEndpointInstance endpointInstance;
        private string firstItemId;
        private IDictionary<string, double> currentSellingItems = new Dictionary<string, double>();
        private IDictionary<string, IList<string>> currentClientForItems = new Dictionary<string, IList<string>>();

        private FakeAuctionServer(string firstItemId)
        {
            this.firstItemId = firstItemId;
        }
        
        public FakeAuctionServer() { 
        
        }
        
        public static async Task<FakeAuctionServer> Create(string firstItemId) {
            Console.Title = "AuctionServer";

            var endpointConfiguration = new EndpointConfiguration("AuctionServer");
            endpointConfiguration.UseTransport<LearningTransport>();
            var server = new FakeAuctionServer(firstItemId);
            server.endpointInstance = await NServiceBus.Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            return server;
        }

        internal string GetItemId()
        {
            return firstItemId;
        }

        internal void StartSellingItem()
        {
            currentSellingItems.Add(firstItemId, 0.0);
        }

        internal async Task AnnounceClosed()
        {
            await endpointInstance.Publish(new Close());
        }

        internal async Task Stop()
        {
            await endpointInstance.Stop().ConfigureAwait(false);
        }

        public async Task Handle(Join message, IMessageHandlerContext context)
        {
            Console.WriteLine("Handle Join");
            if (!currentClientForItems.ContainsKey(message.ItemId)) 
                await Task.Run(() => currentClientForItems.Add(message.ItemId, new List<string>(new[] { context.ReplyToAddress })));
           
            await Task.Run(() => currentClientForItems[message.ItemId].Add(context.ReplyToAddress));
            await context.Reply(new Joined());
            Console.WriteLine("Reply Joined " + context.ReplyToAddress);
        }
    }
}
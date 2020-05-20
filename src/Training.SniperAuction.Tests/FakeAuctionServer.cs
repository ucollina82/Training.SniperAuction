using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.SniperAuction.Communication;

namespace Training.SniperAuction.Tests
{
    internal class FakeAuctionServer
    {
        private string firstItemId;
        private IDictionary<string, double> currentSellingItems = new Dictionary<string, double>();
        private ConcurrentDictionary<string, IList<string>> currentClientForItems = new ConcurrentDictionary<string, IList<string>>();
        private NserviceBusEndpoint nserviceBusEndpoint;

        private FakeAuctionServer(string firstItemId)
        {
            this.firstItemId = firstItemId;
        }

        internal void StartSellingItem()
        {
            currentSellingItems.Add(firstItemId, 0.0);
        }

        internal async Task HasReceivedJoinRequestFromSniper()
        {
            await Task.Run(() => currentClientForItems.Any().Should().BeTrue());
        }

        internal Task AnnounceClosed()
        {
            throw new NotImplementedException("AnnounceClosed");
        }

        public static async Task<FakeAuctionServer> Create(string firstItemId)
        {
            Console.Title = "AuctionServer";
            var server = new FakeAuctionServer(firstItemId);
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<FakeAuctionServer>(server);
            server.nserviceBusEndpoint = new NserviceBusEndpoint("AuctionServer", services);
            await server.nserviceBusEndpoint.StartAsync(services.BuildServiceProvider());
            return server;
        }
    }
}
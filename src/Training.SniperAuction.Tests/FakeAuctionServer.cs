﻿using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using Radical.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.SniperAuction.Communication;
using Training.SniperAuction.Messages;

namespace Training.SniperAuction.Tests
{
    internal class FakeAuctionServer : IHandleMessages<Join>
    {
        private string firstItemId;
        private IDictionary<string, double> currentSellingItems = new Dictionary<string, double>();
        private ConcurrentDictionary<string, IList<string>> currentClientForItems = new ConcurrentDictionary<string, IList<string>>();
        private NserviceBusEndpoint nserviceBusEndpoint;

        private FakeAuctionServer(string firstItemId)
        {
            this.firstItemId = firstItemId;
        }
        
        public static async Task<FakeAuctionServer> Create(string firstItemId) {
            Console.Title = "AuctionServer";
            var server = new FakeAuctionServer(firstItemId);
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<FakeAuctionServer>(server);
            server.nserviceBusEndpoint = new NserviceBusEndpoint("AuctionServer", services); 
            await server.nserviceBusEndpoint.StartAsync(services.BuildServiceProvider());
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
            await nserviceBusEndpoint.PublishAsync(new Close()); 
        }

        internal async Task Stop()
        {
            await nserviceBusEndpoint.StopAsync(); 
        }

        public async Task HasReceivedJoinRequestFromSniper() 
        {
            await Task.Run(() => currentClientForItems.Any().Should().BeTrue());
        }

        public async Task Handle(Join message, IMessageHandlerContext context)
        {            
            currentClientForItems.TryAdd(message.ItemId, new List<string>(new[] { context.ReplyToAddress }));

            currentClientForItems[message.ItemId].Add(context.ReplyToAddress);

            await context.Reply(new Joined());
        }
    }
}
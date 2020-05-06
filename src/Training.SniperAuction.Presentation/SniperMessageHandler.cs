using Microsoft.Extensions.Logging;
using NServiceBus;
using Radical.ComponentModel.Messaging;
using System;
using System.Threading.Tasks;
using Training.SniperAuction.Messages;
using Training.SniperAuction.Presentation.Messages;

namespace Training.SniperAuction.Presentation
{
    public class SniperMessageHandler : IHandleMessages<Joined>, IHandleMessages<Close>
    {
        private readonly IMessageBroker messageBroker;
        private readonly ILogger<SniperMessageHandler> logger;

        public SniperMessageHandler(IMessageBroker messageBroker, ILogger<SniperMessageHandler> logger)
        {
            this.messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
            this.logger = logger;
        }

        public async Task Handle(Joined message, IMessageHandlerContext context)
        {
            logger.LogDebug("Sniper joined message received");
            this.messageBroker.Dispatch(this, new JoinCompleted());
            await Task.FromResult(false);
        }

        public async Task Handle(Close message, IMessageHandlerContext context)
        {
            logger.LogDebug("Sniper close message received");
            this.messageBroker.Dispatch(this, new AuctionClosed());
            await Task.FromResult(false);
        }
    }
}

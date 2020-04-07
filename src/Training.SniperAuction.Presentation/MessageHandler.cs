using NServiceBus;
using Radical.ComponentModel.Messaging;
using Radical.Messaging;
using System;
using System.Threading.Tasks;
using Training.SniperAuction.Messages;
using Training.SniperAuction.Presentation.Presentation;

namespace Training.SniperAuction.Presentation
{
    public class MessageHandler : IHandleMessages<Joined>, IHandleMessages<Close>
    {
        private readonly IMessageBroker messageBroker;

        public MessageHandler(IMessageBroker messageBroker)
        {
            this.messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
        }

        public async Task Handle(Joined message, IMessageHandlerContext context)
        {
            Console.WriteLine("Handle Joined, dispatch JoinCompleted");
            this.messageBroker.Dispatch(this, new JoinCompleted());
            Console.WriteLine("Handle Joined, dispatched JoinCompleted");
            await Task.FromResult(false);
        }

        public async Task Handle(Close message, IMessageHandlerContext context)
        {
            Console.WriteLine("Handle Close, dispatch AuctionClosed");
            this.messageBroker.Dispatch(this, new AuctionClosed());
            Console.WriteLine("Handle Close, dispatched AuctionClosed");
            await Task.FromResult(false);
        }
    }
}

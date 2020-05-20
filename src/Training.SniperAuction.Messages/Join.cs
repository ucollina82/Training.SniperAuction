using NServiceBus;

namespace Training.SniperAuction.Messages
{
    public class Join: ICommand
    {
        public Join(string itemId)
        {
            ItemId = itemId;
        }

        public string ItemId { get; }
    }
}

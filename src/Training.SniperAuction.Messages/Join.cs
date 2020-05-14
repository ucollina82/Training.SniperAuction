using NServiceBus;

namespace Training.SniperAuction.Messages
{
    public class Join : ICommand
    {
        public string ItemId { get; set; }

        public Join(string itemId)
        {
            this.ItemId = itemId;
        }
    }
}

using NUnit.Framework;
using System.Threading.Tasks;

namespace Training.SniperAuction.Tests
{
    [TestFixture]
    public class AuctionSniperEndToEndTests
    {
        private FakeAuctionServer auction;
        private ApplicationRunner application;

        [SetUp]
        public async Task Setup()
        {
            auction = await FakeAuctionServer.Create("item-1234");
            application = new ApplicationRunner();
        }

        [Test]
        public async Task sniperJoinsAuctionUntilAuctionCloses()
        {
            auction.StartSellingItem(); // 1. When an auction is selling an item,
            application.StartBiddingIn(); // 2. And an Auction Sniper has started to bid in that auction,
            await auction.HasReceivedJoinRequestFromSniper(); // 3.Then the auction will receive a Join request from the Auction Sniper.
            await auction.AnnounceClosed(); // 4. When an auction announces that it is Closed,
            application.ShowsSniperHasLostAuction(); // 5.Then the Auction Sniper will show that it lost the auction.
        }

    }
}
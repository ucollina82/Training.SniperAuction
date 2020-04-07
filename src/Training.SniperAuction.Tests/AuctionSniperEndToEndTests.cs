using NUnit.Framework;
using System.Threading.Tasks;

namespace Training.SniperAuction.Tests
{
    [TestFixture]
    public class AuctionSniperEndToEndTests
    {
        private FakeAuctionServer auction = null;
        private ApplicationRunner application = new ApplicationRunner();

        [SetUp]
        public async Task Setup()
        {
            auction = await FakeAuctionServer.Create("item-1234");
        }

        [Test]
        public async Task sniperJoinsAuctionUntilAuctionCloses()
        {
            auction.StartSellingItem();
            application.StartBiddingIn(auction);
            await auction.AnnounceClosed();
            application.ShowsSniperHasLostAuction();
        }

        [TearDown]
        public async Task TearDown()
        {
            await auction?.Stop();
            application?.Dispose();
        }
    }
}
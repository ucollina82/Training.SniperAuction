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
            application =  new ApplicationRunner();
        }

        [Test]
        public async Task sniperJoinsAuctionUntilAuctionCloses()
        {
            auction.StartSellingItem();
            application.StartBiddingIn();
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
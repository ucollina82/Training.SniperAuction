using NUnit.Framework;

namespace Training.SniperAuction.Tests
{
    [TestFixture]
    public class AuctionSniperEndToEndTests
    {
        //private FakeAuctionServer auction = new FakeAuctionServer("item-54321");
        //private ApplicationRunner application = new ApplicationRunner();

        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        //public void sniperJoinsAuctionUntilAuctionCloses()
        //{
        //    //auction.startSellingItem();
        //    //application.startBiddingIn(auction);
        //    //auction.announceClosed();
        //    //application.showsSniperHasLostAuction();
        //}

        [TearDown]
        private void cleanup()
        {
            //auction.stop();
            //application.stop();
        }
    }
}
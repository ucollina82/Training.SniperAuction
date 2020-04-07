using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Training.SniperAuction.Presentation;
using static Training.SniperAuction.Presentation.Presentation.PresentationConstValue;

namespace Training.SniperAuction.Tests
{
    internal class ApplicationRunner : IDisposable
    {
        public const String SNIPER_ID = "sniper";
        public const String SNIPER_PASSWORD = "sniper";
        private AuctionSniperDriver driver;
        Thread testApplicationThread = null;

        internal void StartBiddingIn(FakeAuctionServer auction)
        {
            Thread testApplicationThread = new Thread(
                new ThreadStart(() =>
                {
                    try
                    {
                        Console.WriteLine("Parte thread dell'applicazione");
                        App.Main();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("eccezione : " + ex.ToString());
                    }
            }));

            testApplicationThread.SetApartmentState(ApartmentState.STA);
            testApplicationThread.Start();
            testApplicationThread.Join(5000);

            driver = new AuctionSniperDriver(1000);
            driver.ShowsSniperStatus(STATUS_JOINING);

            Console.WriteLine("Esco dal metodo StartBiddingIn");
        }
        
        internal void ShowsSniperHasLostAuction()
        {
            Thread.Sleep(2000);
            driver.ShowsSniperStatus(STATUS_LOST);
        }


        public void Dispose()
        {
            driver?.Dispose();
            testApplicationThread?.Abort();
        }
    }
}
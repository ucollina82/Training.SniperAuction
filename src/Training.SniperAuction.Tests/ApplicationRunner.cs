using System;
using System.Threading;
using Training.SniperAuction.Presentation;
using static Training.SniperAuction.Presentation.Presentation.PresentationConstValue;

namespace Training.SniperAuction.Tests
{
    internal class ApplicationRunner : IDisposable
    {
        private AuctionSniperDriver driver;
        Thread testApplicationThread = null;

        internal void StartBiddingIn()
        {
            testApplicationThread = new Thread(
                new ThreadStart(() =>
                {
                    try
                    {                        
                        App.Main();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("eccezione : " + ex.ToString());
                    }
            }));

            testApplicationThread.SetApartmentState(ApartmentState.STA);
            testApplicationThread.Start();
            testApplicationThread.Join(10000);
            driver = new AuctionSniperDriver();
            driver.ShowsSniperStatus(STATUS_JOINED);
        }
        
        internal void ShowsSniperHasLostAuction()
        {
            Thread.Sleep(2000);
            driver.ShowsSniperStatus(STATUS_LOST); 
        }


        public void Dispose()
        {
            driver?.Dispose();
            testApplicationThread?.Interrupt();
        }
    }
}
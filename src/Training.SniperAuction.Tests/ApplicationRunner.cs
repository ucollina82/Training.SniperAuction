using System;
using System.Threading;
using System.Threading.Tasks;
using Training.SniperAuction.Presentation;
using static Training.SniperAuction.Presentation.Presentation.PresentationConstValue;

namespace Training.SniperAuction.Tests
{
    internal class ApplicationRunner : IDisposable
    {
        private AuctionSniperDriver driver;
        Thread testApplicationThread = null;
        readonly AutoResetEvent resetEvent = new AutoResetEvent(false);

        internal void StartBiddingIn()
        {
            Task.Factory.StartNew(() => { });
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
            
            driver = new AuctionSniperDriver();
            driver.ShowsSniperStatus(STATUS_JOINED, resetEvent);
            resetEvent.WaitOne();
        }
        
        internal void ShowsSniperHasLostAuction()
        {
            driver.ShowsSniperStatus(STATUS_LOST, resetEvent);
            resetEvent.WaitOne();
        }


        public void Dispose()
        {
            driver?.Dispose();
            testApplicationThread?.Interrupt();
        }
    }
}
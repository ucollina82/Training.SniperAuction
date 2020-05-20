using System;
using System.Collections.Generic;
using System.Threading;
using Training.SniperAuction.Presentation;

namespace Training.SniperAuction.Tests
{
    public class ApplicationRunner
    {
        Action StartUIAction = App.Main;
        IList<Thread> currentThreads = new List<Thread>();
        private AuctionSniperDriver driver;

        internal void StartBiddingIn()
        {
            ExecuteInSTAThread(StartUIAction);
            driver = new AuctionSniperDriver();
            driver.ShowsSniperStatus(PresentationConstValue.STATUS_JOINED);
        }

        private void ExecuteInSTAThread(Action action)
        {
            var staThread = new Thread(new ThreadStart(action));
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            currentThreads.Add(staThread);
        }
        internal void ShowsSniperHasLostAuction()
        {
            throw new NotImplementedException("ShowsSniperHasLostAuction");
        }
    }
}
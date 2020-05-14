using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using Training.SniperAuction.Presentation;
using static Training.SniperAuction.Presentation.Presentation.PresentationConstValue;

namespace Training.SniperAuction.Tests
{
    internal class ApplicationRunner : IDisposable
    {
        private AuctionSniperDriver driver;
        IList<Thread> currentThreads = new List<Thread>();
        Action StartUIAction = App.Main;

        internal void StartBiddingIn()
        {
            ExecuteInSTAThread(StartUIAction);
            driver = new AuctionSniperDriver();
            driver.ShowsSniperStatus(STATUS_JOINED);
        }
        
        private void ExecuteInSTAThread(Action action) {

            var staThread = new Thread(new ThreadStart(action));
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            currentThreads.Add(staThread); 
        }

        internal void ShowsSniperHasLostAuction()
        {
            driver.ShowsSniperStatus(STATUS_LOST);
        }

        public void Dispose()
        {
            driver?.Dispose();
            foreach (var item in currentThreads)
            {
                item.Interrupt();
            }
        }
    }
}
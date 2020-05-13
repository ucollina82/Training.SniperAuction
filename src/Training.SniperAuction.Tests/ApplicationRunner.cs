using NUnit.Framework;
using Polly;
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
        Action StartUIAction = () => App.Main();

        internal void StartBiddingIn()
        {
            ExecuteInSTAThread(StartUIAction);
            driver = new AuctionSniperDriver();
            ExecuteWithRetry(() => driver.ShowsSniperStatus(STATUS_JOINED));
        }
        
        internal void ShowsSniperHasLostAuction()
        {
            ExecuteWithRetry(() => driver.ShowsSniperStatus(STATUS_LOST));
        }

        private void ExecuteInSTAThread(Action action) {

            var staThread = new Thread(
               new ThreadStart(() =>
               {
                   try
                   {
                       action();
                   }
                   catch (Exception ex)
                   {
                       Assert.AreEqual(0, 1, ex.ToString());
                   }
               }));
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            currentThreads.Add(staThread); 
        }

        private void ExecuteWithRetry(Action action) {
            var retryActionPolicy = Policy
                   .Handle<Exception>()
                   .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            retryActionPolicy.Execute(() => action());
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
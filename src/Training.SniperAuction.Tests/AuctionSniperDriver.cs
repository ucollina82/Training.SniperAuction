using FluentAssertions;
using System;
using System.Threading;
using System.Windows.Controls;
using Training.SniperAuction.Presentation;

namespace Training.SniperAuction.Tests
{
    public class AuctionSniperDriver
    {
        private int timeoutMillis;

        public AuctionSniperDriver(int timeoutMillis)
        {
            this.timeoutMillis = timeoutMillis;
        }

        internal void ShowsSniperStatus(string status)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Console.WriteLine("ShowsSniperStatus");
                var mainView = App.ViewResolver.GetView<Presentation.Presentation.MainView>();
                ((TextBlock)mainView.FindName("Status")).Text.Should().Be(status);
            });
        }

        internal void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

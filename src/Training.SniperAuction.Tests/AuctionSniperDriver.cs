using FluentAssertions;
using Polly;
using System;
using System.Threading;
using System.Windows.Controls;
using Training.SniperAuction.Presentation;

namespace Training.SniperAuction.Tests
{
    public class AuctionSniperDriver
    {
        internal void ShowsSniperStatus(string status, AutoResetEvent resetEvent)
        {
            var retryActionPolicy = Policy
                    .Handle<Exception>()
                    .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            retryActionPolicy.Execute(() =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    var mainView = App.ViewResolver.GetView<Presentation.Presentation.MainView>();
                    string text = ((TextBlock)mainView.FindName("Status")).Text;
                    text.Should().Be(status);

                });
            });
            resetEvent.Set();
        }

        internal void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

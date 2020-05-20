using FluentAssertions;
using Polly;
using System;
using System.Windows;
using System.Windows.Controls;
using Training.SniperAuction.Presentation;
using Training.SniperAuction.Presentation.Presentation;

namespace Training.SniperAuction.Tests
{
    public class AuctionSniperDriver
    {
        internal void ShowsSniperStatus(string status)
        {
            ExecuteWithRetry(() => RunOnUIThread(() =>
            {
                string text = Application.Current.MainWindow.FindElement<TextBlock>(PresentationConstValue.STATUS_LABEL_NAME).Text;
                text.Should().Be(status);
            }));
        }

        private void RunOnUIThread(Action action)
        {
            App.Current.Dispatcher.Invoke(() => action());
        }

        private void ExecuteWithRetry(Action action)
        {
            var retryActionPolicy = Policy
                   .Handle<Exception>()
                   .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            retryActionPolicy.Execute(() => action());
        }

        internal void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

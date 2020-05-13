using FluentAssertions;
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
            RunOnUIThread(() =>
            {
                string text = Application.Current.MainWindow.FindElement<TextBlock>(PresentationConstValue.STATUS_LABEL_NAME).Text;
                text.Should().Be(status);
            });
        }

        private void RunOnUIThread(Action action)
        {
            App.Current.Dispatcher.Invoke(() => action());
        }

        internal void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

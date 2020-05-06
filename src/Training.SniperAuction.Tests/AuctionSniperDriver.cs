using FluentAssertions;
using System.Windows.Controls;
using Training.SniperAuction.Presentation;

namespace Training.SniperAuction.Tests
{
    public class AuctionSniperDriver
    {
        internal void ShowsSniperStatus(string status)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
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

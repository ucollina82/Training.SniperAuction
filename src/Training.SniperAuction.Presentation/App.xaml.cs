using System.Windows;

namespace Training.SniperAuction.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.MainView>();
        }
    }
}

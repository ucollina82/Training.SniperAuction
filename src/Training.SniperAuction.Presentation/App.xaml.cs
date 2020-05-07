using Microsoft.Extensions.DependencyInjection;
using Radical.Windows.ComponentModel;
using System.Windows;

namespace Training.SniperAuction.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IViewResolver ViewResolver = null;

        public App()
        {
           this.AddRadicalApplication<Presentation.MainView>((configuration) =>
                {
                    // TODO 3: remove. Found only for test purpose...
                    configuration.OnBootCompleted((serviceProvider) => ViewResolver = serviceProvider.GetRequiredService<IViewResolver>());                    
                });            
        }
    }
}

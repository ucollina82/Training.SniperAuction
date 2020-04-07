using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using Radical.ComponentModel.Messaging;
using Radical.Windows.ComponentModel;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Training.SniperAuction.Messages;
using Training.SniperAuction.Presentation.Presentation;

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
            IServiceProvider container = null;
            this.AddRadicalApplication<Presentation.MainView>((configuration) =>
                configuration.OnBootCompleted((serviceProvider) =>
                {
                    container = serviceProvider;
                    ViewResolver = container.GetRequiredService<IViewResolver>(); //only for test purpose...
                    Console.WriteLine("OnBootCompleted");
                })
            );
        }
    }
}

﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Radical.Windows.ComponentModel;
using System;
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
            IServiceProvider container = null;
            
            this.AddRadicalApplication<Presentation.MainView>((configuration) =>
                configuration.OnBootCompleted((serviceProvider) =>
                {
                    container = serviceProvider;
                    ViewResolver = container.GetRequiredService<IViewResolver>(); // TODO 3: remove. Found only for test purpose...
                    serviceProvider.GetRequiredService<ILogger<App>>().LogDebug("Starting App");
                })
            );
        }
    }
}

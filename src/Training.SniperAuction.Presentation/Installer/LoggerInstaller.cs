using Microsoft.Extensions.DependencyInjection;
using Radical.Windows.Bootstrap;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.SniperAuction.Presentation.Installer
{
    public class LoggerInstaller : IDependenciesInstaller
    {
        public void Install(BootstrapConventions conventions, IServiceCollection services, IEnumerable<Type> assemblyScanningResults)
        {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .Enrich.FromLogContext()
                        .WriteTo
                        .Console().CreateLogger();

            services.AddLogging(e => e.AddSerilog());
        }
    }
}

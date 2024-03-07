using Microsoft.Extensions.DependencyInjection;
using Grain.IO.TGClient.Handlers;
using Microsoft.Extensions.Configuration;
using Grain.IO.TGClient.Models;

namespace Grain.IO.TGClient;

class Program
{
    private static ServiceProvider ConfigureServices()
    {
        var serviceProvider = new ServiceCollection()
            .AddTransient<IGeneralResponseHandler, GeneralResponseHandler>();

        return serviceProvider.BuildServiceProvider();
    }

    private static IConfigurationRoot BuildConfigurations()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configs"))
            .AddJsonFile(path: "appconfig.json", optional: false, reloadOnChange: true)
            .Build();
    }

    static void Main(string[] _)
    {
        var serviceProvider = ConfigureServices();
        var config = BuildConfigurations();

        var configModel = new ConfigurationModel();
        config.Bind(new ConfigurationModel());

        //services.AddSingleton<IConfigurationModel>(configModel);

        new Bootstraper().Initialize(serviceProvider);
        //resolve Bootstraper
    }
}
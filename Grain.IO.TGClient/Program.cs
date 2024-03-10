using Microsoft.Extensions.DependencyInjection;
using Grain.IO.TGClient.Handlers;
using Microsoft.Extensions.Configuration;
using Grain.IO.TGClient.Models;
using Grain.IO.TGClient.Common;

namespace Grain.IO.TGClient;

class Program
{
    private static ServiceProvider ConfigureServices(IConfigurationRoot configModel)
    {
        var serviceProvider = new ServiceCollection()
            .AddTransient<IBootstraper, Bootstraper>()
            .AddTransient<IGeneralResponseHandler, GeneralResponseHandler>()
            .Configure<ConfigurationModel>(configModel.GetSection(Constants.AppSettings));

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
        var config = BuildConfigurations();

        var configModel = new ConfigurationModel();
        config.Bind(configModel);

        var serviceProvider = ConfigureServices(config);

        //Resolve bootstraper
        serviceProvider.GetService<IBootstraper>().Initialize();

        Console.ReadLine();
    }
}
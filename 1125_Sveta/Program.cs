using Avalonia;
using System;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using _1125_Sveta.ViewModels;
using _1125_Sveta.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _1125_Sveta;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder().
            ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsetting.json")
                    .AddEnvironmentVariables();
            }).
            ConfigureServices((c,s) =>
            {
                s.Configure<DataBaseConnection>(c.Configuration.
                    GetSection("DataBaseConnection"));
                s.AddTransient<MainWindow>();
                s.AddTransient<MainWindowViewModel>();
                s.AddTransient<ProductsRepository>();
                s.AddTransient<WareHouseRepository>();
            }).
            Build();
        BuildAvaloniaApp(host.Services)
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp(IServiceProvider serviceProvider)
        => AppBuilder.Configure(()=> new App(serviceProvider))
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
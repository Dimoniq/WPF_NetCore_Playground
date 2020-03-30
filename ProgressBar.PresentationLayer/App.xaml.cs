using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prism.Events;
using ProgressBar.BusinessLayer;
using ProgressBar.Contracts;
using ProgressBar.PresentationLayer.ViewModels;
using ProgressBar.PresentationLayer.Views;


namespace ProgressBar.PresentationLayer
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public IServiceProvider ServiceProvider { get; set; }
    public IConfiguration Configuration { get; set; } 
    public App()
    {
      var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
      this.Configuration = builder.Build();

      var serviceCollection = new ServiceCollection();
      ConfigureServices(serviceCollection);
      this.ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
      var text = this.Configuration.GetSection("AppSettings");

      services.AddSingleton<MainWindow>();
      services.AddSingleton<MainWindowViewModel>();
      services.AddSingleton<IEventAggregator, EventAggregator>();
      services.AddTransient<IReportManager, ReportManager>();
    }


    protected override void OnStartup(StartupEventArgs e)
    {
      var mainWindow = this.ServiceProvider.GetService<MainWindow>();
      mainWindow.Show();
    }
  }
}
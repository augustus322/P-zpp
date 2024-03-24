using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Windows;
using WyszukiwarkaOI.Domain.Models;
using WyszukiwarkaOI.Domain.Services;
using WyszukiwarkaOI.EntityFramework;
using WyszukiwarkaOI.EntityFramework.Services;
using WyszukiwarkaOI.WPF.Stores;
using WyszukiwarkaOI.WPF.ViewModels;
using WyszukiwarkaOI_webScraper;
using WyszukiwarkaOI_webScraper.Services;

namespace WyszukiwarkaOI.WPF;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	private readonly IHost _host;

	public App()
	{
		_host = CreateHostBuilder().Build();
	}

	public static IHostBuilder CreateHostBuilder(string[]? args = null)
	{
		return Host.CreateDefaultBuilder(args)
			.ConfigureAppConfiguration(config =>
			{
				config.AddJsonFile("appsettings.json");
			})
			.ConfigureServices((context, services) =>
			{
				string connectionString = context.Configuration.GetConnectionString("Default")!;

				//services.AddSingleton<DbConnection, SqliteConnection>(services =>
				//{
				//	var connection = new SqliteConnection(connectionString);
				//	connection.Open();

				//	return connection;
				//});

				//            services.AddDbContext<AppDbContext>((services, options) =>
				//{
				//	var connection = services.GetRequiredService<DbConnection>();
				//	options.UseSqlite(connection);
				//});

				Action<DbContextOptionsBuilder> configureDbContext = options => options.UseSqlite(connectionString);

				services.AddDbContext<AppDbContext>(configureDbContext);
				services.AddSingleton(new AppDbContextFactory(configureDbContext));

				services.AddSingleton<WebScraper>();
				services.AddSingleton<WebScraperService>();
				services.AddSingleton<HttpClient>();

				services.AddSingleton<IDataService<Product>, DataService<Product>>();

				services.AddSingleton<ProductStore>();

				services.AddSingleton<MainWindowViewModel>();

				services.AddSingleton(services =>
					new MainWindow(services.GetRequiredService<MainWindowViewModel>())
				);
			});
	}

	protected override async void OnStartup(StartupEventArgs e)
	{
		_host.Start();

		AppDbContextFactory contextFactory = _host.Services.GetRequiredService<AppDbContextFactory>();
		using (AppDbContext context = contextFactory.CreateDbContext())
		{
			context.Database.Migrate();
		}

		Window window = _host.Services.GetRequiredService<MainWindow>();
		window.Show();

		base.OnStartup(e);
	}

	protected override async void OnExit(ExitEventArgs e)
	{
		await _host.StopAsync();
		_host.Dispose();

		base.OnExit(e);
	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using WyszukiwarkaOI.EntityFramework;
using WyszukiwarkaOI.EntityFramework.Models;
using WyszukiwarkaOI_webScraper;

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

				services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
				services.AddSingleton(new AppDbContextFactory(connectionString));
				services.AddScoped<WebScraper, WebScraper>(); //dodane - zarejestrowany web scraper 
				services.AddScoped<HttpClient, HttpClient>(); //dodane
			});
	}

	protected override async void OnStartup(StartupEventArgs e)
	{
		//_host.Start();

		//string baseAddress = "http://www.okazje.info.pl/";
		//string searchPhrase = "rower"; // get that value from search bar

		//string url = $"{baseAddress}search/?q={searchPhrase}";

		//using var scope = _host.Services.CreateScope(); //dodane - pobranie scope

  //      var services = scope.ServiceProvider; //dodane - wyciąganie samych serwisów ze scope
		//var webScraper = services.GetRequiredService<WebScraper>(); //dodane - zwrócenie jednego serwis

		////WebScraper webScraper = new WebScraper();

		//(string? html, bool isSuccess) = await webScraper.GetWebsiteHtmlAsync(url);

		//if (!isSuccess)
		//{
		//	return;
		//}

		//var result = await webScraper.GetChildrenOfGivenElementAsync(".productsBox", html!);

		//if (!result.isSuccess)
		//{
		//	return;
		//}

		//IEnumerable<AngleSharp.Dom.IElement> products = result.children!;

		//Func<string, string, decimal, string, string?, Product> ctor = (p1, p2, p3, p4, p5) => new Product(p1, p2, p3, p4, p5);

		//var scraperResult = webScraper.GetElementsInfo(products, ctor);

		//if (!scraperResult.isSuccess)
		//{
		//	return;
		//}

		//// add products info to database

		//Console.ReadLine();
		base.OnStartup(e);
	}

	protected override async void OnExit(ExitEventArgs e)
	{
		await _host.StopAsync();
		_host.Dispose();

		base.OnExit(e);
	}
}

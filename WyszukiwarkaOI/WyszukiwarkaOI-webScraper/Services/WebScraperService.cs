using WyszukiwarkaOI.Domain.Models;

namespace WyszukiwarkaOI_webScraper.Services;

public class WebScraperService(WebScraper webScraper)
{
	private readonly WebScraper _webScraper = webScraper;

	public async Task<List<Product>> GetElementsInfoAsync(string url)
	{
		(List<Product> result, string _) = await RunWebScraper(url);

		return result;
	}

	public async Task<(IEnumerable<Product> results, int? nextPageNumber)> GetElementsInfoWithPaginationAsync(string url)
	{
		(List<Product> result, string html) = await RunWebScraper(url);

		var getNextPageNumberResult = await _webScraper.GetNextPageNumberAsync(html);

		return (result, getNextPageNumberResult.nextPageNumber);
	}

	private async Task<(List<Product> results, string html)> RunWebScraper(string url)
	{
		string parentCssSelector = ".productsBox";

		var getHtmlResult = await _webScraper.GetWebsiteHtmlAsync(url);

		var GetChildrenOfElementResult = await _webScraper.GetChildrenOfGivenElementAsync(parentCssSelector, getHtmlResult.html!);

		IEnumerable<AngleSharp.Dom.IElement> products = GetChildrenOfElementResult.children!;

		Func<string, string, decimal, string, string?, Product> ctor = (p1, p2, p3, p4, p5) =>
		{
			return new Product(p1, p2, p3, p4, p5);
		};

		var (scraperResult, success) = _webScraper.GetElementsInfo(products, ctor);

		List<Product> elementsInfo = scraperResult ?? new List<Product>();
		return (elementsInfo, getHtmlResult.html!);
	}
}

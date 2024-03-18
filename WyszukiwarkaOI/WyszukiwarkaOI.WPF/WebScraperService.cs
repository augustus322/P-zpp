﻿using WyszukiwarkaOI_webScraper;
using WyszukiwarkaOI.EntityFramework;

namespace WyszukiwarkaOI.WPF;
public class WebScraperService(WebScraper webScraper)
{
	private readonly WebScraper _webScraper = webScraper;

	public async Task<List<Product>> GetElementsInfo(string url)
	{
		string parentCssSelector = ".productsBox";

		var getHtmlResult = await _webScraper.GetWebsiteHtmlAsync(url);

		var GetChildrenOfElementResult = await _webScraper.GetChildrenOfGivenElementAsync(parentCssSelector, getHtmlResult.html!);

		IEnumerable<AngleSharp.Dom.IElement> products = GetChildrenOfElementResult.children!;

		Func<string, string, decimal, string, string?, Product> ctor = (p1, p2, p3, p4, p5) =>
		{
			return new Product(p1, p2, p3, p4, p5);
		};

		var scraperResult = _webScraper.GetElementsInfo(products, ctor);

		return scraperResult.elementsInfo!;
	}
}
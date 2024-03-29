using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System.Net;

namespace WyszukiwarkaOI_webScraper;

public class WebScraper
{
	private readonly HttpClient _httpClient;

	public WebScraper(HttpClient httpClient)
	{
		_httpClient = httpClient;

		_httpClient.DefaultRequestHeaders.Add("Accept", "text/html");
		_httpClient.DefaultRequestHeaders.Add("User-Agent", "web scraper");
	}

	/// <summary>
	/// Asynchronously retrieves the HTML code of a webpage.
	/// </summary>
	/// <param name="url">The URL of the webpage from which the HTML code is to be retrieved.</param>
	/// <returns>A tuple containing the HTML code of the webpage and a boolean indicating whether the operation was successful.</returns>
	/// <remarks>
	/// The method performs an asynchronous GET request to the specified URL.
	/// <para>
	/// If the request is successful, the method returns the HTML code of the webpage and true.
	/// In case of a request error, the method returns null for the HTML code and false for isSuccess.
	/// </para>
	/// </remarks>
	/// <exception cref="HttpRequestException">Thrown when the request is not successful.</exception>
	public async Task<(string? html, bool isSuccess)> GetWebsiteHtmlAsync(string url)
	{
		var requestUri = new Uri(url);

		try
		{
			using HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestUri);

			httpResponse.EnsureSuccessStatusCode();
			string html = await httpResponse.Content.ReadAsStringAsync();

			return (html, true);
		}
		catch (HttpRequestException e)
		{
			return (null, false);
		}
	}

	/// <summary>
	/// Retrieves the children of a given HTML element based on a CSS selector.
	/// </summary>
	/// <param name="cssSelector">The CSS selector to identify the parent element.</param>
	/// <param name="html">The HTML content to search within.</param>
	/// <returns>
	/// A tuple containing:
	/// <para>- An enumerable of <see cref="IElement"/> representing the children elements (or null if the parent element is not found).</para>
	/// <para>- A boolean indicating whether the operation was successful.</para>
	/// </returns>
	public async Task<(IEnumerable<IElement>? children, bool isSuccess)> GetChildrenOfGivenElementAsync(string cssSelector, string html)
	{
		if(cssSelector is null)
		{
            return (null, false);
        }

		IBrowsingContext context = BrowsingContext.New(Configuration.Default);

		IDocument document = await context.OpenAsync(req => req.Content(html));

		IElement? parentElement = document.QuerySelector(cssSelector);

		if (parentElement is null)
		{
			return (null, false);
		}

		IEnumerable<IElement> children = parentElement.Children.Where(c => c is IHtmlDivElement);

		return (children, true);
	}

	/// <summary>
	/// Retrieves information about a collection of HTML elements.
	/// </summary>
	/// <typeparam name="T">The type of element information to retrieve.</typeparam>
	/// <param name="elements">The enumerable of HTML elements to process.</param>
	/// <param name="ctor">A delegate that constructs an instance of type T based on element properties.</param>
	/// <returns>
	/// A tuple containing:
	/// - A list of type T representing the extracted element information (or null if unsuccessful).
	/// - A boolean indicating whether the operation was successful.
	/// </returns>
	public (List<T>? elementsInfo, bool isSuccess) GetElementsInfo<T>(IEnumerable<IElement> elements, Func<string, string, decimal, string, string?, T> ctor) where T : class
	{
		var result = new List<T>();

		foreach (var product in elements)
		{
			string productName = product.QuerySelector(".pB__i--name")?.TextContent ?? string.Empty;
			string shopName = product.QuerySelector(".pB__i--shop")?.TextContent ?? string.Empty;
			string textPrice = product.QuerySelector(".pB__i--price")?.TextContent ?? string.Empty;
			string? productDescription = product.QuerySelector(".pB__i--desc")?.TextContent;

			string shopLink = product.QuerySelector("a.pB__href")?
				.Attributes.SingleOrDefault(a => a.Name == "data-r-hash")?
				.TextContent ?? string.Empty;

			decimal price = 0;

			string tmp = textPrice.Substring(0, textPrice.Length - 3)
				.Replace(',', '.').Replace(" ", "");


			if (!decimal.TryParse(tmp, out price))
			{
				//add some error handling

				// temporary
				return (null, false);
			}

			string imageUrl = product.QuerySelector(".pB__image")?.QuerySelector("img")?.GetAttribute("src") ?? "czemu nie dziala";


            //DownloadImage(productName, @"https://www.okazje.info.pl/images/offer/m/8495/298798495-rower-trekkingowy-delta-m-28-touring-nex-7s.jpg");
            //using (WebClient webClient = new WebClient())
            //{
            //    string folderPath = @"C:\Users\HP\source\repos\P-zpp\WyszukiwarkaOI\temp\";
            //    string path = $"{folderPath}{productName}.jpg";
            //    webClient.Headers.Add("User-Agent: Other");
            //    webClient.DownloadFile(new Uri(imageUrl), @"C:\Users\HP\source\repos\P-zpp\WyszukiwarkaOI\temp\" + productName + ".jpg");
            //    //await webClient.DownloadFileTaskAsync(new Uri(url), path);
            //}


            T elementInfo = ctor(shopLink, productName, price, shopName, productDescription);

			result.Add(elementInfo);

		}

		return (result, true);
	}
	//public static async Task DownloadImage(string name, string url)
	//{
	//	using (WebClient webClient = new WebClient())
	//	{
	//		string folderPath = @"C:\Users\HP\source\repos\P-zpp\WyszukiwarkaOI\temp\";
	//		string path = $"{folderPath}{name}.jpg";
	//		webClient.Headers.Add("User-Agent: Other");
	//		webClient.DownloadFileAsync(new Uri(url), @"C:\Users\HP\source\repos\P-zpp\WyszukiwarkaOI\temp\" + name + ".jpg");
	//		//await webClient.DownloadFileTaskAsync(new Uri(url), path);
	//	}
	//}

	public async Task<(int? nextPageNumber, bool isSuccess)> GetNextPageNumberAsync(string html)
	{
		using IBrowsingContext context = BrowsingContext.New(Configuration.Default);

		IDocument document = await context.OpenAsync(req => req.Content(html));
		var anchorElements = document.QuerySelectorAll<IHtmlAnchorElement>(".pageArrow");
		var nextPageAnchor = anchorElements.FirstOrDefault(a => a.Title == "Następna");

		// to znaczy, że nie ma już następnej strony
		if (nextPageAnchor is null)
		{
			return (null, false);
		}

		string tmp = nextPageAnchor
			.Attributes.SingleOrDefault(a => a.Name == "data-page")?
			.TextContent ?? string.Empty;

		int nextPageNumber;

		if (!int.TryParse(tmp, out nextPageNumber))
		{
			return (null, false);
		}

		return (nextPageNumber, true);
	}
}
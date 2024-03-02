namespace WyszukiwarkaOI_webScraper;

public class WebScraper
{
	private static HttpClient _httpClient = new HttpClient();
    public WebScraper()
    {
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
}

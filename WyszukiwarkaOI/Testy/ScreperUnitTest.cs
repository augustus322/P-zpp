using WyszukiwarkaOI_webScraper;

namespace Testy
{
    public class ScraperUnitTest
    {
        WebScraper scraper = new();

        [Fact]
        public void ConnectionTest()
        {
            string url = "https://www.okazje.info.pl/search/?q=rower";
            var t1 = scraper.GetWebsiteHtmlAsync(url).Result.isSuccess;
            Assert.True(t1);
        }

        public void a()
        {

        }
    }
}
﻿using AngleSharp.Dom;
using WyszukiwarkaOI.Domain.Models;
using WyszukiwarkaOI_webScraper;

namespace Testy
{
    public class ScraperUnitTest
    {
        static HttpClient _httpClient = new();
        static WebScraper scraper = new(_httpClient);
        public string url = "https://www.okazje.info.pl/search/?q=rower";

        [Fact]
        public async void GetHtmlTest()
        {
            var t1 = scraper.GetWebsiteHtmlAsync(url).Result.isSuccess;

            Assert.True(t1);
        }

        [Fact]
        public async void GetChildrenTest()
        {
            //var html = @"<div class=""productsBox"">
	           //             <div class=""pB--cr""></div>
            //            </div>";
            var css = ".productsBox";
            var t2 = scraper.GetChildrenOfGivenElementAsync(css, testHtml).Result.isSuccess;

            Assert.True(t2);
        }

        [Fact]
        public async void GetElementsInfoTest()
        {
            Func<string, string, decimal, string, string?, Product> ctor = (p1, p2, p3, p4, p5) =>
            {
                return new Product(p1, p2, p3, p4, p5);
            };

            var css = ".productsBox";
            var t3 = scraper.GetChildrenOfGivenElementAsync(css, testHtml).Result.children;
            var t4 = scraper.GetElementsInfo(t3,ctor).isSuccess;

            Assert.True(t4);
        }


        public string testHtml = @"<div class=""productsBox""><div class=""pB--cr""><a class=""pB__href loadHref o_238698531"" data-r-hash=""redirect/?r=eyJ1cmwiOiJodHRwczpcL1wvd3d3Lm1lZGlhZXhwZXJ0LnBsXC9yb3dlcnlcL3Jvd2VyeS1taWVqc2tpZVwvcm93ZXItbWllanNraS1kYXdzdGFyLXJldHJvLXMxYi0yOC1jYWxpLWRhbXNraS1iaWFseS0xP3V0bV9zb3VyY2U9b2themplaW5mbyZ1dG1fbWVkaXVtPWNwYyZ1dG1fY29udGVudD00OTUxNjImdXRtX2NhbXBhaWduPTIwMjQtMDMmdXRtX3Rlcm09Um93ZXJ5LW1pZWpza2llIiwic2hvcCI6Ik1lZGlhIEV4cGVydCIsInNob3BfaWQiOjI1ODk2LCJvZmZlcl9pZCI6MjM4Njk4NTMxLCJyYXRlIjowLjMzfQ%3D%3D"" data-offer-id=""238698531"" href=""/redirect/?r=eyJ1cmwiOiJodHRwczpcL1wvd3d3Lm1lZGlhZXhwZXJ0LnBsXC9yb3dlcnlcL3Jvd2VyeS1taWVqc2tpZVwvcm93ZXItbWllanNraS1kYXdzdGFyLXJldHJvLXMxYi0yOC1jYWxpLWRhbXNraS1iaWFseS0xP3V0bV9zb3VyY2U9b2themplaW5mbyZ1dG1fbWVkaXVtPWNwYyZ1dG1fY29udGVudD00OTUxNjImdXRtX2NhbXBhaWduPTIwMjQtMDMmdXRtX3Rlcm09Um93ZXJ5LW1pZWpza2llIiwic2hvcCI6Ik1lZGlhIEV4cGVydCIsInNob3BfaWQiOjI1ODk2LCJvZmZlcl9pZCI6MjM4Njk4NTMxLCJyYXRlIjowLjMzfQ%3D%3D"" rel=""noopener"" target=""_blank""><div class=""pB__params--loveit"" data-offer-id=""238698531"" data-offer-img=""https://www.okazje.info.pl/images/offer/m/8531/238698531-rower-miejski-dawstar-retro-s1b-28-cali-damski-bialy-darmowy-tra.jpg"" data-offer-adult=""0""><span><img class=""add"" src=""/assets/images/love_add.svg"" alt=""ikona dodaj do ulubionych""><img class=""remove"" src=""/assets/images/love_remove.svg"" alt=""ikona usuń z ulubionych""></span></div><div class=""picWrapper""><picture class=""pB__image""><img src=""https://www.okazje.info.pl/images/offer/m/8531/238698531-rower-miejski-dawstar-retro-s1b-28-cali-damski-bialy-darmowy-tra.jpg"" class=""pB__image--src"" alt=""DAWSTAR Rower miejski Retro S1B 28 cali damski"" loading=""lazy""></picture><div class=""pB__params--info"" data-offer-id=""238698531""><img src=""/assets/images/info.svg"" alt=""ikona info"" width=""35"" height=""35"" loading=""lazy""></div></div><div class=""pB__bC""><div class=""otbLine""></div><div class=""pB__i--price pB__price""><span class=""priceRegular"">629,99&nbsp;zł</span></div><div class=""pB__i--shop"">Media Expert</div><div class=""pB__i--name""><div class=""pB__href--text"">DAWSTAR Rower miejski Retro S1B 28 cali damski Biały</div></div><div class=""pB__i--desc"">Każdy rower zakupiony w naszym sklepie jest fabrycznie zapakowany, czyli ma zdemontowaną kierownicę, siodło, pedały. Elementy te należy złożyć samodzielnie. W niektórych egzemplarzach może być koniecz...</div><div class=""pB__i--params""><p>Ean: 5901986498711</p><p>Producer: DAWSTAR</p></div><div class=""pB__i--avl""><b>Dostępny</b></div></div></a></div></div>";
    }
}
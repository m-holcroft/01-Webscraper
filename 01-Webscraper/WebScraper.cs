using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Webscraper.DataModels;

namespace Webscraper
{
    public class WebScraper
    {
        private static ScrapySharp.Network.ScrapingBrowser _scrapingBrowser = null;

        public WebScraper()
        {
            _scrapingBrowser = new ScrapySharp.Network.ScrapingBrowser();
        }

        private HtmlNode GetHTML(string url_)
        {
            ScrapySharp.Network.WebPage webPage = _scrapingBrowser.NavigateToPage(new Uri(url_));
            return webPage.Html;
        }

        private List<PageDetails> GetPageDetails(List<string> urlList_)
        {
            var pageDetailsList = new List<PageDetails>();
            foreach (var url in urlList_)
            {
                var htmlNode = GetHTML(url);
                var pageDetails = new PageDetails()
                {
                    Title = htmlNode.OwnerDocument.DocumentNode.SelectSingleNode("//html/head/title").InnerText,
                    Description = htmlNode.OwnerDocument.DocumentNode.SelectSingleNode("//html/body/section/section/section/section").InnerText,
                    URL = url
                };
                pageDetailsList.Add(pageDetails);
            }
            return pageDetailsList;
        }

        public List<string> GetPageLinks(string url_)
        {
            var homePageLinks = new List<string>();
            var html = GetHTML(url_);
            var links = html.CssSelect("a");

            foreach (var link in links)
            {
                if (link.Attributes["class"].Value.Contains("productLink"))
                {
                    string productURL = link.Attributes["href"].Value;
                    if (productURL.Contains("/challenge001"))
                        productURL = productURL.Replace("/challenge001", "");
                    StringBuilder sb = new StringBuilder();
                    sb.Append(url_);
                    sb.Append(productURL);
                    homePageLinks.Add(sb.ToString());
                }
            }

            return homePageLinks;
        }
    }
}
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webscraper.DataModels;

namespace Webscraper.Managers
{
    public class WebManager
    {
        /*
         IDEAS;

             */

        private static ScrapySharp.Network.ScrapingBrowser _scrapingBrowser = null;

        private async Task<HtmlNode> GetHTML(string url_)
        {
            var webPage = await _scrapingBrowser.NavigateToPageAsync(new Uri(url_));
            return webPage.Html;
        }

        public async Task<List<string>> GetPageLinks(string url_)
        {
            var homePageLinks = new List<string>();
            var html = await GetHTML(url_);
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

                    if (!homePageLinks.Contains(sb.ToString()))
                    {
                        homePageLinks.Add(sb.ToString());
                    }
                }
            }
            return homePageLinks;
        }

        private async Task<List<ProductPageDetails>> GetPageDetails(List<string> urlList_)
        {
            var pageDetailsList = new List<ProductPageDetails>();
            foreach (var url in urlList_)
            {
                var htmlNode = await GetHTML(url);
                var pageDetails = new ProductPageDetails()
                {
                    URL = url
                };

                var paragraphs = htmlNode.CssSelect("p");
                foreach (var p in paragraphs)
                {
                    if (p.Attributes["class"] != null && p.Attributes["class"].Value.Contains("Description"))
                    {
                        pageDetails.Title = p.InnerText.Trim();
                    }

                    var spans = p.CssSelect("span");
                    if (spans != null)
                    {
                        foreach (var span in spans)
                        {
                            switch (span.Attributes["class"].Value)
                            {
                                case "productUnitPrice":
                                    if (double.TryParse(span.InnerHtml.Trim(), out double unitPrice))
                                    {
                                        pageDetails.UnitPrice = unitPrice;
                                    }
                                    break;

                                case "productDescription2":
                                    pageDetails.Description = HtmlEntity.DeEntitize(span.InnerText);
                                    break;

                                case "productItemCode":
                                    pageDetails.Code = span.InnerHtml.Trim();
                                    break;
                            }
                        }
                    }
                }

                var tables = htmlNode.CssSelect("table");
                foreach (var t in tables)
                {
                    var tableCells = tables.CssSelect("td");
                    if (t != null && t.ChildNodes.Count > 0)
                    {
                        foreach (var td in tableCells)
                        {
                            if (td.Attributes["class"] != null)
                            {
                                var spans = td.CssSelect("span");
                                if (spans != null)
                                {
                                    foreach (var span in spans)
                                    {
                                        switch (span.Attributes["class"].Value)
                                        {
                                            case "productKcalPer100Grams":
                                                if (double.TryParse(span.InnerHtml.Trim(), out double kcal))
                                                {
                                                    pageDetails.Energy = kcal;
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                pageDetailsList.Add(pageDetails);
            }
            return pageDetailsList;
        }

        public WebManager()
        {
            _scrapingBrowser = new ScrapySharp.Network.ScrapingBrowser();
            _scrapingBrowser.Encoding = Encoding.UTF8;
        }

        public async Task<ProductCatalogueSummary> ScrapeSite(string url_)
        {
            var productCatalogueSummary = new ProductCatalogueSummary();
            var pageLinks = await GetPageLinks(url_);
            List<ProductPageDetails> pageDetails = null;

            if (pageLinks != null && pageLinks.Count > 0)
            {
                pageDetails = await GetPageDetails(pageLinks);
            }

            if (pageDetails != null)
            {
                double net = 0;
                double vat = 0;
                double gross = 0;

                foreach (var detail in pageDetails)
                {
                    productCatalogueSummary.ProductPageDetails.Add(detail);
                    net += detail.UnitPrice;
                    vat += (detail.UnitPrice * 0.2d);
                }
                gross += net + vat;
                productCatalogueSummary.PriceSummary.Net = Math.Round(net, 2);
                productCatalogueSummary.PriceSummary.VAT = Math.Round(vat, 2);
                productCatalogueSummary.PriceSummary.Gross = Math.Round(gross, 2);
            }

            return productCatalogueSummary;
        }
    }
}
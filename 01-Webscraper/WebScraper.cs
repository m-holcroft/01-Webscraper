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

        public List<ProductPageDetails> GetPageDetails(List<string> urlList_)
        {
            var pageDetailsList = new List<ProductPageDetails>();
            foreach (var url in urlList_)
            {
                var htmlNode = GetHTML(url);
                var pageDetails = new ProductPageDetails()
                {
                    Title = htmlNode.OwnerDocument.DocumentNode.SelectSingleNode("//html/head/title").InnerText,
                    URL = url
                };

                var paragraphs = htmlNode.CssSelect("p");

                foreach (var p in paragraphs)
                {
                    if (p.Attributes["class"] != null && p.Attributes["class"].Value.Contains("Description"))
                    {
                        pageDetails.Description = p.InnerText.Trim();
                    }
                }

                var tables = htmlNode.CssSelect("table");
                foreach (var t in tables)
                {
                    var tableCells = tables.CssSelect("td");
                    if (t != null && t.ChildNodes.Count > 0)
                    {
                        pageDetails.ProductNutritionalInformation = new ProductNutritionalInformation();
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
                                                    pageDetails.ProductNutritionalInformation.Energy = kcal;
                                                }
                                                break;

                                            case "productFatPer100Grams":
                                                if (double.TryParse(span.InnerHtml.Trim(), out double fat))
                                                {
                                                    pageDetails.ProductNutritionalInformation.Fat = fat;
                                                }
                                                break;

                                            case "productSaturatesPer100Grams":
                                                if (double.TryParse(span.InnerHtml.Trim(), out double saturates))
                                                {
                                                    pageDetails.ProductNutritionalInformation.Saturates = saturates;
                                                }
                                                break;

                                            case "productCarbsPer100Grams":
                                                if (double.TryParse(span.InnerHtml.Trim(), out double carbs))
                                                {
                                                    pageDetails.ProductNutritionalInformation.Carbohydrates = carbs;
                                                }
                                                break;

                                            case "productSugarsPer100Grams":
                                                if (double.TryParse(span.InnerHtml.Trim(), out double sugars))
                                                {
                                                    pageDetails.ProductNutritionalInformation.TotalSugars = sugars;
                                                }
                                                break;

                                            case "productFibrePer100Grams":
                                                if (double.TryParse(span.InnerHtml.Trim(), out double fibre))
                                                {
                                                    pageDetails.ProductNutritionalInformation.Fibre = fibre;
                                                }
                                                break;

                                            case "productProteinPer100Grams":
                                                if (double.TryParse(span.InnerHtml.Trim(), out double protein))
                                                {
                                                    pageDetails.ProductNutritionalInformation.Protein = protein;
                                                }
                                                break;

                                            case "productSaltPer100Grams":
                                                if (double.TryParse(span.InnerHtml.Trim(), out double salt))
                                                {
                                                    pageDetails.ProductNutritionalInformation.Salt = salt;
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

        private HtmlNode GetHTML(string url_)
        {
            ScrapySharp.Network.WebPage webPage = _scrapingBrowser.NavigateToPage(new Uri(url_));
            return webPage.Html;
        }
    }
}
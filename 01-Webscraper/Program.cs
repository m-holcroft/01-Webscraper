using System;
using System.Collections.Generic;
using System.Text;

/* http://devtools.truecommerce.net:8080/challenge001 */

namespace Webscraper
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            WebScraper scraper = new WebScraper();

            var links = scraper.GetPageLinks(Properties.AppSettings.Default.KibbleStoresURL);
            foreach (var item in links)
            {
                Console.WriteLine(item);
            }
            Console.Read();
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Webscraper.Extensions.JSON;
using Webscraper.Managers;

/* http://devtools.truecommerce.net:8080/challenge001 */

namespace Webscraper
{
    internal static class Program
    {

        static void Main (string[] args)
        {
            MainAsync(args).Wait();
        }

        private static async Task MainAsync(string[] args)
        {
            WebManager scraper = new WebManager();

            var productCatalogueSummary = await scraper.ScrapeSite(Properties.AppSettings.Default.KibbleStoresURL);

            foreach (var details in productCatalogueSummary.ProductPageDetails)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("Title: " + details.Title);
                Console.WriteLine("\tCode:" + details.Code);
                Console.WriteLine("\tEnergy: " + string.Format("{0:0}kcal per 100g", details.Energy));
                Console.WriteLine("\tUnit Price: " + string.Format("£{0:0.00}/unit", details.UnitPrice));
                Console.WriteLine("\tDescription: " + details.Description);
                Console.WriteLine("----------------------------------");    
            }

            Console.WriteLine("--------- Total ----------");
            Console.WriteLine(string.Format("Net: £{0:0.00}", productCatalogueSummary.PriceSummary.Net));
            Console.WriteLine(string.Format("VAT: £{0:0.00}", productCatalogueSummary.PriceSummary.VAT));
            Console.WriteLine(string.Format("Gross: £{0:0.00}", productCatalogueSummary.PriceSummary.Gross));

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new DecimalFormatConverter());
            settings.NullValueHandling = NullValueHandling.Ignore;

            string json = JsonConvert.SerializeObject(productCatalogueSummary, Formatting.Indented, settings);

            if (File.Exists(@".\Output\Output.txt"))
                File.Delete(@".\Output\Output.txt");

            File.WriteAllText(@".\Output\Output.txt", json);

            Console.Read();
        }
    }
}
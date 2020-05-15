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
            foreach (var details in scraper.GetPageDetails(links))
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine("Title: " + details.Title);
                Console.WriteLine("Description: " + details.Description);
                Console.WriteLine("URL: " + details.URL);
                Console.WriteLine("\tEnergy: " + details.ProductNutritionalInformation.Energy.ToString() + "kcal");
                Console.WriteLine("\tFat: " + details.ProductNutritionalInformation.Fat.ToString() + "g");
                Console.WriteLine("\tSaturates: " + details.ProductNutritionalInformation.Saturates.ToString() + "g");
                Console.WriteLine("\tCarbohydrate: " + details.ProductNutritionalInformation.Carbohydrates.ToString() + "g");
                Console.WriteLine("\tTotal Sugars: " + details.ProductNutritionalInformation.TotalSugars.ToString() + "g");
                Console.WriteLine("\tFibre: " + details.ProductNutritionalInformation.Fibre.ToString() + "g");
                Console.WriteLine("\tProtein: " + details.ProductNutritionalInformation.Protein.ToString() + "g");
                Console.WriteLine("\tSalt: " + details.ProductNutritionalInformation.Salt.ToString() + "g");
                Console.WriteLine("----------------------------------");
            }
            Console.Read();
        }
    }
}
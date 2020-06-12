using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webscraper.DataModels
{
    public class ProductCatalogueSummary
    {
        public ProductCatalogueSummary()
        { 
            ProductPageDetails = new List<ProductPageDetails>();
            PriceSummary = new PriceSummary();
        }
        public List<ProductPageDetails> ProductPageDetails { get; set; }
        public PriceSummary PriceSummary { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webscraper.DataModels
{
    public class ProductNutritionalInformation
    {
        public double Energy { get; set; }
        public double Fat { get; set; }
        public double Saturates { get; set; }
        public double Carbohydrates { get; set; }
        public double TotalSugars { get; set; }
        public double Fibre { get; set; }
        public double Protein { get; set; }
        public double Salt { get; set; }
    }
}
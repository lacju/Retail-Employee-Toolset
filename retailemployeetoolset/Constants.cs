using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retailemployeetoolset
{
    public static class Constants
    {
        //Azure Constants
        public static string WebApp = @"https://retailemployeetoolset.azurewebsites.net";

        //Webscraper Constants
        //Retrieve Product MFG 
        public static string ManufacturerFirstString = "manufacturer\" content=\"";
        public static string ManufacturerSecondString = "\"";
        //Retrieve Product Description
        public static string NameFirstString = "twitter:title\" content=\"";
        public static string NameSecondString = "\"";
        //Retrieve Product Price
        public static string PriceFirstString = "itemprop=\"price\" content=\"";
        public static string PriceSecondString = "\"";
        //Retrieve Product Description
        public static string DescriptionFirstString = "twitter:description\" content=\"";
        public static string DescriptionSecondString = "\"";
        //Retrieve Product HDD Size
        public static string HDDSizeFirstString = "Hard drive size</h2></div><div class=\"grid-unit colspan-5 \"><p>";
        public static string HDDSizeSecondString = "</p>";
        //Retrieve Product Display Info
        public static string DisplayFirstString = "Display</h2></div><div class=\"grid-unit colspan-5 \"><p>";
        public static string DisplaySecondString = "</p>";
        //Retrieve Product Processor
        public static string ProcessorFirstString = "Processor</h2></div><div class=\"grid-unit colspan-5 \"><p>";
        public static string ProcessorSecondString = "</p>";
        //Retrieve Product RAM
        public static string RAMFirstString = "Memory</h2></div><div class=\"grid-unit colspan-5 \"><p>";
        public static string RAMSecondString = "</p>";
        //Retrieve Product Weight
        public static string WeightFirstString = "Weight</h2></div><div class=\"grid-unit colspan-5 \"><p>";
        public static string WeightSecondString = "</p>";
        //Retrieve Product GPU
        public static string GPUFirstString = "Video</h2></div><div class=\"grid-unit colspan-5 \"><p>";
        public static string GPUSecondString = "</p>";
        //Retrieve Product Image URL
        public static string ImageFirstString = "twitter:image\" content=\"";
        public static string ImageSecondString = "\"";
        //Search URL
        public static string SearchURLFirstString = "<a href=\"/store/msusa/en_US/pdp/";
        public static string SearchURLSecondString = "\" pid";
    }
}

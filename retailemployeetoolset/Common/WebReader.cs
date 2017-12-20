using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.UI.Popups;
using HttpClient = System.Net.Http.HttpClient;


namespace retailemployeetoolset
{
    public class WebReader
    {
        private string completedSearchURL;
        private string searchPageOutput;
        private string completedProductPageURL;
        private string productPageOutput;
        private string currentLine;

        public double GetRandomNumber(int start, int end)
        {
            Random randomPrice = new Random();
            return randomPrice.Next(start, end);
        }

        public async Task<string> QueryProductPage(string sku)
        {
            System.Net.Http.HttpClient httpClient = new HttpClient();
            Uri searchURL = new Uri(("https://www.microsoftstore.com/store?keywords=" + sku + "&SiteID=msusa&Locale=en_US&Action=DisplayProductSearchResultsPage&result=&sortby=score%20descending&filters="));
            completedSearchURL = searchURL.ToString();
            searchPageOutput = await httpClient.GetStringAsync(searchURL);

            int stringPos1 = searchPageOutput.IndexOf(Constants.SearchURLFirstString) + Constants.SearchURLFirstString.Length;
            int stringPos2 = searchPageOutput.IndexOf(Constants.SearchURLSecondString);

            if (stringPos1 > 0 && stringPos2 > 0)
            {
                Uri productPageUrl = new Uri(("https://www.microsoftstore.com/store/msusa/en_US/pdp/" + searchPageOutput.Substring(stringPos1, stringPos2 - stringPos1)));
                completedProductPageURL = productPageUrl.ToString();
                productPageOutput = await httpClient.GetStringAsync(productPageUrl);

                return productPageOutput;
            }
            else
            {
                return "no product page results";
            }
        }

        public string RetrieveProductHDDSize(string productpage)
        {
            string productPage = productpage;
            if (productPage != "")
            {
                int newStringStartIndex = productPage.IndexOf(Constants.HDDSizeFirstString) + Constants.HDDSizeFirstString.Length;
                string interimString = productPage.Substring(newStringStartIndex, productPage.Length - newStringStartIndex);
                int secondStringIndex = interimString.IndexOf(Constants.HDDSizeSecondString);
                if (secondStringIndex > 0 && newStringStartIndex > 0)
                {
                    if (interimString.Substring(0, secondStringIndex).Length > 50)
                    {
                        return "";
                    }
                    else
                    {
                        return interimString.Substring(0, secondStringIndex);
                    }
                }

            }
            return "";
        }

        public string GetContentBetweenStrings(string targetString, string betweenString1, string betweenString2)
        {
            if (targetString != "")
            {
                int newStringStartIndex = targetString.IndexOf(betweenString1) + betweenString1.Length;
                string interimString = targetString.Substring(newStringStartIndex, targetString.Length - newStringStartIndex);
                int secondStringIndex = interimString.IndexOf(betweenString2);
                currentLine = interimString;
                if (secondStringIndex > 0 && newStringStartIndex > 0)
                {
                    return interimString.Substring(0, secondStringIndex);
                }
            }
            return "";
        }

        public string RetrieveProductWeight(string productpage)
        {
            if (productpage != "")
            {
                int newStringStartIndex = productpage.IndexOf(Constants.WeightFirstString) + Constants.WeightSecondString.Length;
                string interimString = productpage.Substring(newStringStartIndex, productpage.Length - newStringStartIndex);
                int secondStringIndex = interimString.IndexOf(Constants.WeightSecondString);
                if (secondStringIndex > 0 && newStringStartIndex > 0)
                {
                    if (interimString.Substring(0, secondStringIndex).Length > 50)
                    {
                        return "";
                    }
                    else
                    {
                        return interimString.Substring(0, secondStringIndex);
                    }
                }
            }
            return "";
        }
        public string RetrieveProductGPU(string productpage)
        {
            string productPage = productpage;
            if (productPage != "")
            {
                int newStringStartIndex = productPage.IndexOf(Constants.GPUFirstString) + Constants.GPUFirstString.Length;
                string interimString = productPage.Substring(newStringStartIndex, productPage.Length - newStringStartIndex);
                int secondStringIndex = interimString.IndexOf(Constants.GPUSecondString);
                if (secondStringIndex > 0 && newStringStartIndex > 0)
                {
                    if (interimString.Substring(0, secondStringIndex).Length > 100)
                    {
                        return "";
                    }
                    else
                    {
                        return interimString.Substring(0, secondStringIndex);
                    }
                }
            }
            return "";
        }
        public string RetrieveProductProcessor(string productpage)
        {
            string productPage = productpage;
            if (productPage != "")
            {
                int newStringStartIndex = productPage.IndexOf(Constants.ProcessorFirstString) + Constants.ProcessorFirstString.Length;
                string interimString = productPage.Substring(newStringStartIndex, productPage.Length - newStringStartIndex);
                int secondStringIndex = interimString.IndexOf(Constants.ProcessorSecondString);
                if (secondStringIndex > 0 && newStringStartIndex > 0)
                {
                    if (interimString.Substring(0, secondStringIndex).Length > 100)
                    {
                        return "";
                    }
                    else
                    {
                        return interimString.Substring(0, secondStringIndex);
                    }
                }
            }
            return "";
        }
        public string RetrieveProductDisplayInfo(string productpage)
        {
            string productPage = productpage;
            if (productPage != "")
            {
                int newStringStartIndex = productPage.IndexOf(Constants.DisplayFirstString) + Constants.DisplayFirstString.Length;
                string interimString = productPage.Substring(newStringStartIndex, productPage.Length - newStringStartIndex);
                int secondStringIndex = interimString.IndexOf(Constants.DisplaySecondString);
                if (secondStringIndex > 0 && newStringStartIndex > 0)
                {
                    if (interimString.Substring(0, secondStringIndex).Length > 100)
                    {
                        return "";
                    }
                    else
                    {
                        return interimString.Substring(0, secondStringIndex);
                    }
                }
            }
            return "";
        }
        public string RetrieveProductRAM(string productpage)
        {
            string productPage = productpage;
            if (productPage != "")
            {
                int newStringStartIndex = productPage.IndexOf(Constants.RAMFirstString) + Constants.RAMFirstString.Length;
                string interimString = productPage.Substring(newStringStartIndex, productPage.Length - newStringStartIndex);
                int secondStringIndex = interimString.IndexOf(Constants.RAMSecondString);
                if (secondStringIndex > 0 && newStringStartIndex > 0)
                {
                    if (interimString.Substring(0, secondStringIndex).Length > 100)
                    {
                        return "";
                    }
                    else
                    {
                        return interimString.Substring(0, secondStringIndex);
                    }
                }
            }
            return "";
        }
        public string RetrieveProductName(string productpage)
        {
            string productPage = productpage;
            if (productPage != "")
            {
                int newStringStartIndex = productPage.IndexOf(Constants.NameFirstString) + Constants.NameFirstString.Length;
                string interimString = productPage.Substring(newStringStartIndex, productPage.Length - newStringStartIndex);
                int secondStringIndex = interimString.IndexOf(Constants.NameSecondString);
                if (secondStringIndex > 0 && newStringStartIndex > 0)
                {
                    return interimString.Substring(0, secondStringIndex);
                }
            }
            return "";
        }
        public string RetrieveProductMFG(string productpage)
        {
            string productPage = productpage;
            if (productPage != "")
            {
                int newStringStartIndex = productPage.IndexOf(Constants.ManufacturerFirstString) + Constants.ManufacturerFirstString.Length;
                string interimString = productPage.Substring(newStringStartIndex, productPage.Length - newStringStartIndex);
                int secondStringIndex = interimString.IndexOf(Constants.ManufacturerSecondString);
                if (secondStringIndex > 0 && newStringStartIndex > 0)
                {
                    return interimString.Substring(0, secondStringIndex);
                }
            }
            return "";
        }
        public string RetrieveProductPrice(string productpage)
        {
            string productPage = productpage;
            if (productPage != "")
            {
                int newStringStartIndex = productPage.IndexOf(Constants.PriceFirstString) + Constants.PriceFirstString.Length;
                string interimString = productPage.Substring(newStringStartIndex, productPage.Length - newStringStartIndex);
                int secondStringIndex = interimString.IndexOf(Constants.PriceSecondString);
                if (secondStringIndex > 0 && newStringStartIndex > 0)
                {
                    return interimString.Substring(0, secondStringIndex);
                }
            }
            return "";
        }
        public string RetrieveProductDescription(string productpage)
        {
            string productPage = productpage;
            if (productPage != "")
            {
                int newStringStartIndex = productPage.IndexOf(Constants.DescriptionFirstString) + Constants.DescriptionFirstString.Length;
                string interimString = productPage.Substring(newStringStartIndex, productPage.Length - newStringStartIndex);
                int secondStringIndex = interimString.IndexOf(Constants.DescriptionSecondString);
                if (interimString.Substring(0, secondStringIndex).Length > 25)
                {
                    return interimString.Substring(0, secondStringIndex);
                }
                else
                {
                    return "";
                }
            }

            return "";
        }
        public string RetrieveProductImageURL(string productpage)
        {
            string productPage = productpage;
            if (productPage != "")
            {

                int newStringStartIndex = productPage.IndexOf(Constants.ImageFirstString) + Constants.ImageFirstString.Length;
                string interimString = productPage.Substring(newStringStartIndex, productPage.Length - newStringStartIndex);
                int secondStringIndex = interimString.IndexOf(Constants.ImageSecondString);
                return interimString.Substring(0, secondStringIndex);
            }
            return "";
        }
    }
}

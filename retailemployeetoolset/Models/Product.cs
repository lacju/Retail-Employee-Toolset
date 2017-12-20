using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using retailemployeetoolset.Common;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;


namespace retailemployeetoolset.Models
{

    public class Product
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        [JsonProperty(PropertyName = "Sku")]
        public string SKU { get; set; }

        [JsonProperty(PropertyName = "Manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get;  set; }

        [JsonProperty(PropertyName = "Category")]
        public string Category { get;  set; }

        [JsonProperty(PropertyName = "SubCategory")]
        public string SubCategory { get;  set; }

        [JsonProperty(PropertyName = "Price")]
        public double Price { get;  set; }

        [JsonProperty(PropertyName = "Weight")]
        public string Weight { get;  set; }

        [JsonProperty(PropertyName = "Processor")]
        public string Processor { get;  set; }

        [JsonProperty(PropertyName = "RAM")]
        public string RAM { get;  set; }

        [JsonProperty(PropertyName = "HDD")]
        public string HDDCap { get;  set; }

        [JsonProperty(PropertyName = "GPU")]
        public string GPU { get;  set; }

        [JsonProperty(PropertyName = "Display")]
        public string Display { get;  set; }

        [JsonProperty(PropertyName = "ImageUrl")]
        public string ImageUrl { get;  set; }

        


        double priceout;

        public Product()
        {

        }

        public Product(string sku, string mfg, string name, string desc, string category, string subcategory, string price, string hdd, string ram, string processor, string weight, string display, string gpu, string imageurl)
        {
            Manufacturer = mfg;
            SKU = sku;
            Name = name;
            Description = desc;
            Category = category;
            SubCategory = subcategory;
            double.TryParse(price, out priceout);
            Price = priceout;
            Weight = weight;
            Processor = processor;
            RAM = ram;
            HDDCap = hdd;
            GPU = gpu;
            Display = display;
            ImageUrl = imageurl;
            
        }

        public Product(string sku, string mfg, string name, string desc, string price, string hdd, string ram, string proc, string pweight, string pdisplay, string gpu, string imageurl)
        {
            WebReader wr = new WebReader();

            SKU = sku;
            Manufacturer = mfg;
            Description = desc;
            HDDCap = hdd;
            RAM = ram;
            Processor = proc;
            Weight = pweight;
            Display = pdisplay;
            GPU = gpu;
            double.TryParse(price, out priceout);
            Price = priceout;
            Name = name;
            Category = "";
            ImageUrl = imageurl;
            SubCategory = "";
            //    imageUrl = new Uri(imageurl, UriKind.RelativeOrAbsolute);


            if (Processor != "")
            {
                if (Name.Contains("Surface"))
                {
                    Category = "Surface";

                    if (Name.Contains("Pro"))
                    {
                        SubCategory = "Surface Pro";
                    }
                }
                else
                {
                    Category = "PC";

                    Processor = wr.GetContentBetweenStrings(Processor, "I", "z");
                    Processor = Processor.Insert(0, "I");
                    Processor = Processor.Insert(Processor.Length, "z");
                    if (GPU.Contains("AMD") || GPU.Contains("NVIDIA"))
                    {
                        if (!Name.Contains("Desktop"))
                        {
                            SubCategory = "Gaming Laptop";
                        }
                        else
                        {
                            SubCategory = "Gaming Desktop";
                        }

                    }
                    else
                    {
                        if (!Name.Contains("Desktop"))
                        {
                            SubCategory = "Laptop";
                        }
                        else
                        {
                            SubCategory = "Desktop";
                        }
                    }

                    Display = wr.GetContentBetweenStrings(Display, "1", ")");
                    Display = Display.Insert(0, "1");
                    Display = Display.Insert(Display.Length, ")");
                    if (SubCategory == "Desktop")
                    {
                        Display = "";
                    }
                }
            }
            if (Manufacturer == "MAROO" || Manufacturer == "KATE SPADE" || Manufacturer == "Jack Spade" || Manufacturer == "HEX" || Manufacturer == "Tumi" || Manufacturer == "UAG" || Manufacturer == "Brenthaven"
                || Manufacturer == "Maroo")
            {
                Category = "Case";

                if (Name.Contains("Pro"))
                {
                    SubCategory = "Surface Pro Case";
                }
                if (Name.Contains("Book"))
                {
                    SubCategory = "Surface Book Case";
                }
            }
            if (Name.Contains("Xbox") || Description.Contains("Xbox"))
            {
                Category = "Xbox";

                if (Price == 0)
                {
                    SubCategory = "Xbox Software";
                    Price = wr.GetRandomNumber(1, 5) * 9.99;
                }
                if (Price > 200)
                {
                    SubCategory = "Xbox Hardware";
                }
                else
                {
                    SubCategory = "Xbox Accessory";
                }
            }
            if (Name.Contains("Fitbit"))
            {
                Category = "Fitness";
                SubCategory = "Wearable";
            }
            if (Name.Contains("Roku"))
            {
                Category = "Misc";
                SubCategory = "Streaming Hardware";
            }
            if ((name.Contains("Consumer") || Name.Contains("Commercial")))
            {
                Category = "Warranty";
                if (Name.Contains("Complete"))
                {
                    SubCategory = "Microsoft Complete";
                }
                else
                {
                    SubCategory = "Performance Guard";
                }


            }
            else if (Category == "")
            {
                Category = "Accessory";

                if ((Name.Contains("Surface") || Description.Contains("Surface")))
                {
                    SubCategory = "Surface Accessory";
                }
                if (Name.Contains("Cable"))
                {
                    SubCategory = "Cable";
                }
                else if (Name.Contains("Mouse") || Name.Contains("Keyboard") || Name.Contains("Hard Drive") || Name.Contains("USB") || Name.Contains("Bluetooth") || Name.Contains("Adpater") || Name.Contains("SSD") || Name.Contains("Memory") || Name.Contains("Headset") || Name.Contains("Monitor"));
                {
                    SubCategory = "PC Accessory";
                }

            }
            //if (ImageUrl != "")
            //{
            //    try
            //    {
            //        ImageURI = new Uri(ImageUrl);
            //    }
            //    catch (Exception)
            //    {

            //        return;
            //    }

            //}

        }
    }




}

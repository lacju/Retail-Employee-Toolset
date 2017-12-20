using Newtonsoft.Json;
using retailemployeetoolset;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using Newtonsoft.Json.Converters;
using retailemployeetoolset.Common;

namespace retailemployeetoolset.Models
{
    public class Solution
    {

        [JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        [JsonProperty("CustomerName")]
        public string CustomerName { get; set; }

        [JsonProperty("SolutionID")]
        public string SolutionID { get; set; }

        [JsonProperty("BusinessCX")]
        public bool BusinessCustomer { get; set; }

        [JsonProperty("BusinessName")]
        public string BusinessName { get; set; }

        [JsonProperty("SolutionProductsByteArray")]
        public byte[] SolutionProductsByteArray { get; set; }

        [JsonProperty("CustomerByteArray")]
        public byte[] CustomerByteArray { get; set; }

        private List<Product> _Products;
        private Customer _Customer;
        public  string Subtotal { get; set; }
        public  string Taxes { get; set; }
        public  string FinalTotal { get; set; }

        [JsonProperty("DateCreated")]
        public string DateCreated { get; set; }

        public bool ActiveSolution { get; set; }



        public void AddProductToSolution(Product product)
        {
           
            _Products.Add(product);
            //EncodeProductsToByteArray();
        }

        public List<Product> GetProductList()
        {
            return _Products;
        }
        public Customer GetCustomer()
        {
            return _Customer;
        }

        private void EncodeCustomerToByteArray()
        {
            CustomerByteArray = CryptographicBuffer.ConvertStringToBinary(JsonConvert.SerializeObject(_Customer), BinaryStringEncoding.Utf16BE).ToArray();
        }

        private void DecodeCustomerFromByteArray()
        {
            if (CustomerByteArray != null)
            {
                _Customer = JsonConvert.DeserializeObject<Customer>(CryptographicBuffer.ConvertBinaryToString(
                    BinaryStringEncoding.Utf16BE,
                    CustomerByteArray.AsBuffer()));
            }
        }

        private void EncodeProductsToByteArray()
        {
            
            string productJsonString = "";
            //for (int i = 0; i < _Products.Count; i++)
            //{
            //    productJsonStringArray[i] = JsonConvert.SerializeObject(_Products[i]);
            //}


          //  string jsonArrayToString = String.Join(",", productJsonStringArray);

            SolutionProductsByteArray = CryptographicBuffer.ConvertStringToBinary(JsonConvert.SerializeObject(_Products), BinaryStringEncoding.Utf16BE).ToArray();

            
        }

        private void DecodeProductsFromByteArray()
        {
            if (SolutionProductsByteArray != null)
            {


                _Products = new List<Product>();
                _Products = JsonConvert.DeserializeObject<List<Product>>(CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf16BE,
                        SolutionProductsByteArray.AsBuffer()));
                //string jsonArrayToString =
                //    CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf16BE,
                //        SolutionProductsByteArray.AsBuffer());
                //string[] productJsonStringArray = jsonArrayToString.Split(',');

                //foreach (string product in productJsonStringArray.ToList())
                //{
                //    _Products.Add(JsonConvert.DeserializeObject<Product>(product));
                //}
            }
        }

        public void SetSolutionCustomer(Customer customerToSet)
        {
            _Customer = customerToSet;
        }
        public void PrepareSolutionForSuspension()
        {
            CustomerName = _Customer.CustomerFirstName + " " + _Customer.CustomerLastName;
            EncodeCustomerToByteArray();
            EncodeProductsToByteArray();
        }

        public void RebuildSolutionAfterRecall()
        {
            DecodeCustomerFromByteArray();
            DecodeProductsFromByteArray();
            CustomerName = _Customer.CustomerFirstName + " " + _Customer.CustomerLastName;
        }


        public Solution()
        {

        }

        public Solution(Customer customer)
        {
            _Customer = customer;
            BusinessCustomer = customer.BusinessCustomer;
            EncodeCustomerToByteArray();
        }

        public Solution(bool businessCustomer)
        {
            _Products = new List<Product>();

            BusinessCustomer = businessCustomer;
            SolutionID = Guid.NewGuid().ToString();

            DateTime dt = new DateTime();
            dt = DateTime.Today;
            DateCreated = dt.ToString();
        }
        
       
    }
}

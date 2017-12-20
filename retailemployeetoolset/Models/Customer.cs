using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retailemployeetoolset.Models
{
    public class Customer : INotifyPropertyChanged
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        [JsonProperty("CustomerFirstName")]
        public string CustomerFirstName { get; set; }

        [JsonProperty("CustomerLastName")]
        public string CustomerLastName { get; set; }

        [JsonProperty("CustomerEmail")]
        public string CustomerEmail { get; set; }

        [JsonProperty("BusinessCustomer")]
        public bool BusinessCustomer { get; set; }

        [JsonProperty("OrganizationName")]
        public string OrganizationName { get; set; }

        [JsonProperty("CustomerPhoneNumber")]
        public string CustomerPhoneNumber { get; set; }

        public bool Anonymous { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Customer()
        {

        }

        public Customer(bool anonymous)
        {
            CustomerFirstName = "Anonymous";
            CustomerLastName = string.Empty;
            CustomerEmail = string.Empty;
            BusinessCustomer = false;
            OrganizationName = string.Empty;
            CustomerPhoneNumber = string.Empty;
        }

        public Customer(string firstName, string lastName, string emailAddress, bool businessCustomer, string phoneNum,
            string businessName = "")
        {
            CustomerFirstName = firstName;
            CustomerLastName = lastName;
            CustomerEmail = emailAddress;
            BusinessCustomer = businessCustomer;
            CustomerPhoneNumber = phoneNum;
            OrganizationName = businessName;
        }

        public Customer(string firstName)
        {
            CustomerFirstName = firstName;
        }

        public Customer(bool businessCustomer, bool anonymous)
        {
            BusinessCustomer = businessCustomer;

            if (anonymous)
            {
                CustomerFirstName = string.Empty;
                CustomerLastName = string.Empty;
                CustomerEmail = string.Empty;
                BusinessCustomer = businessCustomer;
                OrganizationName = string.Empty;
                CustomerPhoneNumber = string.Empty;
            }
        }
    }
}

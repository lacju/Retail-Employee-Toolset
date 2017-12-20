using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;

namespace retailemployeetoolset.Models
{
    public enum AppointmentType
    {
        [Display(Name = "Answer Desk")]
        AnswerDesk,
        [Display(Name = "Personal Training")]
        PersonalTraining,
        [Display(Name = "Discover More")]
        DiscoverMore
    }

    public enum AppointmentTopic
    {
        [Display(Name = "Malware Removal")]
        MalwareRemoval,
        [Display(Name = "Data Transfer")]
        DataTransfer,
        [Display(Name = "Xbox")]
        Xbox,
        [Display(Name = "Office")]
        Office,
        [Display(Name = "Tuneup")]
        TuneUp,
        [Display(Name = "Windows")]
        Windows,
        [Display(Name = "Band")]
        Band,
        [Display(Name = "Surface")]
        Surface,
        [Display(Name = "Pickup")]
        Pickup,
        [Display(Name = "One Drive")]
        OneDrive
    }
    public class Appointment
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        [JsonProperty("AppointmentType")]
        public string AppointmentType { get; set; }

        [JsonProperty("AppointmentTopic")]
        public string AppointmentTopic { get; set; }

        [JsonProperty("AppointmentDescription")]
        public string AppointmentDescription { get; set; }

        [JsonProperty("CustomerName")]
        public string CustomerName { get; set; }

        [JsonProperty("CustomerByteArray")]
        public byte[] CustomerByteArray { get; set; }

        [JsonProperty("AppointmentScheduledTime")]
        public DateTime AppointmentScheduledTime { get; set; }

        [JsonProperty("AppointmentCheckedinTime")]
        public DateTime AppointmentCheckedinTime { get; set; }

        [JsonProperty("AppointmentFinishTime")]
        public DateTime AppointmentFinishTime { get; set; }

        [JsonProperty("AppointmentCompletedBy")]
        public string AppointmentCompletedBy { get; set; }

        [JsonProperty("AppointmentCreatedBy")]
        public string AppointmentCreatedBy { get; set; }

        [JsonProperty("AppointmentCreateTime")]
        public DateTime AppointmentCreateTime { get; set; }

        [JsonProperty("AppointmentCheckedinBy")]
        public string AppointmentCheckedinBy { get; set; }

        [JsonProperty("AppointmentStatus")]
        public string AppointmentStatus { get; set; }


        private Customer _customer;

        public Appointment()
        {
            DeserializeCustomer();

        }
        public Appointment(Customer cxForApt, AppointmentType aptType, AppointmentTopic aptTopic, string aptDesc, DateTime aptScheduledTime, string createdBy)
        {
            _customer = cxForApt;
            SetCustomerName();
            SerializeCustomer();

            switch (aptType)
            {
                case Models.AppointmentType.AnswerDesk:
                    AppointmentType = "Answer Desk";
                    break;
                case Models.AppointmentType.PersonalTraining:
                    AppointmentType = "Personal Training";
                    break;
                case Models.AppointmentType.DiscoverMore:
                    AppointmentType = "Personal Shopping";
                    break;
            }

            switch (aptTopic)
            {
                case Models.AppointmentTopic.MalwareRemoval:
                    AppointmentTopic = "Malware Removal";
                    break;
                case Models.AppointmentTopic.DataTransfer:
                    AppointmentTopic = "Data Transfer";
                    break;
                case Models.AppointmentTopic.Xbox:
                    AppointmentTopic = "Xbox";
                    break;
                case Models.AppointmentTopic.Office:
                    AppointmentTopic = "Office";
                    break;
                case Models.AppointmentTopic.TuneUp:
                    AppointmentTopic = "Tune Up";
                    break;
                case Models.AppointmentTopic.Windows:
                    AppointmentTopic = "Windows";
                    break;
                case Models.AppointmentTopic.Band:
                    AppointmentTopic = "Band";
                    break;
                case Models.AppointmentTopic.Surface:
                    AppointmentTopic = "Surface";
                    break;
                case Models.AppointmentTopic.Pickup:
                    AppointmentTopic = "Pickup Device";
                    break;
                case Models.AppointmentTopic.OneDrive:
                    AppointmentTopic = "OneDrive";
                    break;
                default:
                    break;
            }
            AppointmentDescription = aptDesc;
            AppointmentScheduledTime = aptScheduledTime;
            AppointmentCreatedBy = createdBy;
            AppointmentCreateTime = DateTime.Now;
            AppointmentStatus = "Scheduled";

        }

        public void CheckInAppointment(string checkedInBy)
        {
            AppointmentCheckedinBy = checkedInBy;
            AppointmentCheckedinTime = DateTime.Now;
            AppointmentStatus = "Checked In";
        }
        public void CompleteAppointment(string completedBy)
        {
            AppointmentCompletedBy = completedBy;
            AppointmentFinishTime = DateTime.Now;
            AppointmentStatus = "Completed";
        }

        public void CancelAppointment(string completedBy)
        {
            AppointmentCompletedBy = completedBy;
            AppointmentFinishTime = DateTime.Now;
            AppointmentStatus = "Cancelled";
        }
        private void PrepareToUploadToDatabase()
        {
            SerializeCustomer();
            SetCustomerName();

        }
        private void SerializeCustomer()
        {
            CustomerByteArray = CryptographicBuffer.ConvertStringToBinary(JsonConvert.SerializeObject(_customer), BinaryStringEncoding.Utf16BE).ToArray();
        }

        private void DeserializeCustomer()
        {
            if (CustomerByteArray != null)
            {
                _customer = JsonConvert.DeserializeObject<Customer>(CryptographicBuffer.ConvertBinaryToString(
                    BinaryStringEncoding.Utf16BE,
                    CustomerByteArray.AsBuffer()));
            }
        }
        public Customer GetCustomer()
        {
            return _customer;
        }
        public void SetCustomer(Customer cxToSet)
        {
            _customer = cxToSet;
            SetCustomerName();
            SerializeCustomer();
        }
        private void SetCustomerName()
        {
            CustomerName = _customer.CustomerFirstName + " " + _customer.CustomerLastName;
        }
    }
}

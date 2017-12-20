using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Newtonsoft.Json;

namespace retailemployeetoolset.Models
{
    public class QueueCustomer : Customer
    {
        [JsonProperty("CustomerDescription")]
        public string CustomerDescription { get; set; }

        [JsonProperty("ProductInterestedIn")]
        public string ProductInterestedIn { get; set; }

        [JsonProperty("CustomerCheckinTime")]
        public DateTime CustomerCheckinTime { get; set; }

        [JsonProperty("CustomerFinishedWithTime")]
        public DateTime CustomerClaimedTime { get; set; }

        [JsonProperty("TimeWaited")]
        public string TimeWaited { get; set; }

        [JsonProperty("CustomerHelped")]
        public bool CustomerHelped { get; set; }

        [JsonProperty("HelpedByUserName")]
        public string HelpedByUserName { get; set; }

        [JsonProperty("AddedByUserName")]
        public string AddedByUserName { get; set; }

        private Stopwatch _stopwatch = new Stopwatch();
        private ThreadPoolTimer _timer;
        private DateTime _now;

        private Visibility _claimButtonVisibility;
        public Visibility ClaimButtonVisibility
        {
            get { return _claimButtonVisibility; }
            set { _claimButtonVisibility = value; NotifyPropertyChanged("ClaimButtonVisibility"); }
        }

        private Visibility _helpedByText;
        public Visibility HelpedByText
        {
            get { return _helpedByText; }
            set { _helpedByText = value; NotifyPropertyChanged("HelpedByText"); }
        }

        private string _timeWaiting;
        public string TimeWaiting
        {
            get { return _timeWaiting; }
            set { _timeWaiting = value; NotifyPropertyChanged("TimeWaiting"); }
        }

        private string _helpedBy;
        [JsonIgnore]
        public string HelpedBy
        {
            get { return _helpedBy; }
            set { _helpedBy = value; NotifyPropertyChanged("HelpedBy"); }
        }

        public void ClaimCustomer(string userName)
        {
            CustomerClaimedTime = DateTime.Now;
            CustomerHelped = true;
            SetHelpedBy(userName);
            ToggleTimer();
        }

        public void ToggleVisibilities()
        {
            if (CustomerHelped)
            {
                ClaimButtonVisibility = Visibility.Collapsed;
                HelpedByText = Visibility.Visible;
                HelpedBy = HelpedByUserName;
            }
            else
            {
                ClaimButtonVisibility = Visibility.Visible;
                HelpedByText = Visibility.Collapsed;
            }
        }
        private void SetHelpedBy(string user)
        {
            HelpedByUserName = user;
            HelpedBy = HelpedByUserName;
            ToggleVisibilities();

        }
        public void ToggleTimer()
        {
            if (CustomerHelped)
            {
                StopTimer();
            }
            else
            {
                StartTimer();
            }
        }
        public QueueCustomer()
        {
            ToggleVisibilities();
            _now = new DateTime();
            ToggleTimer();
        }

        public void CheckInCustomer()
        {
            CustomerCheckinTime = DateTime.Now;
        }

        public QueueCustomer(string fName, string lName, string email, bool busCx, string phoneNum, string orgName, string cusDesc, string prodInterest)
        {
            ToggleVisibilities();
            _now = new DateTime();
            CustomerFirstName = fName;
            CustomerLastName = lName;
            CustomerEmail = email;
            BusinessCustomer = busCx;
            CustomerPhoneNumber = phoneNum;
            OrganizationName = orgName;
            CustomerDescription = cusDesc;
            ProductInterestedIn = prodInterest;
            ToggleTimer();
        }
        public void StartTimer()
        {
            TimeSpan oneSecondTimeSpane = new TimeSpan(0, 0, 1);
            _stopwatch.Start();
            _timer = ThreadPoolTimer.CreatePeriodicTimer(timerCallback, oneSecondTimeSpane);
        }

        public void StopTimer()
        {
            if (CustomerHelped)
            {
                _stopwatch.Stop();
                _timer.Cancel();
                CustomerClaimedTime = DateTime.Now;
                TimeSpan waitedTime = CustomerCheckinTime - CustomerClaimedTime;
                TimeWaited = waitedTime.ToString();
            }
        }

        public delegate void TimerElapsedHandler(ThreadPoolTimer timer);

        private async void timerCallback(object state)
        {
            // do some work not connected with UI
          

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
{
                    UpdateTimeElapsedProperty();
                });
        }

        public void UpdateTimeElapsedProperty() 
        {

            _now = DateTime.Now;
            TimeSpan timesinceCheckIn = _now - CustomerCheckinTime;
            TimeSpan timeToDisplay = new TimeSpan(_stopwatch.Elapsed.Hours + timesinceCheckIn.Hours, _stopwatch.Elapsed.Minutes + timesinceCheckIn.Minutes, _stopwatch.Elapsed.Seconds + timesinceCheckIn.Seconds);
            TimeWaiting = timeToDisplay.ToString();
        }

    }
}

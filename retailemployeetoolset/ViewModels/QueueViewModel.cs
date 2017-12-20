using retailemployeetoolset.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Toolkit.Uwp.UI;
using retailemployeetoolset.Common;

namespace retailemployeetoolset.ViewModels
{
    public class QueueViewModel : ViewModel
    {

        public string CurrentUser { get; set; }

        public ICommand HelpCustomer { get; set; }

        public QueueViewModel()
        {


            RefreshCustomerProperties();
            CurrentUser = "not implimented";
            HelpCustomer = new RelayCommand<QueueCustomer>(ClaimCustomer);
        }

        private async void ClaimCustomer(QueueCustomer queueCustomer)
        {
            if (!queueCustomer.CustomerHelped)
            {
                queueCustomer.ClaimCustomer("Dynamic Users Not Implimented");
                await AddQueueCustomer(queueCustomer);
;                if (SolutionHelper.CurrentSolution == null)
                {
                    SolutionHelper.CreateNewSolution(queueCustomer);
                }
            }
        }

        private void RefreshCustomerProperties() //Refreshes the data bindings for the gridview and makes sure the time is is ticking if it should be for each customer
        {
            QueueCustomers.ToList().ForEach(x => (x as QueueCustomer).ToggleTimer());
            QueueCustomers.ToList().ForEach(x => (x as QueueCustomer).ToggleVisibilities());
        }

        public async void AddNewCustomerToQueue(string fName, string lName, string email,
            bool busCx, string phoneNum, string orgName, string cusDesc, string prodInterest, string ID = "")
        {
                Customer newCustomer = new Customer(fName, lName, email, busCx, phoneNum, orgName);
            if (ID != "")
            {
                newCustomer.Id = ID;
            }
            await AddNewCustomer(newCustomer);
            QueueCustomer newQc = newCustomer as QueueCustomer;
            newQc.ProductInterestedIn = prodInterest;
            newQc.CustomerDescription = cusDesc;
            newQc.CheckInCustomer();
            await AddNewCustomer(newQc);
        }


        public async void CancelCustomer(QueueCustomer queueCustomer)
        {
            if (!queueCustomer.CustomerHelped)
            {
                queueCustomer.ClaimCustomer("Cancelled");
                await AddNewCustomer(queueCustomer);
            }
        }

    }
}

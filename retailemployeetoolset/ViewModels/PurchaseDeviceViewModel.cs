using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Microsoft.Toolkit.Uwp.UI;
using retailemployeetoolset.Common;
using retailemployeetoolset.Models;

namespace retailemployeetoolset.ViewModels
{
    public class PurchaseDeviceViewModel : ViewModel
    {
        public string CurrentCustomer
        {
            get => _currentCustomer;
            set
            {
                _currentCustomer = value;
                OnPropertyChanged();
                ;
            }
        }


        private string _currentCustomer;
        public Customer _CurrentCustomer;
        private string searchParam = "";

        private AdvancedCollectionView _customerAdvancedView;
        public AdvancedCollectionView CustomerAdvancedView
        {
            get => _customerAdvancedView;
            set
            {
                _customerAdvancedView = value;
                OnPropertyChanged();
            }
        }

        public PurchaseDeviceViewModel()
        {


            //Sets the current customer if there is an active solution
            if (_CurrentCustomer == null)
                if (SolutionHelper.CurrentSolutionCustomer != null)
                    SetCurrentCustomer(SolutionHelper.CurrentSolutionCustomer);

            CustomerAdvancedView = new AdvancedCollectionView(Customers);
            
        }

       

        private async void InitialDBLoad()
        {

        }
        //Creates a new solution with an anonymous customer
        public void ContinueAsAnonymous()
        {
            if (SolutionHelper.CurrentSolution == null)
            {
                SolutionHelper.CreateNewSolution(new Customer(true));
                SetCurrentCustomer(SolutionHelper.CurrentSolutionCustomer);
            }
            else
            {
                ExistingSolutionPrompt(new Customer(true));
            }
        }
        //Called when the user has selected an existing customer from the customer collection
        public async void SelectCustomer(Customer customer)
        {
            await ExistingSolutionPrompt(customer);
        }

        //Adds a new customer to the database or updates an existing one
        public async void AddCustomer(Customer customerToAdd)
        {
            await ExistingSolutionPrompt(customerToAdd);
            await AddNewCustomer(customerToAdd);
            await LoadCustomers();
        }

        //Applies filter to the customer collection
        public void FilterCustomerDatabase(string searchParameters)
        {
            searchParam = searchParameters;
            CustomerAdvancedView.Filter = null;
            CustomerAdvancedView.Filter = SearchFilter;
        }

        //Called when the user attempts to select device a category to shop from but does not currently have
        //an active customer/solution
        public async Task<bool?> CreateASolutionPrompt()
        {
            if (SolutionHelper.CurrentSolution == null)
            {
                var activeSolution =
                    new MessageDialog("You currently do not have an active solution, what would you like to do?");
                activeSolution.Commands.Add(new UICommand("Add a customer") { Id = 0 });
                activeSolution.Commands.Add(new UICommand("Continue anonymously") { Id = 1 });
                var result = await activeSolution.ShowAsync();
                if ((int)result.Id == 0)
                    return true;
                if ((int)result.Id == 1)
                    return false;
                return null;
            }
            return null;
        }

        //Determines if there is already an existing solution, if there is the user is prompted
        //to decide if they'd like to start a new solution, suspend the current solution, or do nothing
        private async Task ExistingSolutionPrompt(Customer customerTryingToadd)
        {
            if (SolutionHelper.CurrentSolution != null)
            {
                var activeSolution =
                    new MessageDialog("You currently have an active solution, what would you like to do with it?");
                activeSolution.Commands.Add(new UICommand("Start a new solution") {Id = 0});
                activeSolution.Commands.Add(new UICommand("Suspend the current solution") {Id = 1});
                activeSolution.Commands.Add(new UICommand("Do nothing") {Id = 2});
                var result = await activeSolution.ShowAsync();
                if ((int) result.Id == 0)
                {
                    SolutionHelper.CreateNewSolution(customerTryingToadd);
                    SetCurrentCustomer(SolutionHelper.CurrentSolutionCustomer);
                }
                if ((int) result.Id == 1)
                {
                    await SolutionHelper.SuspendCurrentSolution();
                    SolutionHelper.CreateNewSolution(customerTryingToadd);
                    SetCurrentCustomer(SolutionHelper.CurrentSolutionCustomer);
                }
                if ((int) result.Id == 1)
                    return;
            }
            else
            {
                SolutionHelper.CreateNewSolution(customerTryingToadd);
                SetCurrentCustomer(SolutionHelper.CurrentSolutionCustomer);
            }
        }

        //Sets the current customer property displaying the active customer in the UI
        private void SetCurrentCustomer(Customer curCustomer)
        {
            _CurrentCustomer = curCustomer;
            CurrentCustomer = _CurrentCustomer.CustomerFirstName + " " + _CurrentCustomer.CustomerLastName;
        }

        //Searches all customer properties for the provided search terms
        private bool SearchFilter(object item)
        {
            var c = item as Customer;
            var searchTerms = "";
            searchTerms = c.CustomerLastName + " " + c.CustomerFirstName + " " + c.CustomerEmail + " " +
                          c.CustomerPhoneNumber + " " + c.OrganizationName;
            return searchTerms.Contains(searchParam);
        }
    }
}
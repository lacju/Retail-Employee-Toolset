using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using retailemployeetoolset.Models;
using retailemployeetoolset.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI;
using Microsoft.WindowsAzure.MobileServices;

namespace retailemployeetoolset.ViewModels
{
    public class SolutionViewModel : ViewModel
    {

        public Solution CurrentSolution { get; set; }
        public ObservableCollection<Product> ProductsInSolution { get; set; }
        public Customer CurrentCustomer { get; set; }
        public string CurrentUser { get; set; }
        private string searchParam = "";

        private string _customerOrganization;
        public string CustomerOrganization
        {
            get { return _customerOrganization; }
            set { _customerOrganization = value; OnPropertyChanged(); }
        }
        private string _customerFullName;
        public string CustomerFullName
        {
            get { return _customerFullName; }
            set { _customerFullName = value; OnPropertyChanged(); }
        }
        private string _customerEmail;
        public string CustomerEmail
        {
            get { return _customerEmail; }
            set { _customerEmail = value; OnPropertyChanged(); }
        }

        private string _subtotal;
        public string Subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; OnPropertyChanged(); }
        }

        private string _tax;
        public string Tax
        {
            get { return _tax; }
            set { _tax = value; OnPropertyChanged(); }
        }

        private string _totalSaved;
        public string TotalSaved
        {
            get { return _totalSaved; }
            set { _totalSaved = value; OnPropertyChanged(); }
        }

        private string _total;
        public string Total
        {
            get { return _total; }
            set { _total = value; OnPropertyChanged(); }
        }

        public AdvancedCollectionView CustomerAdvancedView;

        public ICommand RemoveCommand { get; set; }
        public SolutionViewModel()
        {


            CurrentSolution = SolutionHelper.CurrentSolution;
            CurrentCustomer = SolutionHelper.CurrentSolutionCustomer;
            ProductsInSolution = new ObservableCollection<Product>();
            SetCustomerInfo();
            RemoveCommand = new RelayCommand<Product>(RemoveItem);
            if (CurrentSolution != null)
            {
                CurrentSolution.GetProductList().ForEach(x => ProductsInSolution.Add(x));
                SetPrices();
            }
            CustomerAdvancedView = new AdvancedCollectionView(Customers);
        }

        public async Task CreateNewSolution()
        {
            if (SolutionHelper.CurrentSolution == null)
            {
                MessageDialog newSolution = new MessageDialog("Would you like to start a new Solution?");
                newSolution.Commands.Add(new UICommand("Yes") { Id = 0 });
                newSolution.Commands.Add(new UICommand("No") { Id = 1 });
                var result = await newSolution.ShowAsync();
                if ((int)result.Id == 0)
                {
                    
                }
                else
                {
                    
                }
            }
        }

        private void SetPrices()
        {
            double subtotal = 0;
            double total = 0;
            double taxCharged = 0;
            foreach (var item in ProductsInSolution)
            {
                subtotal += item.Price;
            }
            Subtotal = subtotal.ToString("0.##");
            total = subtotal * 1.0625; //MA tax rate
            Total = total.ToString("0.##");
            taxCharged = subtotal * .0625;
            Tax = taxCharged.ToString("0.##");

        }

        private void SetCustomerInfo()
        {
            if (CurrentCustomer != null)
            {
                CustomerFullName = CurrentCustomer.CustomerFirstName + " " + CurrentCustomer.CustomerLastName;
                CustomerEmail = CurrentCustomer.CustomerEmail;
                CustomerOrganization = CurrentCustomer.OrganizationName;
            }
        }
        private async void RemoveItem(Product item)
        {
            MessageDialog removeWarning = new MessageDialog("Are you sure you want to remove this product from the solution?");
            removeWarning.Commands.Add(new UICommand("Yes") { Id = 0 });
            removeWarning.Commands.Add(new UICommand("No, leave it there.") { Id = 1 });
            var result = await removeWarning.ShowAsync();
            if ((int)result.Id == 0)
            {
                Product productToRemove = item as Product;
                ProductsInSolution.Remove(productToRemove);
                SetPrices();
            }
            else
            {
                return;
            }


            
        }

        public void FilterCustomerDatabase(string searchParameters) //filters the advancedcollectionview using the SearchFilter method
        {
            searchParam = searchParameters;
            CustomerAdvancedView.Filter = null;
            CustomerAdvancedView.Filter = SearchFilter;
        }

        private bool SearchFilter(object item) //Filters each object in the advancedcollectionview based on user input into a textbox
        {
            

            Customer c = item as Customer;
            string searchTerms = "";
            searchTerms = c.CustomerLastName + " " + c.CustomerFirstName + " " + c.CustomerEmail + " " +
                          c.CustomerPhoneNumber + " " + c.OrganizationName;
            if (searchTerms.Contains(searchParam))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddAnotherProduct()
        {
           
        }
        public async void ClearSolutionMessage()
        {
            MessageDialog clearSolution = new MessageDialog("Are you sure you want to clear this solution?");
            clearSolution.Commands.Add(new UICommand("Yes") { Id = 0 });
            clearSolution.Commands.Add(new UICommand("No") { Id = 1 });
            var result2 = await clearSolution.ShowAsync();
            if ((int)result2.Id == 0)
            {
                ClearSolution();
            }
            if ((int)result2.Id == 1)
            {
                return;
            }           
        }
        public void ClearSolution()
        {
            SolutionHelper.ClearSolution();
            CurrentSolution = null;
            CurrentCustomer = null;
            CustomerEmail = "";
            CustomerFullName = "";
            CustomerOrganization = "";
            ProductsInSolution.Clear();
        }

        public async Task SuspendSolution()
        {
            try
            {
                CurrentSolution.PrepareSolutionForSuspension();
                await AddSolution(CurrentSolution);
                ClearSolution();
            }
            catch 
            {
               
            }
        }
       
        public async void AddCustomer(Customer customerToAdd)
        {
            await AddNewCustomer(customerToAdd);
            if (SolutionHelper.CurrentSolution == null)
            {
                SolutionHelper.CreateNewSolution(customerToAdd);
            }

        }
        public void AnonymousCustomer()
        {
            Customer anonymouscx = new Customer(true);
            CurrentCustomer = anonymouscx;
            SolutionHelper.CurrentSolutionCustomer = anonymouscx;
        }
        public void ResumeSolution(Solution solutionResuming)
        {
            solutionResuming.RebuildSolutionAfterRecall();
            SolutionHelper.CurrentSolution = solutionResuming;
            CurrentSolution = solutionResuming;
            CurrentSolution.GetProductList().ForEach(x => ProductsInSolution.Add(x));
            CurrentCustomer = CurrentSolution.GetCustomer();
            SetPrices();
            SetCustomerInfo();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using retailemployeetoolset.ViewModels;
using System.Windows.Input;
using Windows.UI.Popups;
using retailemployeetoolset.Common;
using retailemployeetoolset.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SolutionView : Page
    {
        SolutionViewModel svm = new SolutionViewModel();
        
        public SolutionView()
        {
            this.InitializeComponent();
            this.DataContext = svm;
            CustomerDatabaseControl.CustomerSelected += GetCustomerFromUserControl;
        }

        private void GetCustomerFromUserControl(Customer passedCustomer)
        {
            svm.AddCustomer(passedCustomer);
        }

        private async Task CreateNewSolution()
        {
            MessageDialog newSolution = new MessageDialog("Would you like to create a new Solution?");
            newSolution.Commands.Add(new UICommand("Yes") { Id = 0 });
            newSolution.Commands.Add(new UICommand("No") { Id = 1 });
            var result = await newSolution.ShowAsync();
            if ((int)result.Id == 0)
            {
                CustomerDatabaseControl.OpenCustomerSelectBlade();
            }
            else
            {
          

            }
        }

    

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PurchaseDeviceView));

        }

        private void ClearSolutionButton_Click(object sender, RoutedEventArgs e)
        {
            svm.ClearSolutionMessage();
        }

        private async void SuspendSolutionButton_Click(object sender, RoutedEventArgs e)
        {
            await svm.SuspendSolution();
            svm = new SolutionViewModel();
            this.DataContext = svm;
        }

        private async void RecallSolutionButton_Click(object sender, RoutedEventArgs e)
        {
            BladeViewer.IsEnabled = true;
            SuspendedSolutionBlade.IsOpen = true;
        }

        private void SendRunnerButton_Click(object sender, RoutedEventArgs e)
        {
            //not implimented
        }

        private void SendPOSButton_Click(object sender, RoutedEventArgs e)
        {
            //not implimented
        }

        private void AnonymousButton_Click(object sender, RoutedEventArgs e)
        {
            svm.AnonymousCustomer();
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerDatabaseControl.OpenCustomerSelectBlade();

        }

        private void ResumeSolutionButton_Click(object sender, RoutedEventArgs e)
        {
            Solution solutionToResume = SuspendedSolutionList.SelectedItem as Solution;
            svm.ResumeSolution(solutionToResume);
            SuspendedSolutionBlade.IsOpen = false;
            BladeViewer.IsEnabled = false;
        }


        private void CloseSolutionResumeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            SuspendedSolutionBlade.IsOpen = false;
            BladeViewer.IsEnabled = false;
        }

        private void EditCustomerButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (SolutionHelper.CurrentSolutionCustomer != null)
            {
                //not implimented
            }
        }

    }
}

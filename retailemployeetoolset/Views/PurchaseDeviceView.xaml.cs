using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using retailemployeetoolset.Models;
using retailemployeetoolset.ViewModels;
using System.Threading.Tasks;
using retailemployeetoolset.Views;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PurchaseDeviceView : Page
    {
        /// <summary>
        /// The PDVM
        /// </summary>
        private PurchaseDeviceViewModel pdvm;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseDeviceView"/> class.
        /// </summary>
        /// <param name="purchaseDeviceVirModel">The purchase device vir model.</param>
        public PurchaseDeviceView()
        {
            pdvm = new PurchaseDeviceViewModel();
            this.InitializeComponent();
            this.DataContext = pdvm;
            CustomerDatabaseControl.CustomerSelected += GetCustomerFromUserControl;
        }

        private void GetCustomerFromUserControl(Customer passedCustomer)
        {
            pdvm.AddCustomer(passedCustomer);
        }

        //Calls CreatNewSolution() before navigating to the SurfaceSelectedView
        /// <summary>
        /// Handles the Click event of the SurfaceButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void SurfaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (await CreateNewSolution())
            {
                this.Frame.Navigate(typeof(SurfaceSelectedView));
            }
        }

        //Calls CreatNewSolution() before navigating to the OEMView 
        /// <summary>
        /// Handles the Click event of the PCButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void PCButton_Click(object sender, RoutedEventArgs e)
        {
            if (await CreateNewSolution())
            {
                this.Frame.Navigate(typeof(OEMView));
            }
        }

        //Calls CreatNewSolution() before navigating to the XboxView
        /// <summary>
        /// Handles the Click event of the XboxButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void XboxButton_Click(object sender, RoutedEventArgs e)
        {
            if (await CreateNewSolution())
            {
                this.Frame.Navigate(typeof(XboxView));
            }
        }

        //Opts out of adding a customer to the solution and sets the solution customer to anonymous
        /// <summary>
        /// Handles the Click event of the AnonymouscustomerButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AnonymouscustomerButton_Click(object sender, RoutedEventArgs e)
        {
            pdvm.ContinueAsAnonymous();
        }
        

        //Determines if there is an active customer/solution, if not the user is prompted to create a new solution and either create/add a customer to it
        //or continue anonymously
        /// <summary>
        /// Creates the new solution.
        /// </summary>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        private async Task<bool> CreateNewSolution()
        {
            bool? proceedWithSolution = await pdvm.CreateASolutionPrompt();
            if (proceedWithSolution.HasValue && proceedWithSolution.Value)
            {
                CustomerDatabaseControl.OpenCustomerSelectBlade();
                return false;
            }
            if (proceedWithSolution.HasValue && !proceedWithSolution.Value)
            {
                pdvm.ContinueAsAnonymous();
                return true;
            }
            if (proceedWithSolution == null)
            {
                return true;
            }
            return false;
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerDatabaseControl.OpenCustomerSelectBlade();
        }
    }
}

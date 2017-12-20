using retailemployeetoolset.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Messaging;
using retailemployeetoolset.Common;
using retailemployeetoolset.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GreeterView : Page
    {
        QueueViewModel qvm;
        private string selectedCustomerID;
        public GreeterView()
        {
            qvm = new QueueViewModel();
            this.InitializeComponent();
            this.DataContext = qvm;

        }

        private void AddNewCustomerButton_OnClick(object sender, RoutedEventArgs e)
        {
            qvm.AddNewCustomerToQueue(FirstNameBox.Text, LastNameBox.Text, EmailBox.Text, BusinessCustomerCheckbox.IsChecked.Value, PhoneBox.Text, BusinessNameBox.Text, customerDescription.Text, customerInterestBox.Text, selectedCustomerID);
            ListPanel.Width = 1190;
            CreatNewCustomer.Visibility = Visibility.Visible;
            AddCustomerPanel.Visibility = Visibility.Collapsed;
            AddCustomerBlade.IsOpen = false;
            BladeViewer.IsEnabled = false;
        }

        private void BusinessCustomerCheckbox_OnChecked(object sender, RoutedEventArgs e)
        {
            OrgNamePanel.Visibility = Visibility.Visible;
        }

        private void AddSelectedCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Customer newCx = (CustomerList.SelectedItem as Customer);
            FirstNameBox.Text = newCx.CustomerFirstName;
            LastNameBox.Text = newCx.CustomerLastName;
            EmailBox.Text = newCx.CustomerEmail;
            BusinessCustomerCheckbox.IsChecked = newCx.BusinessCustomer;
            PhoneBox.Text = newCx.CustomerPhoneNumber;
            BusinessNameBox.Text = newCx.OrganizationName;
            selectedCustomerID = newCx.Id;
            ListPanel.Width = 728;
            CreatNewCustomer.Visibility = Visibility.Collapsed;
            AddCustomerPanel.Visibility = Visibility.Visible;

        }

        private void CreatNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            ListPanel.Width = 728;
            CreatNewCustomer.Visibility = Visibility.Collapsed;
            AddCustomerPanel.Visibility = Visibility.Visible;
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            BladeViewer.IsEnabled = true;
            AddCustomerBlade.IsOpen = true;
        }

        private void CancelCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QueueCustomer qc = CustomerQueue.SelectedItem as QueueCustomer;
                qvm.CancelCustomer(qc);
            }
            catch 
            {
                
            }
            
        }

        private void CloseCustomerWindow_OnClick(object sender, RoutedEventArgs e)
        {
            ListPanel.Width = 1190;
            CreatNewCustomer.Visibility = Visibility.Visible;
            AddCustomerPanel.Visibility = Visibility.Collapsed;
            AddCustomerBlade.IsOpen = false;
            BladeViewer.IsEnabled = false;
        }
    }
}

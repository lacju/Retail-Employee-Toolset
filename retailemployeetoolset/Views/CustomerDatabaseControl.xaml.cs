// ***********************************************************************
// Assembly         : retailemployeetoolset
// Author           : Lacju
// Created          : 06-25-2017
//
// Last Modified By : Lacju
// Last Modified On : 06-29-2017
// ***********************************************************************
// <copyright file="CustomerDatabaseControl.xaml.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.Toolkit.Uwp.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// Class CustomerDatabaseControl.
    /// </summary>
    public sealed partial class CustomerDatabaseControl : UserControl, INotifyPropertyChanged
    {
        public delegate void CustomerSelectedHandler(Customer selectedCustomer);
        public event CustomerSelectedHandler CustomerSelected;


        private Customer passCustomer()
        {
            return SelectedCustomer;
        }

        /// <summary>
        /// Gets or sets the selected customer.
        /// </summary>
        /// <value>The selected customer.</value>
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
                if (CustomerSelected != null)
                {
                    CustomerSelected(SelectedCustomer);
                }
            }
        }

        public void OpenCustomerSelectBlade()
        {
            CustomerSearchBladeview.IsEnabled = true;
            SearchCustomerBlade.IsOpen = true;
        }
        /// <summary>
        /// The selected customer
        /// </summary>
        private Customer _selectedCustomer;

        /// <summary>
        /// Gets or sets the customer database.
        /// </summary>
        /// <value>The customer database.</value>
        public AdvancedCollectionView CustomerDatabase
        {
            get { return (AdvancedCollectionView)GetValue(CustomerAdvancedCollectionViewProperty); }
            set { SetValueCustomerAdvancedViewDP(CustomerAdvancedCollectionViewProperty, value); }
        }

        /// <summary>
        /// The customer advanced collection view property
        /// </summary>
        public static readonly DependencyProperty CustomerAdvancedCollectionViewProperty =
            DependencyProperty.Register("CustomerDatabase", typeof(AdvancedCollectionView),
                typeof(CustomerDatabaseControl), null);

        /// <summary>
        /// Sets the value customer advanced view dp.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="p">The p.</param>
        void SetValueCustomerAdvancedViewDP(DependencyProperty property, object value,
            [CallerMemberName] String p = null)
        {
            SetValue(property, value);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        /// <summary>
        /// The customer databse
        /// </summary>
        private AdvancedCollectionView _customerDatabse;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDatabaseControl"/> class.
        /// </summary>
        public CustomerDatabaseControl()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
            CustomerSearchBladeview.IsEnabled = false;
            SearchCustomerBlade.IsOpen = false;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var temp = PropertyChanged;
            if (temp != null)
                temp(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddNewCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            bool businessCustomer = false;
            if (BusinessCustomerCheckbox.IsChecked == true)
            {
                businessCustomer = true;
            }
            Customer newCustomer = new Customer(FirstNameBox.Text, LastNameBox.Text, EmailBox.Text, businessCustomer, PhoneBox.Text, BusinessNameBox.Text);
            CustomerSelected(newCustomer);
        }
    }
}
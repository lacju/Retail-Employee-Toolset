using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.UI;
using Microsoft.WindowsAzure.MobileServices.Sync;
using retailemployeetoolset.Common;
using retailemployeetoolset.Models;

namespace retailemployeetoolset.ViewModels
{
    /// <summary>
    /// Class ViewModel.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class ViewModel : INotifyPropertyChanged
    {
        private long pendingChanges;
        private bool isStatusBarVisible;

        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Gets or sets the appointments.
        /// </summary>
        /// <value>The appointments.</value>
        public ObservableCollection<Appointment> Appointments
        {
            get
            {
                return _appointments;
            }
            set
            {
                _appointments = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Solution> Solutions
        {
            get
            {
                return _solutions;
            }
            set
            {
                _solutions = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the customers.
        /// </summary>
        /// <value>The customers.</value>
        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            } 
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>The products.</value>
        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<QueueCustomer> QueueCustomers
        {
            get
            {
                return _queueCustomers;
            }
            set
            {
                _queueCustomers = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The appointment manager
        /// </summary>
        /// <summary>
        /// The appointments
        /// </summary>
        private ObservableCollection<Appointment> _appointments;

        private ObservableCollection<Solution> _solutions;
        /// <summary>
        /// The customer manager
        /// </summary>
        /// <summary>
        /// The customers
        /// </summary>
        private ObservableCollection<Customer> _customers;
        /// <summary>
        /// The product manager
        /// </summary>
        /// <summary>
        /// The products
        /// </summary>
        private ObservableCollection<Product> _products;
        private ObservableCollection<QueueCustomer> _queueCustomers;

        public ViewModel()
        {
            LoadDataBases();
        }

        public async void LoadDataBases()
        {
            _appointments = new ObservableCollection<Appointment>();
            _solutions = new ObservableCollection<Solution>();
            _customers = new ObservableCollection<Customer>();
            _products = new ObservableCollection<Product>();
            _queueCustomers = new ObservableCollection<QueueCustomer>();
            await LoadAppointments();
            await LoadCustomers();
            await LoadAppointments();
            await LoadSolutions();
            await LoadQueueCustomers();
            await LoadProducts();
        }

        /// <summary>
        /// Loads the appointments.
        /// </summary>
        /// <returns>Task.</returns>
        protected async Task LoadAppointments()
        {
            var appointments = await DataManager.GetAppointmentsAsync();

            foreach (var i in appointments)
            {
                Appointments.Add(i);
            }
        }

        /// <summary>
        /// synchronize appointments as an asynchronous operation.
        /// </summary>
        /// <returns>Task.</returns>
        protected async Task SyncAppointmentsAsync()
        {
            await DataManager.SyncAppointmentsAsync();
            await LoadAppointments();
        }

        /// <summary>
        /// Adds the appointment.
        /// </summary>
        /// <param name="appointment">The appointment.</param>
        /// <returns>Task.</returns>
        protected async Task AddAppointment(Appointment appointment)
        {
            await DataManager.SaveAppointmentAsync(appointment);
            await LoadAppointments();
        }

        /// <summary>
        /// Loads the customers.
        /// </summary>
        /// <returns>Task.</returns>
        protected async Task LoadCustomers()
        {
            var customers = await DataManager.GetCustomersAsync();

            foreach (var i in customers)
                Customers.Add(i);

        }

        /// <summary>
        /// synchronize customers as an asynchronous operation.
        /// </summary>
        /// <returns>Task.</returns>
        protected async Task SyncCustomersAsync()
        {
            await DataManager.SyncCustomersAsync();
            await LoadCustomers();
        }

        /// <summary>
        /// Adds the new customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns>Task.</returns>
        protected async Task AddNewCustomer(Customer customer)
        {
            await DataManager.SaveCustomerAsync(customer);
            await LoadCustomers();
        }

        /// <summary>
        /// Loads the products.
        /// </summary>
        /// <returns>Task.</returns>
        protected async Task LoadProducts()
        {
            var products = await DataManager.GetProductsAsync();

            foreach (var i in products)
            {
                Products.Add(i);
            }
        }

        /// <summary>
        /// synchronize products as an asynchronous operation.
        /// </summary>
        /// <returns>Task.</returns>
        protected async Task SyncProductsAsync()
        {
            await DataManager.SyncProductsAsync();
            await LoadProducts();
        }

        /// <summary>
        /// Adds the new product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>Task.</returns>
        protected async Task AddNewProduct(Product product)
        {
            await DataManager.SaveProductsAsync(product);
            await LoadCustomers();
        }


        protected async Task LoadQueueCustomers()
        {
            var queuecustomers = await DataManager.GetQueueCustomersAsync();
            foreach (var i in queuecustomers)
            {
                QueueCustomers.Add(i);
            }
        }

        protected async Task SyncQueueCustomersAsync()
        {
            await DataManager.SyncQueueCustomerAsync();
            await LoadQueueCustomers();
        }

        protected async Task AddQueueCustomer(QueueCustomer queueCustomer)
        {
            await DataManager.SaveQueueCustomerAsync(queueCustomer);
            await LoadQueueCustomers();
        }


        protected async Task LoadSolutions()
        {
            var solutions = await DataManager.GetSolutionsAsync();
            foreach (var solution in solutions)
            {
                Solutions.Add(solution);
            }
        }

        protected async Task SyncSolutionsAsync()
        {
            await DataManager.SyncSolutionsAsync();
            await LoadQueueCustomers();
        }

        protected async Task AddSolution(Solution queueCustomer)
        {
            await DataManager.SaveSolutionAsync(queueCustomer);
            await LoadQueueCustomers();
        }
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var temp = PropertyChanged;
            if (temp != null)
                temp(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
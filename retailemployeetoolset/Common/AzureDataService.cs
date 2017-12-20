//using Microsoft.WindowsAzure.MobileServices;
//using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
//using Microsoft.WindowsAzure.MobileServices.Sync;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using retailemployeetoolset.Models;
//using Newtonsoft.Json;
//using Microsoft.Data.OData;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.ObjectModel;
//using Windows.UI.Popups;

//namespace retailemployeetoolset.Common
//{
//    public static class AzureDataService

//    {
//        public static MobileServiceClient MobileService { get; set; }
//        public static IMobileServiceSyncTable<Product> productTable;
//        public static IMobileServiceSyncTable<Solution> solutionTable;
//        public static IMobileServiceSyncTable<Customer> customerTable;
//        public static IMobileServiceSyncTable<QueueCustomer> queueCustomerTable;
//        public static IMobileServiceSyncTable<Appointment> appointmentTable;



//        public static async Task Intialize()
//        {
//            //Create our client
//            MobileService = new MobileServiceClient("https://retailemployeetoolset.azurewebsites.net");
//            const string Path = "syncstore1.db";
//            //setup our local sqlite store and intialize our table
//            var syncstore = new MobileServiceSQLiteStore(Path);
//            // Product test = new Product();
//            syncstore.DefineTable<Product>();
//            syncstore.DefineTable<Solution>();
//            syncstore.DefineTable<Customer>();
//            syncstore.DefineTable<QueueCustomer>();
//            syncstore.DefineTable<Appointment>();
//            await MobileService.SyncContext.InitializeAsync(syncstore, new MobileServiceSyncHandler());
//            //Get our sync table that will call out to azure
//            productTable = MobileService.GetSyncTable<Product>();
//            solutionTable = MobileService.GetSyncTable<Solution>();
//            customerTable = MobileService.GetSyncTable<Customer>();
//            appointmentTable = MobileService.GetSyncTable<Appointment>();
//            queueCustomerTable = MobileService.GetSyncTable<QueueCustomer>();

//        }

//        public static async Task<ObservableCollection<Appointment>> GetAppointments()
//        {
//            await SyncAppointments();
//            return await appointmentTable.ToCollectionAsync();
//        }

//        public static async Task AddSolution(Appointment appointmentToAdd)
//        {
//            //create and insert coffee
//            await appointmentTable.InsertAsync(appointmentToAdd);
//            //Synchronize coffee
//            await SyncAppointments();
//        }

//        public static async Task SyncAppointments()
//        {
//            //pull down all latest changes and then push current coffees up
//            await appointmentTable.PullAsync("allProducts", appointmentTable.CreateQuery());
//            await MobileService.SyncContext.PushAsync();
//        }

//        public static async Task<ObservableCollection<Solution>> GetSolutions()
//        {
//            await SyncSolutions();
//            return await solutionTable.ToCollectionAsync();
//        }

//        public static async Task AddSolution(Solution solutionToAdd)
//        {
//            //create and insert coffee
//            await solutionTable.InsertAsync(solutionToAdd);
//            //Synchronize coffee
//            await SyncSolutions();
//        }

//        public static async Task SyncSolutions()
//        {
//            //pull down all latest changes and then push current coffees up
//            await solutionTable.PullAsync("allSolutions", solutionTable.CreateQuery());
//            await MobileService.SyncContext.PushAsync();


//        }

//        public static async Task<ObservableCollection<QueueCustomer>> GetQueueCustomers()
//        {
//            await SyncQueueCustomers();
//            return await queueCustomerTable.ToCollectionAsync();
//        }

//        public static async Task UpdateQueueCustomer(QueueCustomer qcToUpdate)
//        {
//            await queueCustomerTable.UpdateAsync(qcToUpdate);
//            await SyncQueueCustomers();
//        }
//        public static async Task AddQueueCustomer(QueueCustomer qcToAdd)
//        {
//            //create and insert coffee
//            await queueCustomerTable.InsertAsync(qcToAdd);
//            //Synchronize coffee
//            await SyncQueueCustomers();
//        }

//        public static async Task SyncQueueCustomers()
//        {
//            //pull down all latest changes and then push current coffees up
//            await queueCustomerTable.PullAsync("allQueueCustomers", queueCustomerTable.CreateQuery());
//            await MobileService.SyncContext.PushAsync();
//        }

//        public static async Task<ObservableCollection<Customer>> GetCustomers()
//        {
//            await SyncCustomers();
//            return await customerTable.ToCollectionAsync();
//        }

//        public static async Task AddCustomer(Customer customerToAdd)
//        {
//            //create and insert coffee
//            await customerTable.InsertAsync(customerToAdd);
//            //Synchronize coffee
//            await SyncCustomers();
//        }

//        public static async Task SyncCustomers()
//        {
//            //pull down all latest changes and then push current coffees up
//            await customerTable.PullAsync("allCustomers", customerTable.CreateQuery());
//            await MobileService.SyncContext.PushAsync();
//        }

//        public static async Task<List<Product>> GetProducts()
//        {
//            await SyncProducts();
//            return await productTable.ToListAsync();
//        }
//        public static async Task AddProduct(Product productToAdd)
//        {
//            //create and insert coffee
//            await productTable.InsertAsync(productToAdd);
//            //Synchronize coffee
//            await SyncProducts();
//        }
//        public static async Task RemoveProduct(Product productToRemove)
//        {
//            await productTable.DeleteAsync(productToRemove);
//            await SyncProducts();
//        }
//        public static async Task SyncProducts()
//        {
//            //pull down all latest changes and then push current coffees up

//                await productTable.PullAsync("allProducts", productTable.CreateQuery());
//                await MobileService.SyncContext.PushAsync();


//        }


//    }
//}

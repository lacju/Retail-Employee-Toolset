using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using retailemployeetoolset.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using retailemployeetoolset.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace retailemployeetoolset.Common
{
    public static class DataManager
    {

        static MobileServiceClient client;

        static IMobileServiceSyncTable<QueueCustomer> _queueCustomerTable;
        static IMobileServiceSyncTable<Customer> _customerTable;
        static IMobileServiceSyncTable<Solution> _solutionTable;
        static IMobileServiceSyncTable<Product> _productTable;
        static IMobileServiceSyncTable<Appointment> _appointmentTable;





        static DataManager() { }

        public static async Task InitializeSyncTables()
        {
            client = new MobileServiceClient(Constants.WebApp
            );

            var store = new MobileServiceSQLiteStore("localstore4.db" + DateTime.UtcNow.Ticks);

            store.DefineTable<QueueCustomer>();
            store.DefineTable<Customer>();
            store.DefineTable<Solution>();
            store.DefineTable<Product>();
            store.DefineTable<Appointment>();


            // Initialize the SyncContext using the default IMobileServiceSyncHandler
            await client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            _queueCustomerTable = client.GetSyncTable<QueueCustomer>();
            _customerTable = client.GetSyncTable<Customer>();
            _solutionTable = client.GetSyncTable<Solution>();
            _productTable = client.GetSyncTable<Product>();
            _appointmentTable = client.GetSyncTable<Appointment>();
            SyncAllTables().Wait();

        }

        public static IMobileServiceClient MobileServiceClient
        {
            get
            {
                return client;
            }
        }

        public static async Task SyncAllTables() 
        {
            await SyncAppointmentsAsync();
            await SyncCustomersAsync();
            await SyncProductsAsync();
            await SyncQueueCustomerAsync();
            await SyncSolutionsAsync();
        }

        #region QueueCustomer Methods

        public static async Task SyncQueueCustomerAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await client.SyncContext.PushAsync();
                // A normal pull will automatically process new/modified/deleted files, engaging the file sync handler
                await _queueCustomerTable.PullAsync("QueueCustomers", _queueCustomerTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }
                }
            }
        }

        public static async Task<IEnumerable<QueueCustomer>> GetQueueCustomersAsync()
        {
            try
            {
                return await _queueCustomerTable.OrderBy(item => item.CustomerCheckinTime).ToListAsync();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }

        public static async Task SaveQueueCustomerAsync(QueueCustomer item)
        {
            if (item.Id == null)
            {
                await _queueCustomerTable.InsertAsync(item);
            }
            else
            {
                await _queueCustomerTable.UpdateAsync(item);
            }
        }

        public static async Task DeleteQueueCustomerAsync(QueueCustomer item)
        {
            try
            {
                //TodoViewModel.QueueCustomers.Remove(item);
                await _queueCustomerTable.DeleteAsync(item);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
        }

        #endregion

        #region Customer Methods

        public static async Task SyncCustomersAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await client.SyncContext.PushAsync();
                // A normal pull will automatically process new/modified/deleted files, engaging the file sync handler
                await _customerTable.PullAsync("Customers", _customerTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }
                }
            }
        }

        public static async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            try
            {
                return await _customerTable.OrderBy(item => item.CustomerLastName).ToListAsync();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }

        public static async Task SaveCustomerAsync(Customer item)
        {
            if (item.Id == null)
            {
                await _customerTable.InsertAsync(item);
            }
            else
            {
                await _customerTable.UpdateAsync(item);
            }
        }

        public static async Task DeleteCustomerAsync(Customer item)
        {
            try
            {
                //TodoViewModel.Customers.Remove(item);
                await _customerTable.DeleteAsync(item);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
        }

        #endregion

        #region Solution Methods

        public static async Task SyncSolutionsAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await client.SyncContext.PushAsync();
                // A normal pull will automatically process new/modified/deleted files, engaging the file sync handler
                await _solutionTable.PullAsync("Solutions", _solutionTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }
                }
            }
        }

        public static async Task<IEnumerable<Solution>> GetSolutionsAsync()
        {
            try
            {
                return await _solutionTable.OrderBy(item => item.DateCreated).ToListAsync();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }

        public static async Task SaveSolutionAsync(Solution item)
        {
            if (item.Id == null)
            {
                await _solutionTable.InsertAsync(item);
            }
            else
            {
                await _solutionTable.UpdateAsync(item);
            }
        }

        public static async Task DeleteSolutionAsync(Solution item)
        {
            try
            {
                //TodoViewModel.QueueCustomers.Remove(item);
                await _solutionTable.DeleteAsync(item);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
        }

        #endregion

        #region Product Methods

        public static async Task SyncProductsAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await client.SyncContext.PushAsync();
                // A normal pull will automatically process new/modified/deleted files, engaging the file sync handler
                await _productTable.PullAsync("Products", _productTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }
                }
            }
        }

        public static async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                return await _productTable.OrderBy(x => x.Category).ToListAsync();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }

        public static async Task SaveProductsAsync(Product item)
        {
            if (item.Id == null)
            {
                await _productTable.InsertAsync(item);
            }
            else
            {
                await _productTable.UpdateAsync(item);
            }
        }

        public static async Task DeleteProductsAsync(Product item)
        {
            try
            {
                //TodoViewModel.Products.Remove(item);
                await _productTable.DeleteAsync(item);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
        }

        #endregion

        #region Appointment Methods

        public static async Task SyncAppointmentsAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await client.SyncContext.PushAsync();
                // A normal pull will automatically process new/modified/deleted files, engaging the file sync handler
                await _appointmentTable.PullAsync("Appointments", _appointmentTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }
                }
            }
        }

        public static async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            try
            {
                return await _appointmentTable.OrderBy(item => item.AppointmentScheduledTime).ToListAsync();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }

        public static async Task SaveAppointmentAsync(Appointment item)
        {
            if (item.Id == null)
            {
                await _appointmentTable.InsertAsync(item);
            }
            else
            {
                await _appointmentTable.UpdateAsync(item);
            }
        }

        public static async Task DeleteAppointmentAsync(Appointment item)
        {
            try
            {
                //TodoViewModel.QueueCustomers.Remove(item);
                await _appointmentTable.DeleteAsync(item);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
        }

        #endregion
    }
}

  
/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=717898
 */
//#define OFFLINE_SYNC_ENABLED

using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using retailemployeetoolset.Common;
using retailemployeetoolset.Models;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;  // offline sync
using Microsoft.WindowsAzure.MobileServices.Sync;         // offline sync
#endif

namespace retailemployeetoolset
{
    public sealed partial class MainPage : Page
    {
        private MobileServiceCollection<Product, Product> items;
#if OFFLINE_SYNC_ENABLED
        private IMobileServiceSyncTable<Product> productTable = App.MobileService.GetSyncTable<Product>(); // offline sync
#else
        private IMobileServiceTable<Product> productTable = App.MobileService.GetTable<Product>();
#endif
        
        public  MainPage()
        {
            
            this.InitializeComponent();
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#if OFFLINE_SYNC_ENABLED
            await InitLocalStoreAsync(); // offline sync
#endif
            ButtonRefresh_Click(this, null);
        }

        private async Task InsertProduct(Product Product)
        {
            // This code inserts a new Product into the database. After the operation completes
            // and the mobile app backend has assigned an id, the item is added to the CollectionView.
            await productTable.InsertAsync(Product);
            items.Add(Product);

#if OFFLINE_SYNC_ENABLED
            await App.MobileService.SyncContext.PushAsync(); // offline sync
#endif
        }

        private async Task RefreshProducts()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the Products table.
                // The query excludes completed Products.
                items = await productTable
                    .Where(Product => Product.SKU !=  null)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                //ListItems.ItemsSource = items;
                //this.ButtonSave.IsEnabled = true;
            }
        }

        private async Task UpdateCheckedProduct(Product item)
        {
            // This code takes a freshly completed Product and updates the database.
			// After the MobileService client responds, the item is removed from the list.
            await productTable.UpdateAsync(item);
            items.Remove(item);
            //ListItems.Focus(Windows.UI.Xaml.FocusState.Unfocused);

#if OFFLINE_SYNC_ENABLED
            await App.MobileService.SyncContext.PushAsync(); // offline sync
#endif
        }

        private async void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            //ButtonRefresh.IsEnabled = false;

#if OFFLINE_SYNC_ENABLED
            await SyncAsync(); // offline sync
#endif
            await RefreshProducts();

            //ButtonRefresh.IsEnabled = true;
        }

        //private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        //{
        //    var Product = new Product { Text = TextInput.Text };
        //    TextInput.Text = "";
        //    await InsertProduct(Product);
        //}

        private async void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Product item = cb.DataContext as Product;
            await UpdateCheckedProduct(item);
        }

        private void TextInput_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter) {
                //ButtonSave.Focus(FocusState.Programmatic);
            }
        }

        #region Offline sync
#if OFFLINE_SYNC_ENABLED
        private async Task InitLocalStoreAsync()
        {
           if (!App.MobileService.SyncContext.IsInitialized)
           {
               var store = new MobileServiceSQLiteStore("localstore.db");
               store.DefineTable<Product>();
               await App.MobileService.SyncContext.InitializeAsync(store);
           }

           await SyncAsync();
        }

        private async Task SyncAsync()
        {
           await App.MobileService.SyncContext.PushAsync();
           await productTable.PullAsync("Products", productTable.CreateQuery());
        }
#endif
        #endregion
    }
}

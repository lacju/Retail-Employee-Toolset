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
using retailemployeetoolset.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductCatalogView : Page
    {
        private ProductCatalogViewModel pcvm;
        public ProductCatalogView()
        {
            pcvm = new ProductCatalogViewModel();
            this.InitializeComponent();
            this.DataContext = pcvm;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SearchTextBoxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            pcvm.FilterProductDatabase(SearchTextBoxt.Text); //passes users search term into the vm filter method
        }

        private void SearchTextBoxt_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            pcvm.FilterProductDatabase(SearchTextBoxt.Text); //passes users search term into the vm filter method
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            pcvm.ClearAllFilters(); 
        }
    }
}

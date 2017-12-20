using retailemployeetoolset.Models;
using retailemployeetoolset.ViewModels;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConfigurationView : Page
    {
        public ConfigurationViewModel cvm;
        public ConfigurationView()
        {
            cvm = new ConfigurationViewModel();
            this.InitializeComponent();
            this.DataContext = cvm;
        }


 

        private void AddNewProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            
           // cvm.RemoveProductFromDatabase(DatabaseList.SelectedItem as Product);
        }

        private void LoadDatabase_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

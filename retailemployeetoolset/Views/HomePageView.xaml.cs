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
    public sealed partial class HomePageView : Page
    {
        HomePageViewModel hpvm;
        public HomePageView()
        {
            hpvm = new HomePageViewModel();
            this.InitializeComponent();
            this.DataContext = hpvm;
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationHelper.SignOut();
        }

        private void PurchaseDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PurchaseDeviceView));
        }

        private void GetHelpButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EventsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

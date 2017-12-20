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
using retailemployeetoolset.Common;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView : Page
    {
       
        LoginViewModel lvm;
        public LoginView()
        {
            this.InitializeComponent();
            lvm = new LoginViewModel();
            
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (await lvm.AuthenticateAsync())
            {
              // DataManager.PopulateDataOnStart();
                frame.Navigate(typeof(Views.NavView));
                Window.Current.Content = frame;
                Window.Current.Activate();
            } 
        }
        Frame frame = new Frame();
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (lvm.IsAuthenticated().Result)
            {
              //  DataManager.PopulateDataOnStart();
                frame.Navigate(typeof(Views.NavView));
                Window.Current.Content = frame;
                Window.Current.Activate();
            }
        }


    }
}

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


namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavView : Page
    {
        NavViewModel vm;
        public NavView()
        {
            this.InitializeComponent();
            vm = new NavViewModel();
            hamburgerMenuControl.ItemsSource = vm.MainMenuItems;
            hamburgerMenuControl.OptionsItemsSource = vm.OptionalMenuItems;
            contentFrame.Navigate(typeof(Views.HomePageView));
        }

        private void OnMenuItemClick(object sender, ItemClickEventArgs e)
        {
            var menuitem = e.ClickedItem as MenuItem;
            contentFrame.Navigate(menuitem.PageType);
        }

        private async void OnOptionalItemMenuItemClick(object sender, ItemClickEventArgs e)
        {
            await vm.LogoutAsync();
            var menuItem = e.ClickedItem as MenuItem;
            Frame.Navigate(menuItem.PageType);
        }
    }
}

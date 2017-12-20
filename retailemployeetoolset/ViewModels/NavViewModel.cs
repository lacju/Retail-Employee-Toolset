using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.UI.Xaml.Controls;
using retailemployeetoolset.Common;
using retailemployeetoolset.Models;
using retailemployeetoolset.Views;

namespace retailemployeetoolset.ViewModels
{
    public class MenuItem
    {
        public Symbol Icon { get; set; }
        public String Name { get; set; }
        public Type PageType { get; set; }
    }
    public class NavViewModel
    {
        public List<MenuItem> MainMenuItems = new List<MenuItem>();
        public List<MenuItem> OptionalMenuItems = new List<MenuItem>();


        public NavViewModel()
        {
            if (MainMenuItems.Count == 0 && OptionalMenuItems.Count == 0)
            {
                MainMenuItems.Add(new MenuItem() { Icon = Symbol.Home, Name = "Home", PageType = typeof(Views.HomePageView) });
                MainMenuItems.Add(new MenuItem() { Icon = Symbol.ViewAll, Name = "Current Solution", PageType = typeof(Views.SolutionView) });
                MainMenuItems.Add(new MenuItem() { Icon = Symbol.AddFriend, Name = "Customer Queue", PageType = typeof(GreeterView) });
                MainMenuItems.Add(new MenuItem() { Icon = Symbol.Help, Name = "Answer Desk", PageType = typeof(Views.AnswerDeskView) });
                MainMenuItems.Add(new MenuItem() { Icon = Symbol.List, Name = "Product Catalog", PageType = typeof(ProductCatalogView) });
             //   OptionalMenuItems.Add(new MenuItem() { Icon = Symbol.Back, Name = "Logout", PageType = typeof(Views.ConfigurationView) });
            }
        }


        private PasswordVault vault = new PasswordVault();
        private PasswordCredential credntial;
        public async Task LogoutAsync()
        {
            var provider = MobileServiceAuthenticationProvider.Facebook;
            try
            {
                credntial = vault.FindAllByResource(provider.ToString()).FirstOrDefault();
            }
            catch (Exception)
            {

                //do nothing
            }
            if (credntial != null)
            {
                vault.Remove(credntial);
                
            }
        }
    }
}

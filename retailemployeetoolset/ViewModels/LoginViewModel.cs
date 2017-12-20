using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.UI.Popups;

namespace retailemployeetoolset.ViewModels
{
    class LoginViewModel
    {
        private MobileServiceUser user;
        private PasswordVault vault = new PasswordVault();
        private PasswordCredential credential;

        public async Task<bool> IsAuthenticated()
        {
            var provider = MobileServiceAuthenticationProvider.MicrosoftAccount;
            bool success = false;
            try
            {
                credential = vault.FindAllByResource(provider.ToString()).FirstOrDefault();
            }
            catch (Exception e)
            {

                MessageDialog cake = new MessageDialog(e.ToString());
                await cake.ShowAsync();
            }
            if (credential != null)
            {
                user = new MobileServiceUser(credential.UserName);
                credential.RetrievePassword();
                user.MobileServiceAuthenticationToken = credential.Password;

                App.MobileService.CurrentUser = user;
                success = true;
            }
            return success;
        }

        public async Task<bool> AuthenticateAsync()
        {
            try
            {
                var provider = MobileServiceAuthenticationProvider.MicrosoftAccount;
                user = await App.MobileService.LoginAsync(provider);
                credential = new PasswordCredential(provider.ToString(), user.UserId, user.MobileServiceAuthenticationToken);
                vault.Add(credential);
                return true;
            }
            catch
            {
                return false;
                
            }

        }

    }
}

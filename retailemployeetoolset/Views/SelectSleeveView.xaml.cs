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
using retailemployeetoolset.Views;
using retailemployeetoolset.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SelectSleeveView : Page
    {
        SleeveSelectionViewModel ssv;
        public SelectSleeveView()
        {
            this.InitializeComponent();
            ssv = new SleeveSelectionViewModel();
            ssv.GotoPage2Command = new RelayCommand(() => { Frame.Navigate(typeof(SolutionView)); });
            this.DataContext = ssv;

            Messenger.Default.Register<NotificationMessage>(this, (nm) =>
            {
                //Check which message you've sent
                if (nm.Notification == "CloseWindowsBoundToMe")
                {
                    //If the DataContext is the same ViewModel where you've called the Messenger
                    if (nm.Sender == this.DataContext)
                        //Do something here, for example call a function. I'm closing the view:
                        this.Frame.Navigate(typeof(SolutionView));
                }
            });
        }
       
        
    }
}

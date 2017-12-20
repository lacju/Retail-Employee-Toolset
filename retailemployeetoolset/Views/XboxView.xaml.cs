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
using GalaSoft.MvvmLight.Messaging;
using retailemployeetoolset.Common;
using retailemployeetoolset.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class XboxView : Page
    {
        private XboxViewModel xvm;
        public XboxView()
        {
            xvm = new XboxViewModel();
            this.InitializeComponent();
            this.DataContext = xvm;


            Messenger.Default.Register<NotificationMessage>(this, (nm) =>
            {
                //Check which message you've sent
                if (nm.Notification == "OpenCompleteBlades")
                {
                    //If the DataContext is the same ViewModel where you've called the Messenger
                    if (nm.Sender == this.DataContext)
                        //Do something here, for example call a function. I'm closing the view:
                        CompleteBladeView.IsEnabled = true;
                        xboxCompleteBlade.IsOpen = true;
                }
                if (nm.Notification == "MoveToSolutionView")
                {
                    //If the DataContext is the same ViewModel where you've called the Messenger
                    if (nm.Sender == this.DataContext)
                        //Do something here, for example call a function. I'm closing the view:
                        this.Frame.Navigate(typeof(SolutionView));
                }
            });
        }

        private void xboxCompleteAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            xvm.AddCompleteToSolution(true);
            CompleteBladeView.IsEnabled = false;
            xboxCompleteBlade.IsOpen = false;
        }

        private void xboxCompleteDontAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            xvm.AddCompleteToSolution(false);
            CompleteBladeView.IsEnabled = false;
            xboxCompleteBlade.IsOpen = false;
        }
    }
}

using retailemployeetoolset.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using retailemployeetoolset.Common;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OEMView : Page
    {
        OEMViewModel ovm;
        public OEMView()
        {
            ovm = new OEMViewModel();
            this.InitializeComponent();
           // ovm.OpenCompleteBlade = new RelayCommand(() => { CompleteBladeView.IsEnabled = true; com });
            this.DataContext = ovm;

            Messenger.Default.Register<NotificationMessage>(this, (nm) =>
            {
                //Check which message you've sent
                if (nm.Notification == "OpenCompleteBlades")
                {
                    //If the DataContext is the same ViewModel where you've called the Messenger
                    if (nm.Sender == this.DataContext)
                        //Do something here, for example call a function. I'm closing the view:
                        CompleteBladeView.IsEnabled = true;
                    if (SolutionHelper.BusinessCustomer())
                    {
                        commercialCompleteBlade.IsOpen = true;
                    }
                    else
                    {
                        consumerCompleteBlade.IsOpen = true;
                    }
                }
            });
        }

        private void DesktopButton_Checked(object sender, RoutedEventArgs e)
        {
            LaptopButton.IsChecked = false;
            FormfactorChecked("Desktop");
        }

        private void DesktopButton_Unchecked(object sender, RoutedEventArgs e)
        {
            FormfactorUnchecked();
        }

        private void LaptopButton_Checked(object sender, RoutedEventArgs e)
        {
            DesktopButton.IsChecked = false;
            FormfactorChecked("Laptop");
        }

        private void LaptopButton_Unchecked(object sender, RoutedEventArgs e)
        {
            FormfactorUnchecked();
        }

        private void GPUYesToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            GPUNoToggleButton.IsChecked = false;
            GPUChecked(true);
        }

        private void GPUYesToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void GPUNoToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            GPUYesToggleButton.IsChecked = false;
            GPUChecked(false);
        }

        private void GPUNoToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void procToggleButtoni5_Checked(object sender, RoutedEventArgs e)
        {
            procToggleButtoni7.IsChecked = false;
            ProcessorChecked("i5");
        }

        private void procToggleButtoni5_Unchecked(object sender, RoutedEventArgs e)
        {
            ProcessorUnchecked();
        }

        private void procToggleButtoni7_Checked(object sender, RoutedEventArgs e)
        {
            procToggleButtoni5.IsChecked = false;
            ProcessorChecked("i7");
        }

        private void procToggleButtoni7_Unchecked(object sender, RoutedEventArgs e)
        {
            ProcessorUnchecked();
        }

        private void ramToggleButton8gb_Checked(object sender, RoutedEventArgs e)
        {
            ramToggleButton16gb.IsChecked = false;
            MemoryChecked("8GB");
        }

        private void ramToggleButton8gb_Unchecked(object sender, RoutedEventArgs e)
        {
            MemoryUnchecked();
        }

        private void ramToggleButton16gb_Checked(object sender, RoutedEventArgs e)
        {
            ramToggleButton8gb.IsChecked = false;
            MemoryChecked("16GB");
        }

        private void ramToggleButton16gb_Unchecked(object sender, RoutedEventArgs e)
        {
            MemoryUnchecked();
        }

        private void hddToggleButton128_Checked(object sender, RoutedEventArgs e)
        {
            hddToggleButton256.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            hddToggleButton1TB.IsChecked = false;
            StorageChecked("128GB");
        }

        private void hddToggleButton128_Unchecked(object sender, RoutedEventArgs e)
        {
            StorageUnchecked();
        }

        private void hddToggleButton256_Checked(object sender, RoutedEventArgs e)
        {
            hddToggleButton128.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            hddToggleButton1TB.IsChecked = false;
            StorageChecked("256GB");
        }

        private void hddToggleButton256_Unchecked(object sender, RoutedEventArgs e)
        {
            StorageUnchecked();
        }

        private void hddToggleButton512_Checked(object sender, RoutedEventArgs e)
        {
            hddToggleButton128.IsChecked = false;
            hddToggleButton256.IsChecked = false;
            hddToggleButton1TB.IsChecked = false;
            StorageChecked("512GB");
        }

        private void hddToggleButton512_Unchecked(object sender, RoutedEventArgs e)
        {
            StorageUnchecked();
        }

        private void hddToggleButton1TB_Checked(object sender, RoutedEventArgs e)
        {
            hddToggleButton128.IsChecked = false;
            hddToggleButton256.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            StorageChecked("1TB");
        }

        private void hddToggleButton1TB_Unchecked(object sender, RoutedEventArgs e)
        {
            StorageUnchecked();
        }

        private void FormfactorChecked(string formfactor)
        {
            GPUPanel.Visibility = Visibility.Visible;
            ovm.SelectFormfactor(formfactor);
        }
        private void FormfactorUnchecked()
        {
            //GPUPanel.Visibility = Visibility.Collapsed;
            //LaptopButton.IsChecked = false;
            //DesktopButton.IsChecked = false;
       //     GPUUnchecked();
            ovm.DeselectFormfactor();
        }
        private void GPUChecked(bool withGPU)
        {
            ProcessorPanel.Visibility = Visibility.Visible;
            ovm.SelectGPU(withGPU);
        }
        private void GPUUnchecked()
        {

            //ProcessorPanel.Visibility = Visibility.Collapsed;
            //GPUYesToggleButton.IsChecked = false;
            //GPUNoToggleButton.IsChecked = false;
       //     ProcessorUnchecked();
         ovm.DeselectGPU();
        }
        private void ProcessorChecked(string series)
        {
            MemoryPanel.Visibility = Visibility.Visible;
            ovm.SelectProcessor(series);
        }
        private void ProcessorUnchecked()
        {
            //MemoryPanel.Visibility = Visibility.Collapsed;
            //procToggleButtoni5.IsChecked = false;
            //procToggleButtoni7.IsChecked = false;
         //   MemoryUnchecked();
         ovm.DeselectProcessor();
        }
        private void MemoryChecked(string capacity)
        {
            
          ovm.SelectMemory(capacity);
        }
        private void MemoryUnchecked()
        {
            //StoragePanel.Visibility = Visibility.Collapsed;
            //ramToggleButton8gb.IsChecked = false;
            //ramToggleButton16gb.IsChecked = false;
       //     StorageUnchecked();
           ovm.DeselectMemory();
        }
        private void StorageChecked(string capacity)
        {
            ovm.SelectStorage(capacity);
        }
        private void StorageUnchecked()
        {
            //hddToggleButton128.IsChecked = false;
            //hddToggleButton256.IsChecked = false;
            //hddToggleButton512.IsChecked = false;
            //hddToggleButton1TB.IsChecked = false;
            ovm.DeselectStorage();
        }

        private async void commercialCompleteAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxTermLength.SelectedItem == comboBoxComComlete3Yr)
            {
                NavigateToNextPage(await ovm.AddCompleteToSolution("Commercial3"));
            }
            if (comboBoxTermLength.SelectedItem == comboBoxComComlete4Yr)
            {
                NavigateToNextPage(await ovm.AddCompleteToSolution("Commercial4"));

            }
            else
            {
                MessageDialog noTerm = new MessageDialog("Please Select a term length.");
                await noTerm.ShowAsync();
            }
        }

        private async void commercialCompleteDontAddToSolution_Click(object sender, RoutedEventArgs e)
        {
           NavigateToNextPage(await ovm.AddCompleteToSolution("None"));
        }

        private async void consumerCompleteAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            NavigateToNextPage(await ovm.AddCompleteToSolution("Consumer"));
        }

        private async void consumerCompleteDontAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            NavigateToNextPage(await ovm.AddCompleteToSolution("None"));
        }

        private void NavigateToNextPage(bool addOffice)
        {
            CompleteBladeView.IsEnabled = false;
            consumerCompleteBlade.IsOpen = false;
            commercialCompleteBlade.IsOpen = false;
            if (addOffice)
            {
                this.Frame.Navigate(typeof(OfficeView));
            }
            else
            {
                this.Frame.Navigate(typeof(SolutionView));
            }
        }
        
    }
}

using retailemployeetoolset.Common;
using retailemployeetoolset.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SurfaceBookView : Page
    {
        SurfaceViewModelBase svm;
        public SurfaceBookView()
        {
            SolutionHelper.CurrentSolutionStage = GuidedSolutionStages.DeviceConfiguration;
            SolutionHelper.CurrentSolutionDevice = GuidedSolutionDevices.SurfaceBook;
            svm = new SurfaceViewModelBase();
            this.InitializeComponent();
            this.DataContext = svm;
            ResetPage();
        }

        private void procToggleButtoni5_Checked(object sender, RoutedEventArgs e)
        {
            ResetPage();
            procToggleButtoni7.IsChecked = false;
            GPUPanel.Visibility = Visibility.Visible;
            ramToggleButton8gb.Visibility = Visibility.Visible;
            GPUYesToggleButton.Visibility = Visibility.Visible;
            GPUNoToggleButton.Visibility = Visibility.Visible;
            svm.SelectProcessor("i5");

        }

        private void procToggleButtoni5_Unchecked(object sender, RoutedEventArgs e)
        {
            svm.DeselectProcessor();
            ResetPage();
        }

        private void procToggleButtoni7_Checked(object sender, RoutedEventArgs e)
        {
            ResetPage();
            procToggleButtoni5.IsChecked = false;
            GPUPanel.Visibility = Visibility.Visible;
            ramToggleButton8gb.Visibility = Visibility.Visible;
            ramToggleButton16gb.Visibility = Visibility.Visible;
            GPUYesToggleButton.Visibility = Visibility.Visible;
            svm.SelectProcessor("i7");
        }

        private void procToggleButtoni7_Unchecked(object sender, RoutedEventArgs e)
        {
            svm.DeselectProcessor();
            ResetPage();
        }

        private void GPUYesToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            GPUNoToggleButton.IsChecked = false;
            hddToggleButton128.Visibility = Visibility.Collapsed;
            MemoryPane.Visibility = Visibility.Visible;
        }

        private void GPUYesToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckGPU();
        }

        private void GPUNoToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            GPUYesToggleButton.IsChecked = false;
            MemoryPane.Visibility = Visibility.Visible;
        }

        private void GPUNoToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckGPU();
        }

        private void ramToggleButton8gb_Checked(object sender, RoutedEventArgs e)
        {
            ramToggleButton16gb.IsChecked = false;
            StoragePane.Visibility = Visibility.Visible;
            if (GPUYesToggleButton.IsChecked == true)
            {
                hddToggleButton256.Visibility = Visibility.Visible;
            }
            else hddToggleButton128.Visibility = Visibility.Visible;
            svm.SelectRam("8");
        }

        private void ramToggleButton8gb_Unchecked(object sender, RoutedEventArgs e)
        {
            svm.DeselectRam();
            UncheckRAM();
        }

        private void ramToggleButton16gb_Checked(object sender, RoutedEventArgs e)
        {
            ramToggleButton8gb.IsChecked = false;
            StoragePane.Visibility = Visibility.Visible;
            hddToggleButton512.Visibility = Visibility.Visible;
            hddToggleButton256.Visibility = Visibility.Collapsed;
            svm.SelectRam("16");
        }

        private void ramToggleButton16gb_Unchecked(object sender, RoutedEventArgs e)
        {
            svm.DeselectRam();
            UncheckRAM();
        }

        private void hddToggleButton128_Checked(object sender, RoutedEventArgs e)
        {
            hddToggleButton256.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            PriceButtonPanel.Visibility = Visibility.Visible;

            svm.SelectStorage("128");
        }

        private void hddToggleButton128_Unchecked(object sender, RoutedEventArgs e)
        {
            svm.DeselectStorage();
            PriceButtonPanel.Visibility = Visibility.Collapsed;
        }

        private void hddToggleButton256_Checked(object sender, RoutedEventArgs e)
        {
            hddToggleButton128.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            PriceButtonPanel.Visibility = Visibility.Visible;
            svm.SelectStorage("256");
        }

        private void hddToggleButton256_Unchecked(object sender, RoutedEventArgs e)
        {
            svm.DeselectStorage();
            PriceButtonPanel.Visibility = Visibility.Collapsed;
        }

        private void hddToggleButton512_Checked(object sender, RoutedEventArgs e)
        {
            hddToggleButton256.IsChecked = false;
            hddToggleButton128.IsChecked = false;
            PriceButtonPanel.Visibility = Visibility.Visible;
            svm.SelectStorage("512");
        }

        private void hddToggleButton512_Unchecked(object sender, RoutedEventArgs e)
        {
            svm.DeselectStorage();
            PriceButtonPanel.Visibility = Visibility.Collapsed;
        }

        private async void addSPToSolutionButton_Click(object sender, RoutedEventArgs e)
        {
            await svm.AddProductsToSolution();
            CompleteBladeView.IsEnabled = true;
            if (SolutionHelper.BusinessCustomer())
            {
                commercialCompleteBlade.IsOpen = true;
                SurfaceSelectionStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                consumerCompleteBlade.IsOpen = true;
                SurfaceSelectionStackPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void ResetPage()
        {
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
            hddToggleButton128.IsChecked = false;
            hddToggleButton256.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            GPUNoToggleButton.IsChecked = false;
            GPUYesToggleButton.IsChecked = false;
            GPUNoToggleButton.Visibility = Visibility.Collapsed;
            GPUYesToggleButton.Visibility = Visibility.Collapsed;
            StoragePane.Visibility = Visibility.Collapsed;
            MemoryPane.Visibility = Visibility.Collapsed;
            GPUPanel.Visibility = Visibility.Collapsed;
            PriceButtonPanel.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.Visibility = Visibility.Collapsed;
            ramToggleButton8gb.Visibility = Visibility.Collapsed;
            hddToggleButton128.Visibility = Visibility.Collapsed;
            hddToggleButton256.Visibility = Visibility.Collapsed;
            hddToggleButton512.Visibility = Visibility.Collapsed;
            

        }

        private void UncheckGPU()
        {
            MemoryPane.Visibility = Visibility.Collapsed;
            ramToggleButton8gb.IsChecked = false;
            ramToggleButton16gb.IsChecked = false;

        }

        private void UncheckRAM()
        {
            StoragePane.Visibility = Visibility.Collapsed;
            hddToggleButton128.IsChecked = false;
            hddToggleButton256.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            svm.DeselectRam();
        }
        private void ProcessorChecked()
        {
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
        }

        private async void consumerCompleteDontAddToSolution_Click(object sender, RoutedEventArgs e)
        {

            NavigateToNextPage(await svm.AddCompleteToSolution("None"));
        }

        private async void commercialCompleteDontAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            NavigateToNextPage(await svm.AddCompleteToSolution("None"));
        }


        private void CloseCompleteBlades()
        {
            consumerCompleteBlade.IsOpen = false;
            commercialCompleteBlade.IsOpen = false;
            CompleteBladeView.IsEnabled = false;
        }

        private async void consumerCompleteAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            NavigateToNextPage(await svm.AddCompleteToSolution("Consumer"));
        }

        

        private async void commercialCompleteAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxTermLength.SelectedItem == comboBoxComComlete3Yr)
            {
                NavigateToNextPage(await svm.AddCompleteToSolution("Commercial3"));
            }
            if (comboBoxTermLength.SelectedItem == comboBoxComComlete4Yr)
            {
                NavigateToNextPage(await svm.AddCompleteToSolution("Commercial4")); ;
            }
            else
            {
                MessageDialog noTerm = new MessageDialog("Please Select a term length.");
                await noTerm.ShowAsync();
            }
        }
        private void NavigateToNextPage(bool addOffice)
        {
            CloseCompleteBlades();
            if (addOffice)
            {
                this.Frame.Navigate(typeof(OfficeView));
            }
            else
            {
                this.Frame.Navigate(typeof(SelectSleeveView));
            }
        }
    }
}

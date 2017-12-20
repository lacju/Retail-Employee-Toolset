using retailemployeetoolset.Common;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SurfaceLaptopView : Page
    {
        SurfaceViewModelBase svm;
        public SurfaceLaptopView()
        {
            SolutionHelper.CurrentSolutionStage = GuidedSolutionStages.DeviceConfiguration;
            SolutionHelper.CurrentSolutionDevice = GuidedSolutionDevices.SurfaceLaptop;
            svm = new SurfaceViewModelBase();
            this.InitializeComponent();
            this.DataContext = svm;
            ResetPage();
        }

        private void procToggleButtoni5_Checked(object sender, RoutedEventArgs e)
        {
            ResetPage();
            procToggleButtoni7.IsChecked = false;
            memoryTitle.Visibility = Visibility.Visible;
            ramToggleButton4gb.Visibility = Visibility.Visible;
            ramToggleButton8gb.Visibility = Visibility.Visible;
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
            memoryTitle.Visibility = Visibility.Visible;
            ramToggleButton16gb.Visibility = Visibility.Visible;
            ramToggleButton8gb.Visibility = Visibility.Visible;
            svm.SelectProcessor("i7");
        }

        private void procToggleButtoni7_Unchecked(object sender, RoutedEventArgs e)
        {
            svm.DeselectProcessor();
            ResetPage();
        }

        private void ramToggleButton4gb_Checked(object sender, RoutedEventArgs e)
        {
            ramToggleButton8gb.IsChecked = false;
            ramToggleButton16gb.IsChecked = false;
            storageTitle.Visibility = Visibility.Visible;
            hddToggleButton128.Visibility = Visibility.Visible;
            svm.SelectRam("4");
        }

        private void ramToggleButton4gb_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckRAM();
            svm.DeselectRam();
        }

        private void ramToggleButton8gb_Checked(object sender, RoutedEventArgs e)
        {
            ramToggleButton4gb.IsChecked = false;
            ramToggleButton16gb.IsChecked = false;
            storageTitle.Visibility = Visibility.Visible;
            hddToggleButton256.Visibility = Visibility.Visible;
            svm.SelectRam("8");
        }

        private void ramToggleButton8gb_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckRAM();
            svm.DeselectRam();
        }

        private void ramToggleButton16gb_Checked(object sender, RoutedEventArgs e)
        {
            ramToggleButton4gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
            storageTitle.Visibility = Visibility.Visible;
            hddToggleButton512.Visibility = Visibility.Visible;
            svm.SelectRam("16");
        }

        private void ramToggleButton16gb_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckRAM();
            svm.DeselectRam();
        }

        private void hddToggleButton128_Checked(object sender, RoutedEventArgs e)
        {
            hddToggleButton256.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            ColorStackpanel.Visibility = Visibility.Visible;
            kbToggleButtonPlatinum.Visibility = Visibility.Visible;
            svm.SelectStorage("128");

        }

        private void hddToggleButton128_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckHDD();
            svm.DeselectStorage();
        }

        private void hddToggleButton256_Checked(object sender, RoutedEventArgs e)
        {
            if (procToggleButtoni5.IsChecked == true)
            {
                kbToggleButtonPlatinum.IsChecked = false;
                kbToggleButtonGold.IsChecked = false;
                colorToggleButtonCobalt.IsChecked = false;
                colorToggleButtonBurgandy.IsChecked = false;

                ColorStackpanel.Visibility = Visibility.Visible;
                kbToggleButtonPlatinum.Visibility = Visibility.Visible;
                kbToggleButtonGold.Visibility = Visibility.Visible;
                colorToggleButtonCobalt.Visibility = Visibility.Visible;
                colorToggleButtonBurgandy.Visibility = Visibility.Visible;
            }
            else
            {
                kbToggleButtonPlatinum.IsChecked = false;
                ColorStackpanel.Visibility = Visibility.Visible;
                kbToggleButtonPlatinum.Visibility = Visibility.Visible;
            }
            svm.SelectStorage("256");
        }

        private void hddToggleButton256_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckHDD();
            svm.DeselectStorage();
        }

        private void hddToggleButton512_Checked(object sender, RoutedEventArgs e)
        {
            kbToggleButtonPlatinum.IsChecked = false;
            kbToggleButtonGold.IsChecked = false;
            colorToggleButtonCobalt.IsChecked = false;
            colorToggleButtonBurgandy.IsChecked = false;
            ColorStackpanel.Visibility = Visibility.Visible;
            kbToggleButtonPlatinum.Visibility = Visibility.Visible;
            kbToggleButtonGold.Visibility = Visibility.Visible;
            colorToggleButtonCobalt.Visibility = Visibility.Visible;
            colorToggleButtonBurgandy.Visibility = Visibility.Visible;
            svm.SelectStorage("512");
        }

        private void hddToggleButton512_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckHDD();
            svm.DeselectStorage();
        }

        private void colorToggleButtonBurgandy_Checked(object sender, RoutedEventArgs e)
        {
            kbToggleButtonPlatinum.IsChecked = false;
            kbToggleButtonGold.IsChecked = false;
            colorToggleButtonCobalt.IsChecked = false;
            svm.SelectColor("Burgundy");
            pricePanel.Visibility = Visibility.Visible;
            addSPToSolutionButton.Visibility = Visibility.Visible;
        }

        private void colorToggleButtonBurgandy_Unchecked(object sender, RoutedEventArgs e)
        {
            svm.DeselectColor();
            pricePanel.Visibility = Visibility.Collapsed;
        }

        private void colorToggleButtonCobalt_Checked(object sender, RoutedEventArgs e)
        {
            kbToggleButtonPlatinum.IsChecked = false;
            kbToggleButtonGold.IsChecked = false;
            colorToggleButtonBurgandy.IsChecked = false;
            svm.SelectColor("Cobalt");
            pricePanel.Visibility = Visibility.Visible;
            addSPToSolutionButton.Visibility = Visibility.Visible;
        }

        private void colorToggleButtonCobalt_Unchecked(object sender, RoutedEventArgs e)
        {
            svm.DeselectColor();
            pricePanel.Visibility = Visibility.Collapsed;
        }

        private void kbToggleButtonPlatinum_Checked(object sender, RoutedEventArgs e)
        {
            kbToggleButtonPlatinum.IsChecked = false;
            colorToggleButtonCobalt.IsChecked = false;
            colorToggleButtonBurgandy.IsChecked = false;
            svm.SelectColor("Gold");
            pricePanel.Visibility = Visibility.Visible;
            addSPToSolutionButton.Visibility = Visibility.Visible;
        }

        private void kbToggleButtonPlatinum_Unchecked(object sender, RoutedEventArgs e)
        {
            svm.DeselectColor();
            pricePanel.Visibility = Visibility.Collapsed;
        }

        private void kbToggleButtonSilver_Checked(object sender, RoutedEventArgs e)
        {
            kbToggleButtonGold.IsChecked = false;
            colorToggleButtonCobalt.IsChecked = false;
            colorToggleButtonBurgandy.IsChecked = false;
            svm.SelectColor("Platinum");
            pricePanel.Visibility = Visibility.Visible;
            addSPToSolutionButton.Visibility = Visibility.Visible;
        }

        private void kbToggleButtonSilver_Unchecked(object sender, RoutedEventArgs e)
        {
            svm.DeselectColor();
            pricePanel.Visibility = Visibility.Collapsed;
        }

        private async void addSPToSolutionButton_Click(object sender, RoutedEventArgs e)
        {
            await svm.AddProductsToSolution();
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

        private void UncheckRAM()
        {
            storageTitle.Visibility = Visibility.Collapsed;
            hddToggleButton128.Visibility = Visibility.Collapsed;
            hddToggleButton256.Visibility = Visibility.Collapsed;
            hddToggleButton512.Visibility = Visibility.Collapsed;
            hddToggleButton128.IsChecked = false;
            hddToggleButton256.IsChecked = false;
            hddToggleButton512.IsChecked = false;
        }
        private void UncheckHDD()
        {
            kbToggleButtonPlatinum.IsChecked = false;
            kbToggleButtonGold.IsChecked = false;
            colorToggleButtonCobalt.IsChecked = false;
            colorToggleButtonBurgandy.IsChecked = false;
            ColorStackpanel.Visibility = Visibility.Collapsed;
            kbToggleButtonPlatinum.Visibility = Visibility.Collapsed;
            kbToggleButtonGold.Visibility = Visibility.Collapsed;
            colorToggleButtonCobalt.Visibility = Visibility.Collapsed;
            colorToggleButtonBurgandy.Visibility = Visibility.Collapsed;
        }
        private void ResetPage()
        {
            ColorStackpanel.Visibility = Visibility.Collapsed;
            storageTitle.Visibility = Visibility.Collapsed;
            memoryTitle.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
            ramToggleButton4gb.IsChecked = false;
            hddToggleButton128.IsChecked = false;
            hddToggleButton256.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            pricePanel.Visibility = Visibility.Collapsed;
            addSPToSolutionButton.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.Visibility = Visibility.Collapsed;
            ramToggleButton8gb.Visibility = Visibility.Collapsed;
            ramToggleButton4gb.Visibility = Visibility.Collapsed;
            hddToggleButton128.Visibility = Visibility.Collapsed;
            hddToggleButton256.Visibility = Visibility.Collapsed;
            hddToggleButton512.Visibility = Visibility.Collapsed;
            kbToggleButtonPlatinum.IsChecked = false;
            kbToggleButtonGold.IsChecked = false;
            colorToggleButtonCobalt.IsChecked = false;
            colorToggleButtonBurgandy.IsChecked = false;
            kbToggleButtonPlatinum.Visibility = Visibility.Collapsed;
            kbToggleButtonGold.Visibility = Visibility.Collapsed;
            colorToggleButtonCobalt.Visibility = Visibility.Collapsed;
            colorToggleButtonBurgandy.Visibility = Visibility.Collapsed;


        }

        private async void consumerCompleteDontAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            NavigateToNextPage(await svm.AddCompleteToSolution("None"));
        }

        private async void commercialCompleteDontAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            NavigateToNextPage(await svm.AddCompleteToSolution("None"));
        }

        private async void commercialCompleteAddToSolution_Click(object sender, RoutedEventArgs e)
        {

            if (comboBoxTermLength.SelectedItem == comboBoxComComlete3Yr)
            {
                NavigateToNextPage(await svm.AddCompleteToSolution("Commercial3"));
            }
            if (comboBoxTermLength.SelectedItem == comboBoxComComlete4Yr)
            {
                NavigateToNextPage(await svm.AddCompleteToSolution("Commercial4"));
            }
            else
            {
                MessageDialog noTerm = new MessageDialog("Please Select a term length.");
                await noTerm.ShowAsync();
            }
        }

        private async void consumerCompleteAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            NavigateToNextPage(await svm.AddCompleteToSolution("Consumer"));
        }

        private void CloseCompleteBlades()
        {
            consumerCompleteBlade.IsOpen = false;
            commercialCompleteBlade.IsOpen = false;
            CompleteBladeView.IsEnabled = false;
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

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
    public sealed partial class SurfaceProView : Page
    {
        private SurfaceViewModelBase svm;


        public SurfaceProView()
        {
            SolutionHelper.CurrentSolutionStage = GuidedSolutionStages.DeviceConfiguration;
            SolutionHelper.CurrentSolutionDevice = GuidedSolutionDevices.SurfacePro;
            svm = new SurfaceViewModelBase();
            this.InitializeComponent();
            this.DataContext = svm;
            ProcessorUnchecked();


        }

        private void procToggleButtonM3_Checked(object sender, RoutedEventArgs e)
        {

            procToggleButtoni5.IsChecked = false;
            procToggleButtoni7.IsChecked = false;            
            hddToggleButton128.Visibility = Visibility.Visible;
            svm.SelectProcessor("m3");
            ProcessorChecked();
        }
        private void procToggleButtoni5_Checked(object sender, RoutedEventArgs e)
        {
            procToggleButtonM3.IsChecked = false;
            procToggleButtoni7.IsChecked = false;          
            hddToggleButton128.Visibility = Visibility.Visible;
            hddToggleButton256.Visibility = Visibility.Visible;
            hddToggleButton512.Visibility = Visibility.Collapsed;
            hddToggleButton1TB.Visibility = Visibility.Collapsed;
            svm.SelectProcessor("i5");
            ProcessorChecked();
        }
        private void procToggleButtoni7_Checked(object sender, RoutedEventArgs e)
        {

            procToggleButtonM3.IsChecked = false;
            procToggleButtoni5.IsChecked = false;
            hddToggleButton128.Visibility = Visibility.Collapsed;
            hddToggleButton256.Visibility = Visibility.Visible;
            hddToggleButton512.Visibility = Visibility.Visible;
            hddToggleButton1TB.Visibility = Visibility.Visible;
            svm.SelectProcessor("i7");
            ProcessorChecked();
        }
        private void hddToggleButton128_Checked(object sender, RoutedEventArgs e)
        {

            hddToggleButton256.IsChecked = false;
            memoryTitle.Visibility = Visibility.Visible;
            ramToggleButton4gb.Visibility = Visibility.Visible;
            ramToggleButton8gb.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.Visibility = Visibility.Collapsed;
            svm.SelectStorage("128");            
        }
        private void hddToggleButton256_Checked(object sender, RoutedEventArgs e)
        {
            hddToggleButton128.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            hddToggleButton1TB.IsChecked = false;
            memoryTitle.Visibility = Visibility.Visible;
            ramToggleButton4gb.Visibility = Visibility.Collapsed;
            ramToggleButton8gb.Visibility = Visibility.Visible;
            if (procToggleButtoni5.IsChecked != true)
            {
                ramToggleButton16gb.Visibility = Visibility.Visible;
            }
            svm.SelectStorage("256");
        }
        private void hddToggleButton512_Checked(object sender, RoutedEventArgs e)
        {
            hddToggleButton256.IsChecked = false;
            hddToggleButton1TB.IsChecked = false;
            memoryTitle.Visibility = Visibility.Visible;
            ramToggleButton4gb.Visibility = Visibility.Collapsed;
            ramToggleButton8gb.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.Visibility = Visibility.Visible;
            svm.SelectStorage("512");
        }
        private void hddToggleButton1TB_Checked(object sender, RoutedEventArgs e)
        {
            hddToggleButton256.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            hddToggleButton128.IsChecked = false;
            memoryTitle.Visibility = Visibility.Visible;
            ramToggleButton4gb.Visibility = Visibility.Collapsed;
            ramToggleButton8gb.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.Visibility = Visibility.Visible;
            svm.SelectStorage("1TB");
        }
        private void ramToggleButton4gb_Checked(object sender, RoutedEventArgs e)
        {
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
            hddToggleButton256.Visibility = Visibility.Collapsed;
            hddToggleButton512.Visibility = Visibility.Collapsed;
            hddToggleButton1TB.Visibility = Visibility.Collapsed;
            svm.SelectRam("4");
            RAMChecked();
        }
        private void ramToggleButton8gb_Checked(object sender, RoutedEventArgs e)
        {
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton4gb.IsChecked = false;
            hddToggleButton128.Visibility = Visibility.Collapsed;
            hddToggleButton512.Visibility = Visibility.Collapsed;
            hddToggleButton1TB.Visibility = Visibility.Collapsed;
            svm.SelectRam("8");
            RAMChecked();
        }
        private void ramToggleButton16gb_Checked(object sender, RoutedEventArgs e)
        {
            ramToggleButton4gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
            hddToggleButton128.Visibility = Visibility.Collapsed;
            svm.SelectRam("16");
            RAMChecked();
        }
        private void procToggleButtonM3_Unchecked(object sender, RoutedEventArgs e)
        {
            procToggleButtonM3.IsChecked = false;
            ProcessorUnchecked();
        }
        private void procToggleButtoni5_Unchecked(object sender, RoutedEventArgs e)
        {
            procToggleButtoni5.IsChecked = false;
            ProcessorUnchecked();
        }
        private void procToggleButtoni7_Unchecked(object sender, RoutedEventArgs e)
        {
            procToggleButtoni7.IsChecked = false;
            ProcessorUnchecked();
        }    
        private void hddToggleButton256_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckHDD();
        }
        private void hddToggleButton512_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckHDD();
        }
        private void hddToggleButton1TB_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckHDD();
        }
        private void ramToggleButton4gb_Unchecked(object sender, RoutedEventArgs e)
        {
            if (procToggleButtonM3.IsChecked == false && (procToggleButtoni5.IsChecked == true || procToggleButtoni7.IsChecked == true))
            {
                hddToggleButton256.Visibility = Visibility.Visible;
            }
            UncheckRAM();
        }
        private void ramToggleButton8gb_Unchecked(object sender, RoutedEventArgs e)
        {
            if (procToggleButtoni5.IsChecked == true)
            {
                hddToggleButton128.Visibility = Visibility.Visible;
            }
            else if (procToggleButtoni7.IsChecked == true)
            {
                hddToggleButton256.Visibility = Visibility.Collapsed;
                hddToggleButton512.Visibility = Visibility.Collapsed;
                hddToggleButton1TB.Visibility = Visibility.Collapsed;
            }
            UncheckRAM();
        }
        private void ramToggleButton16gb_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckRAM();
        }
        private void hddToggleButton128_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckHDD();           
        }
        private async void addSPToSolutionButton_Click(object sender, RoutedEventArgs e) //Navigate to the next step in the process if the method returns true in dicating the customer does not want to add an accessory
        {
            if (await svm.AddProductsToSolution())
            {
                CompleteBladeView.IsEnabled = true;
                if(SolutionHelper.BusinessCustomer())
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
                NavigateToNextPage(await svm.AddCompleteToSolution("Commercial4"));
            }
            else
            {
                MessageDialog noTerm = new MessageDialog("Please Select a term length.");
                await noTerm.ShowAsync();
            }
        }
        private void kbToggleButtonAlcantara_Checked(object sender, RoutedEventArgs e)
        {
            KeyboardChecked("Alcantara");
        }
        private void kbToggleButtonRed_Checked(object sender, RoutedEventArgs e)
        {
            KeyboardChecked("Red");
        }
        private void kbToggleButtonTurquoise_Checked(object sender, RoutedEventArgs e)
        {
            KeyboardChecked("Turquoise");
        }
        private void kbToggleButtonCyan_Checked(object sender, RoutedEventArgs e)
        {
            KeyboardChecked("Cyan");
        }
        private void kbToggleButtonBlue_Checked(object sender, RoutedEventArgs e)
        {
            KeyboardChecked("Blue");
        }
        private void kbToggleButtonBlack_Checked(object sender, RoutedEventArgs e)
        {
            KeyboardChecked("Black");
        }
        private void kbToggleButtonFingerPrint_Checked(object sender, RoutedEventArgs e)
        {
            KeyboardChecked("Fingerprint");
        }
        private void kbToggleButtonAlcantara_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyboardUnchecked();
        }
        private void kbToggleButtonRed_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyboardUnchecked();
        }
        private void kbToggleButtonTurquoise_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyboardUnchecked();
        }
        private void kbToggleButtonCyan_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyboardUnchecked();
        }
        private void kbToggleButtonBlue_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyboardUnchecked();
        }
        private void kbToggleButtonBlack_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyboardUnchecked();
        }
        private void kbToggleButtonFingerPrint_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyboardUnchecked();
        }
        private void UncheckRAM()
        {
            pricePanel.Visibility = Visibility.Collapsed;
            keyboardStackpanel.Visibility = Visibility.Collapsed;
            addSPToSolutionButton.Visibility = Visibility.Collapsed;
            svm.DeselectRam();
        }
        private void RAMChecked()
        {
            keyboardStackpanel.Visibility = Visibility.Visible;
            pricePanel.Visibility = Visibility.Visible;
            addSPToSolutionButton.Visibility = Visibility.Visible;
            pricePanel.Visibility = Visibility.Visible;
        }
        private void UncheckHDD()
        {
            ramToggleButton8gb.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.Visibility = Visibility.Collapsed;
            ramToggleButton4gb.Visibility = Visibility.Collapsed;
            memoryTitle.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton4gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
            svm.DeselectStorage();
        }
        private void ProcessorUnchecked()
        {
            hddToggleButton256.Visibility = Visibility.Collapsed;
            hddToggleButton512.Visibility = Visibility.Collapsed;
            hddToggleButton1TB.Visibility = Visibility.Collapsed;
            ramToggleButton8gb.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.Visibility = Visibility.Collapsed;
            ramToggleButton4gb.Visibility = Visibility.Collapsed;
            hddToggleButton128.Visibility = Visibility.Collapsed;
            storageTitle.Visibility = Visibility.Collapsed;
            memoryTitle.Visibility = Visibility.Collapsed;
            keyboardStackpanel.Visibility = Visibility.Collapsed;
            pricePanel.Visibility = Visibility.Collapsed;
            addSPToSolutionButton.Visibility = Visibility.Collapsed;
            hddToggleButton128.IsChecked = false;
            hddToggleButton256.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            hddToggleButton1TB.IsChecked = false;
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton4gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
            svm.DeselectProcessor();
        }
        private void ProcessorChecked()
        {
            hddToggleButton128.IsChecked = false;
            hddToggleButton256.IsChecked = false;
            hddToggleButton512.IsChecked = false;
            hddToggleButton1TB.IsChecked = false;
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton4gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
            storageTitle.Visibility = Visibility.Visible;
        }
        private void KeyboardChecked(string color)
        {
            svm.AccessorySelected();
            switch (color)
            {
                case "Red":
                    svm.SelectColor(color);
                    kbToggleButtonAlcantara.IsChecked = false;
                    kbToggleButtonBlue.IsChecked = false;
                    kbToggleButtonCyan.IsChecked = false;
                    kbToggleButtonTurquoise.IsChecked = false;
                    kbToggleButtonBlack.IsChecked = false;
                    kbToggleButtonFingerPrint.IsChecked = false;
                    break;

                case "Cyan":
                    svm.SelectColor(color);
                    kbToggleButtonRed.IsChecked = false;
                    kbToggleButtonBlue.IsChecked = false;
                    kbToggleButtonAlcantara.IsChecked = false;
                    kbToggleButtonTurquoise.IsChecked = false;
                    kbToggleButtonBlack.IsChecked = false;
                    kbToggleButtonFingerPrint.IsChecked = false;
                    break;

                case "Blue":
                    svm.SelectColor(color);
                    kbToggleButtonRed.IsChecked = false;
                    kbToggleButtonAlcantara.IsChecked = false;
                    kbToggleButtonCyan.IsChecked = false;
                    kbToggleButtonTurquoise.IsChecked = false;
                    kbToggleButtonBlack.IsChecked = false;
                    kbToggleButtonFingerPrint.IsChecked = false;
                    break;

                case "Alcantara":
                    svm.SelectColor(color);
                    kbToggleButtonRed.IsChecked = false;
                    kbToggleButtonBlue.IsChecked = false;
                    kbToggleButtonCyan.IsChecked = false;
                    kbToggleButtonTurquoise.IsChecked = false;
                    kbToggleButtonBlack.IsChecked = false;
                    kbToggleButtonFingerPrint.IsChecked = false;
                    break;

                case "Black":
                    svm.SelectColor(color);
                    kbToggleButtonRed.IsChecked = false;
                    kbToggleButtonBlue.IsChecked = false;
                    kbToggleButtonCyan.IsChecked = false;
                    kbToggleButtonTurquoise.IsChecked = false;
                    kbToggleButtonAlcantara.IsChecked = false;
                    kbToggleButtonFingerPrint.IsChecked = false;
                    break;

                case "Turquoise":
                    svm.SelectColor(color);
                    kbToggleButtonRed.IsChecked = false;
                    kbToggleButtonBlue.IsChecked = false;
                    kbToggleButtonCyan.IsChecked = false;
                    kbToggleButtonAlcantara.IsChecked = false;
                    kbToggleButtonBlack.IsChecked = false;
                    kbToggleButtonFingerPrint.IsChecked = false;
                    break;

                case "FingerPrint":
                    svm.SelectColor(color);
                    kbToggleButtonRed.IsChecked = false;
                    kbToggleButtonBlue.IsChecked = false;
                    kbToggleButtonCyan.IsChecked = false;
                    kbToggleButtonTurquoise.IsChecked = false;
                    kbToggleButtonBlack.IsChecked = false;
                    kbToggleButtonAlcantara.IsChecked = false;
                    break;

            }
        }
        private void KeyboardUnchecked()
        {
            kbToggleButtonAlcantara.IsChecked = false;
            kbToggleButtonRed.IsChecked = false;
            kbToggleButtonTurquoise.IsChecked = false;
            kbToggleButtonCyan.IsChecked = false;
            kbToggleButtonBlue.IsChecked = false;
            kbToggleButtonFingerPrint.IsChecked = false;
            kbToggleButtonBlack.IsChecked = false;
            svm.AccessoryDeselected();
            svm.DeselectColor();
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

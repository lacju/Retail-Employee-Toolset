using retailemployeetoolset.Common;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SurfaceStudioView : Page
    {
        public SurfaceViewModelBase svm;

        public SurfaceStudioView()
        {
            SolutionHelper.CurrentSolutionStage = GuidedSolutionStages.DeviceConfiguration;
            SolutionHelper.CurrentSolutionDevice = GuidedSolutionDevices.SurfaceStudio;
            svm = new SurfaceViewModelBase();
            this.InitializeComponent();
            this.DataContext = svm;
            CollapseRequiredElementsOnStart();
        }

        private void procToggleButtoni5_Checked(object sender, RoutedEventArgs e)
        {
            procToggleButtoni7.IsChecked = false;
            memoryTitle.Visibility = Visibility.Visible;
            ramToggleButton8gb.Visibility = Visibility.Visible;            
            svm.SelectProcessor("i5");
        }

        private void procToggleButtoni5_Unchecked(object sender, RoutedEventArgs e)
        {
            procToggleButtoni5.IsChecked = false;
            ProcessorUnchecked();
            svm.DeselectProcessor();
        }

        private void procToggleButtoni7_Checked(object sender, RoutedEventArgs e)
        {
            procToggleButtoni5.IsChecked = false;
            memoryTitle.Visibility = Visibility.Visible;
            ramToggleButton16gb.Visibility = Visibility.Visible;
            ramToggleButton32gb.Visibility = Visibility.Visible;
            svm.SelectProcessor("i7");

        }

        private void procToggleButtoni7_Unchecked(object sender, RoutedEventArgs e)
        {
            procToggleButtoni7.IsChecked = false;
            ProcessorUnchecked();
            svm.DeselectProcessor();
        }

        private void ramToggleButton8gb_Checked(object sender, RoutedEventArgs e)
        {
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton32gb.IsChecked = false;
            GPUTitle.Visibility = Visibility.Visible;
            ramToggleButton8gb.IsChecked = true;
            GPUMemoryToggleButton2gb.Visibility = Visibility.Visible;
            svm.SelectRam("8GB");

        }

        private void ramToggleButton8gb_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckRAM();
            svm.DeselectRam();
        }

        private void ramToggleButton16gb_Checked(object sender, RoutedEventArgs e)
        {
            GPUMemoryToggleButton4gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
            ramToggleButton32gb.IsChecked = false;
            GPUTitle.Visibility = Visibility.Visible;
            GPUMemoryToggleButton2gb.Visibility = Visibility.Visible;
            GPUMemoryToggleButton4gb.Visibility = Visibility.Collapsed;
            svm.SelectRam("16GB");
        }

        private void ramToggleButton16gb_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckRAM();
            svm.DeselectRam();
        }

        private void ramToggleButton32gb_Checked(object sender, RoutedEventArgs e)
        {
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
            GPUTitle.Visibility = Visibility.Visible;
            GPUMemoryToggleButton4gb.Visibility = Visibility.Visible;        
            svm.SelectRam("32GB");
        }

        private void ramToggleButton32gb_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckRAM();
            svm.DeselectRam();
        }

        private void GPUMemoryToggleButton2gb_Checked(object sender, RoutedEventArgs e)
        {
            GPUChecked("2GB");
            GPUMemoryToggleButton4gb.IsChecked = false;
        }

        private void GPUMemoryToggleButton2gb_Unchecked(object sender, RoutedEventArgs e)
        {
            GPUUnchecked("2GB");
        }

        private void GPUMemoryToggleButton4gb_Checked(object sender, RoutedEventArgs e)
        {
            GPUMemoryToggleButton2gb.IsChecked = false;
            GPUChecked("4GB");
        }

        private void GPUMemoryToggleButton4gb_Unchecked(object sender, RoutedEventArgs e)
        {
            GPUUnchecked("4GB");
        }

        private void AddSurfaceDial_Checked(object sender, RoutedEventArgs e)
        {
          svm.AccessorySelected();
        }

        private void AddSurfaceDial_Unchecked(object sender, RoutedEventArgs e)
        {
           svm.AccessoryDeselected();
        }

        private async void addSPToSolutionButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (await svm.AddProductsToSolution()) //Navigate to the next step in the process if the method returns true in dicating the customer does not want to add an accessory
            {
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
        }

        private void UncheckRAM()
        {
            GPUTitle.Visibility = Visibility.Collapsed;
            GPUMemoryToggleButton2gb.IsChecked = false;
            GPUMemoryToggleButton4gb.IsChecked = false;
            GPUMemoryToggleButton2gb.Visibility = Visibility.Collapsed;
            GPUMemoryToggleButton4gb.Visibility = Visibility.Collapsed;
            addSPToSolutionButton.Visibility = Visibility.Collapsed;
        }
        private void ProcessorUnchecked()
        {

            ramToggleButton8gb.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.Visibility = Visibility.Collapsed;
            ramToggleButton32gb.Visibility = Visibility.Collapsed;
            memoryTitle.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
            ramToggleButton32gb.IsChecked = false;
            GPUTitle.Visibility = Visibility.Collapsed;
            GPUMemoryToggleButton2gb.Visibility = Visibility.Collapsed;
            GPUMemoryToggleButton4gb.Visibility = Visibility.Collapsed;
            SurfaceDialTitle.Visibility = Visibility.Collapsed;
            AddSurfaceDial.Visibility = Visibility.Collapsed;
            pricePanel.Visibility = Visibility.Collapsed;
        }
        private void ProcessorChecked()
        {
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
        }
        private void GPUChecked(string capacity)
        {
            SurfaceDialTitle.Visibility = Visibility.Visible;
            AddSurfaceDial.Visibility = Visibility.Visible;
            pricePanel.Visibility = Visibility.Visible;
            addSPToSolutionButton.Visibility = Visibility.Visible;
            svm.SelectGPU(capacity);
        }
        private void GPUUnchecked(string capacity)
        {
            SurfaceDialTitle.Visibility = Visibility.Collapsed;
            AddSurfaceDial.IsChecked = false;
            AddSurfaceDial.Visibility = Visibility.Collapsed;
            pricePanel.Visibility = Visibility.Collapsed;
            addSPToSolutionButton.Visibility = Visibility.Collapsed;
            svm.DeselectGPU();
        }
        private void CollapseRequiredElementsOnStart()
        {
            ramToggleButton8gb.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.Visibility = Visibility.Collapsed;
            ramToggleButton32gb.Visibility = Visibility.Collapsed;
            memoryTitle.Visibility = Visibility.Collapsed;
            ramToggleButton16gb.IsChecked = false;
            ramToggleButton8gb.IsChecked = false;
            ramToggleButton32gb.IsChecked = false;
            GPUTitle.Visibility = Visibility.Collapsed;
            GPUMemoryToggleButton2gb.Visibility = Visibility.Collapsed;
            GPUMemoryToggleButton4gb.Visibility = Visibility.Collapsed;
            SurfaceDialTitle.Visibility = Visibility.Collapsed;
            AddSurfaceDial.Visibility = Visibility.Collapsed;
            pricePanel.Visibility = Visibility.Collapsed;
            addSPToSolutionButton.Visibility = Visibility.Collapsed;
            procToggleButtoni5.IsChecked = false;
            procToggleButtoni7.IsChecked = false;
        }

        private async void consumerCompleteAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            NavigateToNextPage(await svm.AddCompleteToSolution("Consumer"));
        }

        private async void commercialCompleteAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            NavigateToNextPage(await svm.AddCompleteToSolution("Commercial3"));
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
                this.Frame.Navigate(typeof(SolutionView));
            }
        }
    }
}

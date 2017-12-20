using retailemployeetoolset.Common;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OfficeView : Page
    {
        private OfficeViewModel ovm;

        public OfficeView()
        {
            this.InitializeComponent();
            ovm = new OfficeViewModel();
            SolutionHelper.CurrentSolutionStage = GuidedSolutionStages.AddOffice;
        }


        private void office365AddToSolution_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxHomePersonal.SelectedItem == comboBoxItemOfficeHome)
            {
                ovm.AddOffice365ToSolution("Home", comboBoxSubLength.SelectedIndex + 1);
                NavigateToNextPage();
            }
            else
            {
                ovm.AddOffice365ToSolution("Personal", comboBoxSubLength.SelectedIndex + 1);
                NavigateToNextPage();
            }
        }

        private void officeHBAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            ovm.AddPerpetualOfficeToSolution("Business");
            NavigateToNextPage();
        }

        private void officeHSAddToSolution_Click(object sender, RoutedEventArgs e)
        {
            ovm.AddPerpetualOfficeToSolution("Student");
            NavigateToNextPage();
        }

        private void NavigateToNextPage()
        {
            if (SolutionHelper.CurrentSolutionDevice == GuidedSolutionDevices.SurfaceBook || SolutionHelper.CurrentSolutionDevice == GuidedSolutionDevices.SurfaceLaptop || SolutionHelper.CurrentSolutionDevice == GuidedSolutionDevices.SurfacePro)
            {
                this.Frame.Navigate(typeof(SelectSleeveView));
            }
            else
            {
                this.Frame.Navigate(typeof(SolutionView));
            }
        }
    }
}

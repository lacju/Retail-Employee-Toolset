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
using retailemployeetoolset.Common;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SurfaceSelectedView : Page
    {
        public SurfaceSelectedView()
        {
            this.InitializeComponent();
            SolutionHelper.CurrentSolutionStage = GuidedSolutionStages.SelectDeviceSurface;
        }



        private void _surfacePageSP4Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.SurfaceProView));
            SolutionHelper.CurrentSolutionDevice = GuidedSolutionDevices.SurfacePro;
        }

        private void _surfacePageSurfaceStudioButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.SurfaceStudioView));
            SolutionHelper.CurrentSolutionDevice = GuidedSolutionDevices.SurfaceStudio;
        }

        private void _surfacePageSurfaceBookButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.SurfaceBookView));
            SolutionHelper.CurrentSolutionDevice = GuidedSolutionDevices.SurfaceBook;
        }

        private void _surfacePageSurfaceLaptopButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.SurfaceLaptopView));
            SolutionHelper.CurrentSolutionDevice = GuidedSolutionDevices.SurfaceLaptop;
        }
    }
}

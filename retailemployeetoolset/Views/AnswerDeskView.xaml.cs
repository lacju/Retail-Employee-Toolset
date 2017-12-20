using retailemployeetoolset.Models;
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
    public sealed partial class AnswerDeskView : Page
    {
        AnswerDeskViewModel advm;
        public AnswerDeskView()
        {
            advm = new AnswerDeskViewModel();
            this.InitializeComponent();
            this.DataContext = advm;
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            //advm.NewAppointment(AppointmentList.SelectedItem as Appointment, "AD");
        }

        private void CheckInAppointment_Click(object sender, RoutedEventArgs e)
        {
            //advm.CheckInAppointment(AppointmentList.SelectedItem as Appointment);
        }

        private void CompleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            //advm.CompleteAppointment(AppointmentList.SelectedItem as Appointment);
        }

        private void CancelAppointment_Click(object sender, RoutedEventArgs e)
        {
            //advm.CancelAppointment(AppointmentList.SelectedItem as Appointment);
        }

        private void DetailsAppointment_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SelectDate_Click(object sender, RoutedEventArgs e)
        {
            //advm.FilterByDate();
        }

        private void SelectToday_Click(object sender, RoutedEventArgs e)
        {
            //advm.FilterByDate(DateTime.Today);
        }
    }
}

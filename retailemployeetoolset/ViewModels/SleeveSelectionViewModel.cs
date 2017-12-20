using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using retailemployeetoolset.Models;
using retailemployeetoolset.Common;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using retailemployeetoolset.Views;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Views;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Messaging;

namespace retailemployeetoolset.ViewModels
{
    class SleeveSelectionViewModel : ViewModel
    {
        public ObservableCollection<Product> Sleeves { get; set; }
        public ICommand AddToSolutionCommand { get; set; }
        public ICommand GotoPage2Command { get; set; }
        public SleeveSelectionViewModel()
        {


            Sleeves = new ObservableCollection<Product>();           
            SolutionHelper.CurrentSolutionStage = GuidedSolutionStages.AddCase;

            switch (SolutionHelper.CurrentSolutionDevice)
            {
                case GuidedSolutionDevices.SurfacePro:
                    Products.ToList().ForEach(x => { if (x.SubCategory == "Surface Pro Case") { Sleeves.Add(x); } });
                    break;
                case GuidedSolutionDevices.SurfaceBook:
                    Products.ToList().ForEach(x => { if (x.SubCategory == "Surface Book Case") { Sleeves.Add(x); } });
                    break;
                case GuidedSolutionDevices.SurfaceLaptop:
                    Products.ToList().ForEach(x => { if (x.SubCategory == "Surface Book Case") { Sleeves.Add(x); } });
                    break;
            }

            AddToSolutionCommand = new RelayCommand<Product>(AddToSolution);
        }

        private void AddToSolution(Product item)
        {
            Product productToAdd = item as Product;
            SolutionHelper.AddProductToCurrentSolution(productToAdd);
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "CloseWindowsBoundToMe")); //Send message back to SelecSleeveView.xaml.cs to navigate to the SolutionView


        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using retailemployeetoolset.Common;
using retailemployeetoolset.Models;

namespace retailemployeetoolset.ViewModels
{
    public class XboxViewModel : ViewModel
    {
        public ObservableCollection<Product> XboxListToDisplay { get; set; }
        public Product XboxComplete { get; set; }
        public ICommand AddToSolutionCommand { get; set; }

        public XboxViewModel()
        {

            SolutionHelper.CurrentSolutionDevice = GuidedSolutionDevices.Xbox;
            SolutionHelper.CurrentSolutionStage = GuidedSolutionStages.SelectDeviceXbox;
            XboxListToDisplay = new ObservableCollection<Product>();
            Products.ToList().ForEach(x => { if(x.Category.Contains("Xbox") && x.SubCategory.Contains("Hardware")) { XboxListToDisplay.Add(x);}});
            XboxComplete =
                Products.ToList().Find(
                    x => x.Category.Contains("Complete") && x.SubCategory.Contains("Xbox"));
            AddToSolutionCommand = new RelayCommand<Product>(AddToSolution);
        }

        private async void AddToSolution(Product item)
        {
            Product productToAdd = item as Product;
            SolutionHelper.AddProductToCurrentSolution(productToAdd);

            if (SolutionHelper.CurrentSolutionStage == GuidedSolutionStages.SelectAccessoryXbox)
            {
               
                MessageDialog addAnotherAccy = new MessageDialog("Would you like to add another accessory?");
                addAnotherAccy.Commands.Add(new UICommand("Yes") { Id = 0 });
                addAnotherAccy.Commands.Add(new UICommand("No") { Id = 1 });
                var result = await addAnotherAccy.ShowAsync();
                if ((int)result.Id == 0)
                {
                    return;
                }
                else
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "MoveToSolutionView")); //Send message back to XboxView.xaml.cs to navigate to the solutionview
                    SolutionHelper.CreateNewSolution(new Customer());
                }
            }

            if (SolutionHelper.CurrentSolutionStage == GuidedSolutionStages.SelectGameXbox)
            {


                MessageDialog addAnotherGame = new MessageDialog("Would you like to add another game?");
                addAnotherGame.Commands.Add(new UICommand("Yes") { Id = 0 });
                addAnotherGame.Commands.Add(new UICommand("No") { Id = 1 });
                var result = await addAnotherGame.ShowAsync();
                switch ((int)result.Id)
                {
                    case 0:
                        return;
                    case 1:

                        MessageDialog addAnotherAccy = new MessageDialog("Would you like to add any accessories?");
                        addAnotherAccy.Commands.Add(new UICommand("Yes") { Id = 0 });
                        addAnotherAccy.Commands.Add(new UICommand("No") { Id = 1 });
                        var result1 = await addAnotherAccy.ShowAsync();
                        if ((int)result1.Id == 0)
                        {
                            SolutionHelper.CurrentSolutionStage = GuidedSolutionStages.SelectAccessoryXbox;
                            XboxListToDisplay.Clear();
                            Products.ToList().ForEach(x => { if (!x.SKU.Contains("QH4") && x.SubCategory.Contains("Xbox Accessory")) { XboxListToDisplay.Add(x); } });
                        }
                        else
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "MoveToSolutionView")); //Send message back to XboxView.xaml.cs to navigate to the solutionview
                            SolutionHelper.CreateNewSolution(new Customer());
                        }

                        break;
                }
            }

            if (SolutionHelper.CurrentSolutionStage == GuidedSolutionStages.SelectDeviceXbox)
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "OpenCompleteBlades")); //Send message back to XboxView.xaml.cs to open the complete select blade
                SolutionHelper.CurrentSolutionStage = GuidedSolutionStages.SelectGameXbox;
                XboxListToDisplay.Clear();
                Products.ToList().ForEach(x => { if (x.SKU.Contains("QH4")) { XboxListToDisplay.Add(x); } });
                Products.ToList().ForEach(x => { if (x.Price.Equals(59.99) && !(x.Name.Contains("Controller"))) { XboxListToDisplay.Add(x); } });

                MessageDialog selectFreeGame = new MessageDialog("Select free game.");
                await selectFreeGame.ShowAsync();
            }
            
            
        }

        public void AddCompleteToSolution(bool addComplete)
        {
            if (addComplete)
            {
                SolutionHelper.AddProductToCurrentSolution(XboxComplete);
            }
        }
    }
}

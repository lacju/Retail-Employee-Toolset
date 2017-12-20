using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using retailemployeetoolset.Common;
using retailemployeetoolset.Models;
using retailemployeetoolset.Views;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace retailemployeetoolset.ViewModels
{
    class HomePageViewModel
    {
        public string CurrentUser { get; set; }

        public string RandomInspirationalBlurb { get; set; }

        Frame frame;
        public HomePageViewModel()
        {
            // CurrentUser = AuthenticationHelper.CurrentUserName;
            // RandomInspirationalBlurb = Random message
         //   CurrentUser = App.MobileService.CurrentUser.UserId.ToString();
            frame = new Frame();
        }

        public async void BusinessCustomerDialog()
        {
            MessageDialog businessDialogueBox = new MessageDialog("Is the device for personal or business use?");
            businessDialogueBox.Commands.Add(new UICommand("Business") { Id = 0 });
            businessDialogueBox.Commands.Add(new UICommand("Personal") { Id = 1 });
            //showDialog.DefaultCommandIndex = 0;
            //showDialog.CancelCommandIndex = 1;
            var result = await businessDialogueBox.ShowAsync();
            if ((int)result.Id == 0)
            {
                SolutionHelper.CreateNewSolution(new Customer(true, false));
            }
            else
            {
                SolutionHelper.CreateNewSolution(new Customer());
            }
        }



        public async void NavigateToNextStage()
        {
            if (SolutionHelper.CurrentSolution == null)
            {


                MessageDialog businessDialogueBox = new MessageDialog("Is the device for personal or business use?");
                businessDialogueBox.Commands.Add(new UICommand("Business") { Id = 0 });
                businessDialogueBox.Commands.Add(new UICommand("Personal") { Id = 1 });
                //showDialog.DefaultCommandIndex = 0;
                //showDialog.CancelCommandIndex = 1;
                var result = await businessDialogueBox.ShowAsync();
                if ((int)result.Id == 0)
                {
                    SolutionHelper.CreateNewSolution(new Models.Customer(true));
                    //frame.Navigate(typeof(Views.SurfaceProView));
                }
                else
                {
                    SolutionHelper.CreateNewSolution(new Models.Customer(false));
                    // frame.Navigate(typeof(Views.SurfaceProView));
                }

            }
            else
            {
                MessageDialog dialogueBox = new MessageDialog("You currenty have an active solution what would you like to do with it?");
                dialogueBox.Commands.Add(new UICommand("Continue it") { Id = 0 });
                dialogueBox.Commands.Add(new UICommand("Throw it away") { Id = 1 });
                dialogueBox.Commands.Add(new UICommand("Save it for later") { Id = 2 });
                //showDialog.DefaultCommandIndex = 0;
                //showDialog.CancelCommandIndex = 1;
                var result = await dialogueBox.ShowAsync();
                if ((int)result.Id == 0)
                {
                    switch (SolutionHelper.CurrentSolutionStage)
                    {
                        case GuidedSolutionStages.SelectCategory:
                            switch (SolutionHelper.CurrentSolutionDevice)
                            {
                                case GuidedSolutionDevices.SurfacePro:
                                    //       frame.Navigate(typeof(Views.SurfaceProView));
                                    break;
                                case GuidedSolutionDevices.SurfaceBook:
                                    break;
                                case GuidedSolutionDevices.SurfaceLaptop:
                                    break;
                                case GuidedSolutionDevices.SurfaceStudio:
                                    break;
                                case GuidedSolutionDevices.Xbox:
                                    break;
                                case GuidedSolutionDevices.OEM:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case GuidedSolutionStages.AddOffice:
                            //   frame.Navigate(typeof(OfficePage));
                            break;

                        case GuidedSolutionStages.AddCase:
                            switch (SolutionHelper.CurrentSolutionDevice)
                            {
                                case GuidedSolutionDevices.SurfacePro:
                                    //    frame.Navigate(typeof(Views.SelectSleeveView));
                                    break;
                                case GuidedSolutionDevices.SurfaceBook:
                                    break;
                                case GuidedSolutionDevices.SurfaceLaptop:
                                    break;
                            }
                            break;
                        case GuidedSolutionStages.AddAccessories:
                            switch (SolutionHelper.CurrentSolutionDevice)
                            {
                                case GuidedSolutionDevices.SurfacePro:
                                    break;
                                case GuidedSolutionDevices.SurfaceBook:
                                    break;
                                case GuidedSolutionDevices.SurfaceLaptop:
                                    break;
                                case GuidedSolutionDevices.SurfaceStudio:
                                    break;
                                case GuidedSolutionDevices.Xbox:
                                    break;
                                case GuidedSolutionDevices.OEM:
                                    break;
                                default:
                                    break;
                            }
                            break;

                    }
                }
                if ((int)result.Id == 0)
                {
                    MessageDialog busDiaBox = new MessageDialog("Is the device for personal or business use?");
                    busDiaBox.Commands.Add(new UICommand("Business") { Id = 0 });
                    busDiaBox.Commands.Add(new UICommand("Personal") { Id = 1 });
                    //showDialog.DefaultCommandIndex = 0;
                    //showDialog.CancelCommandIndex = 1;
                    var result2 = await dialogueBox.ShowAsync();
                    if ((int)result2.Id == 0)
                    {
                        SolutionHelper.CreateNewSolution(new Models.Customer(true));
                    }
                    else
                    {
                        SolutionHelper.CreateNewSolution(new Models.Customer(false));
                    }

                }
            }
        }


    }
}


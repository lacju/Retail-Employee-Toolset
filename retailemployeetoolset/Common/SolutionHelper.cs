using retailemployeetoolset.Models;
using retailemployeetoolset.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retailemployeetoolset.Common
{
    public enum GuidedSolutionDevices
    {
        SurfacePro, SurfaceBook, SurfaceLaptop, SurfaceStudio, Xbox, OEM
    }
    public enum GuidedSolutionStages
    {
       SelectCategory, SelectDeviceSurface, SelectDeviceXbox, SelectCompleteXbox, SelectGameXbox, SelectAccessoryXbox, SelectDeviceOEM, DeviceConfiguration, AddOffice, AddCase, AddAccessories
    }
    public static class SolutionHelper 
    {
        public static Solution CurrentSolution { get; set; }
        public static Customer CurrentSolutionCustomer { get; set; }        
        public static GuidedSolutionDevices CurrentSolutionDevice { get; set; }
        public static GuidedSolutionStages CurrentSolutionStage { get; set; }

        private static  double subtotaltracker = 0;
        private static double taxestracker = 0;
        private static double finaltotaltracker = 0;
        public static void CreateNewSolution(Customer customer)
        {
            CurrentSolutionCustomer = customer;
            CurrentSolution = new Solution(CurrentSolutionCustomer.BusinessCustomer);
            CurrentSolution.SetSolutionCustomer(customer);
            subtotaltracker = 0;
            taxestracker = 0;
            finaltotaltracker = 0;
        }
        public static void AddProductToCurrentSolution(Product productToAdd)
        {
             CurrentSolution.AddProductToSolution(productToAdd);
            subtotaltracker += productToAdd.Price;
            taxestracker = subtotaltracker * .0625; //MA Tax Rate
            finaltotaltracker = subtotaltracker * 1.0625;
            CurrentSolution.Subtotal = subtotaltracker.ToString();
            CurrentSolution.Taxes = taxestracker.ToString();
            CurrentSolution.FinalTotal = finaltotaltracker.ToString();
            
        }
        public static bool BusinessCustomer()
        {
            return CurrentSolution.BusinessCustomer;
        }
        public static void SetBusinessStatus(bool isBusiness)
        {
            CurrentSolution.BusinessCustomer = isBusiness;
        }

        public static async Task SuspendCurrentSolution()
        {
            CurrentSolution.PrepareSolutionForSuspension();
            await DataManager.SaveSolutionAsync(CurrentSolution);
            ClearSolution();
        }
        public static void ClearSolution()
        {
            CurrentSolution = null;
            CurrentSolutionCustomer = null;
            subtotaltracker = 0;
            taxestracker = 0;
            finaltotaltracker = 0;
        }
       

        private static async Task AddSolution(Solution queueCustomer)
        {
            await DataManager.SaveSolutionAsync(queueCustomer);
        }

    }
}

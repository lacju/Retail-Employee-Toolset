using retailemployeetoolset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using retailemployeetoolset.Common;
using System.ComponentModel;
using Windows.UI.Popups;

namespace retailemployeetoolset.ViewModels
{
    public class SurfaceViewModelBase : ViewModel
    {

        public Product SelectedSurface;
        public Product SelectedAccessory;
        public Product ConsumerComplete;
        public Product CommercialCompleteThreeYear;
        public Product CommercialCompleteFourYear;
        GuidedSolutionDevices SelectedDeviceClass;
        private string selectedProcessor, selectedGPU, selectedRAM, selectedStorage, selectedColor = string.Empty;
        private bool addAccessory = false;

        private string _consumerCompletePrice;
        public string ConsumerCompletePrice
        {
            get { return _consumerCompletePrice; }
            set { _consumerCompletePrice = value; OnPropertyChanged(); }
        }

        private string _commercialCompleteThreeYearPrice;
        public string CommercialCompleteThreeYearPrice
        {
            get { return _commercialCompleteThreeYearPrice; }
            set { _commercialCompleteThreeYearPrice = value; OnPropertyChanged(); }
        }

        private string _commercialCompleteFourYearPrice;
        public string CommercialCompleteFourYearPrice
        {
            get { return _commercialCompleteFourYearPrice; }
            set { _commercialCompleteFourYearPrice = value; OnPropertyChanged(); }
        }

        private string _surfacePrice;
        public string SurfacePrice
        {
            get { return _surfacePrice; }
            set { _surfacePrice = value; OnPropertyChanged(); }
        }

        public SurfaceViewModelBase()
        {
            SelectedDeviceClass = SolutionHelper.CurrentSolutionDevice;
            

            switch (SelectedDeviceClass)
            {
                case GuidedSolutionDevices.SurfacePro:
                    ConsumerComplete = Products.ToList().Find(x => x.Category.Equals("Complete") && x.SubCategory.Contains("Pro") && x.Name.Contains("Consumer"));
                    CommercialCompleteThreeYear = Products.ToList().Find(x => x.Category.Equals("Complete") && x.SubCategory.Contains("Pro") && x.Name.Contains("3 Year"));
                    CommercialCompleteFourYear = Products.ToList().Find(x => x.Category.Equals("Complete") && x.SubCategory.Contains("Pro") && x.Name.Contains("4 Year"));
                    SetCompletePrices();
                    break;
                case GuidedSolutionDevices.SurfaceBook:
                    ConsumerComplete = Products.ToList().Find(x => x.Category.Equals("Complete") && x.SubCategory.Contains("Book") && x.Name.Contains("Consumer"));
                    CommercialCompleteThreeYear = Products.ToList().Find(x => x.Category.Equals("Complete") && x.SubCategory.Contains("Book") && x.Name.Contains("Commercial") && x.Name.Contains("3 Year"));
                    CommercialCompleteFourYear = Products.ToList().Find(x => x.Category.Equals("Complete") && x.SubCategory.Contains("Book") && x.Name.Contains("Commercial") && x.Name.Contains("4 Year"));
                    SetCompletePrices();
                    break;
                case GuidedSolutionDevices.SurfaceLaptop:
                    ConsumerComplete = Products.ToList().Find(x => x.Category.Equals("Complete") && x.SubCategory.Contains("Surface Laptop") && x.Name.Contains("Consumer"));
                    CommercialCompleteThreeYear = Products.ToList().Find(x => x.Category.Equals("Complete") && x.SubCategory.Contains("Laptop") && x.Name.Contains("Commercial") && x.Name.Contains("3 Year"));
                    CommercialCompleteFourYear = Products.ToList().Find(x => x.Category.Equals("Complete") && x.SubCategory.Contains("Laptop") && x.Name.Contains("Commercial") && x.Name.Contains("4 Year"));
                    SetCompletePrices();
                    break;
                case GuidedSolutionDevices.SurfaceStudio:
                    ConsumerComplete = Products.ToList().Find(x => x.Category.Equals("Complete") && x.SubCategory.Contains("Studio") && x.Name.Contains("Consumer"));
                    CommercialCompleteThreeYear = Products.ToList().Find(x => x.Category.Equals("Complete") && x.SubCategory.Contains("Studio") && x.Name.Contains("Commercial") && x.Name.Contains("3 Year"));
                    SelectedAccessory = Products.ToList().Find(x => x.Name.Contains("Surface Dial"));
                    SetCompletePrices();
                    break;
                case GuidedSolutionDevices.Xbox:
                    break;
                case GuidedSolutionDevices.OEM:
                    break;
                default:
                    break;
            }
        }

        private void SetCompletePrices()
        {
            try
            {
                ConsumerCompletePrice = ConsumerComplete.Price.ToString();
                CommercialCompleteThreeYearPrice = CommercialCompleteThreeYear.Price.ToString();
                CommercialCompleteFourYearPrice = CommercialCompleteFourYear.Price.ToString();

            }
            catch 
            {

                
            }

        }

        public void SelectRam(string capacity)
        {
            selectedRAM = capacity;
            SelectSurface();
        }
        public void DeselectRam()
        {
            selectedRAM = string.Empty;
            SelectedSurface = null;
        }
        public void SelectProcessor(string series)
        {
            selectedProcessor = series;
        }
        public void DeselectProcessor()
        {
            selectedProcessor = string.Empty;
        }
        public void SelectGPU(string capacity)
        {
            selectedGPU = capacity;
            SelectSurface();
        }
        public void DeselectGPU()
        {
            selectedGPU = string.Empty;
            SelectedSurface = null;
        }
        public void SelectStorage(string capacity)
        {
            selectedStorage = capacity;
            SelectSurface();
        }
        public void DeselectStorage()
        {
            selectedStorage = string.Empty;
            SelectedSurface = null;
        }
        public void SelectColor(string color)
        {
            selectedColor = color;
            SelectSurface();
        }
        public void DeselectColor()
        {
            selectedColor = string.Empty;
            SelectedSurface = null;
        }
        public void AccessorySelected()
        {
            addAccessory = true;
            SelectSurface();
        }
        public void AccessoryDeselected()
        {
            addAccessory = false;
        }

        private void SelectSurface()
        {
            try
            {
                switch (SelectedDeviceClass)
                {
                    case GuidedSolutionDevices.SurfacePro:
                        SelectedSurface = Products.ToList().Find(x => x.SubCategory.Equals("Surface Pro") && x.Processor.Contains(selectedProcessor) && x.HDDCap.Contains(selectedStorage) && x.RAM.Contains(selectedRAM));
                        if (selectedColor != string.Empty)
                        {
                            SelectedAccessory = Products.ToList().Find(x => x.Name.Contains("Type Cover") && x.Name.Contains(selectedColor));
                        }
                        SurfacePrice = SelectedSurface.Price.ToString();
                        break;
                    case GuidedSolutionDevices.SurfaceBook:
                        SelectedSurface = Products.ToList().Find(x => x.SubCategory.Equals("Surface Book") && x.Processor.Contains(selectedProcessor) && x.RAM.Contains(selectedRAM) && x.HDDCap.Contains(selectedStorage));
                        SurfacePrice = SelectedSurface.Price.ToString();
                        break;
                    case GuidedSolutionDevices.SurfaceLaptop:
                        SelectedSurface = Products.ToList().Find(x => x.SubCategory.Equals("Surface Laptop") && x.Processor.Contains(selectedProcessor) && x.RAM.Contains(selectedRAM) && x.HDDCap.Contains(selectedStorage) && x.Name.Contains(selectedColor));
                        SurfacePrice = SelectedSurface.Price.ToString();
                        break;
                    case GuidedSolutionDevices.SurfaceStudio:
                        SelectedSurface = Products.ToList().Find(x => x.SubCategory.Equals("Surface Studio") && x.Processor.Contains(selectedProcessor) && x.RAM.Contains(selectedRAM) && x.GPU.Contains(selectedGPU));
                        SurfacePrice = SelectedSurface.Price.ToString();
                        break;
                    case GuidedSolutionDevices.Xbox:
                        break;
                    case GuidedSolutionDevices.OEM:
                        break;
                    default:
                        break;
                }
            }
            catch 
            {

                return;
            }            
        }

        //private void SetPrice()
        //{
        //    try
        //    {
        //        price = SelectedSurface.Price;
        //        if (SelectedAccessory != null)
        //        {
        //            price += SelectedAccessory.Price;
        //        }
        //        SurfacePrice = SelectedSurface.Price.ToString();
        //    }
        //    catch 
        //    {

                
        //    }
            
        //}
        //private void ClearPRice()
        //{
        //    price = 0;
        //    SurfacePrice = string.Empty;
        //}

        public async Task<bool> AddProductsToSolution() //Return bool to determine if the program should remain on the current page to add an accessory
        {
            if (SelectedSurface != null)
            {                
                if (addAccessory)
                {
                    SolutionHelper.AddProductToCurrentSolution(SelectedSurface);
                    SolutionHelper.AddProductToCurrentSolution(SelectedAccessory);
                    return true;
                }
                else
                {
                    if (SelectedDeviceClass == GuidedSolutionDevices.SurfacePro)
                    {
                        MessageDialog noAccessory = new MessageDialog("Are you sure you don't want to add a Type Cover?");
                        noAccessory.Commands.Add(new UICommand("Yes, I'm sure") { Id = 0 });
                        noAccessory.Commands.Add(new UICommand("Actually, let's add one") { Id = 1 });
                        var result = await noAccessory.ShowAsync();
                        if ((int)result.Id == 0)
                        {
                            SolutionHelper.AddProductToCurrentSolution(SelectedSurface);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (SelectedDeviceClass == GuidedSolutionDevices.SurfaceStudio)
                    {
                        MessageDialog noAccessory = new MessageDialog("Are you sure you don't want to add a Sruface Dial?");
                        noAccessory.Commands.Add(new UICommand("Yes, I'm sure") { Id = 0 });
                        noAccessory.Commands.Add(new UICommand("Actually, let's add one") { Id = 1 });
                        var result = await noAccessory.ShowAsync();
                        if ((int)result.Id == 0)
                        {
                            SolutionHelper.AddProductToCurrentSolution(SelectedSurface);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        SolutionHelper.AddProductToCurrentSolution(SelectedSurface);
                        return true;
                    }
                }
            }
            return false;
            
        }

        public async Task<bool> AddCompleteToSolution(string completeType)
        {
            switch (completeType)
            {
                case "Consumer":
                    SolutionHelper.AddProductToCurrentSolution(ConsumerComplete);
                    break;
                case "Commercial3":
                    SolutionHelper.AddProductToCurrentSolution(CommercialCompleteThreeYear);
                    break;
                case "Commercial4":
                    SolutionHelper.AddProductToCurrentSolution(CommercialCompleteFourYear);
                    break;
                case "None":
                    break;
            }
            MessageDialog addOffice = new MessageDialog("Would you like to add Office?");
            addOffice.Commands.Add(new UICommand("Yes") { Id = 0 });
            addOffice.Commands.Add(new UICommand("No") { Id = 1 });
            var result = await addOffice.ShowAsync();
            if ((int)result.Id == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}

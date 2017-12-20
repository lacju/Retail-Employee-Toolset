using Microsoft.Toolkit.Uwp.UI;
using retailemployeetoolset.Common;
using retailemployeetoolset.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace retailemployeetoolset.ViewModels
{
    public class OEMViewModel : ViewModel
    {

        public Product PerformanceGuardConsumer { get; set; }
        public Product PerformanceGuardCommercial3Year { get; set; }
        public Product PerformanceGuardCommercial4Year { get; set; }
        public AdvancedCollectionView ProductsAdvancedView;
        public ICommand AddToSolutionCommand { get; set; }
        public ICommand OpenCompleteBlade { get; set; }
        public string selectedFormFactor { get; set; }
        public string selectedProcessorSeries { get; set; }
        public string selectedMemoryCapacity { get; set; }
        public string selectedStorageCapacity { get; set; }
        private bool? dedicatedGPU = null;


        private string _consumerPerformanceGuardPrice;
        public string ConsumerPerformanceGuardPrice
        {
            get
            {
                return _consumerPerformanceGuardPrice;
            }
            set
            {
                _consumerPerformanceGuardPrice = value;
                OnPropertyChanged();
            }
        }

        private string _commercialPerformanceGuardThreeYearPrice;
        public string CommercialPerformanceGuardThreeYearPrice
        {
            get
            {
                return _commercialPerformanceGuardThreeYearPrice;
            }
            set
            {
                _commercialPerformanceGuardThreeYearPrice = value;
                OnPropertyChanged();
            }
        }

        private string _commercialPerformanceGuardFourYearPrice;
        public string CommercialPerformanceGuardFourYearPrice
        {
            get
            {
                return _commercialPerformanceGuardFourYearPrice;
            }
            set
            {
                _commercialPerformanceGuardFourYearPrice = value;
                OnPropertyChanged();
            }
        }


        private void AddToSolution(Product item)
        {
            Product productToAdd = item as Product;
            SolutionHelper.AddProductToCurrentSolution(productToAdd);
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "OpenCompleteBlades")); //Send message back to SelecSleeveView.xaml.cs to navigate to the SolutionView
        }

        public async Task<bool> AddCompleteToSolution(string completeType)
        {
            switch (completeType)
            {
                case "Consumer":
                    SolutionHelper.AddProductToCurrentSolution(PerformanceGuardConsumer);
                    break;
                case "Commercial3":
                    SolutionHelper.AddProductToCurrentSolution(PerformanceGuardCommercial3Year);
                    break;
                case "Commercial4":
                    SolutionHelper.AddProductToCurrentSolution(PerformanceGuardCommercial4Year);
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
        public OEMViewModel()
        {


            SolutionHelper.CurrentSolutionStage = GuidedSolutionStages.SelectDeviceOEM;
            SolutionHelper.CurrentSolutionDevice = GuidedSolutionDevices.OEM;
            Products = new ObservableCollection<Product>();
            Products.ToList().ForEach(x => { if (x.Category.Equals("PC")) { Products.Add(x); } });
            PerformanceGuardConsumer = Products.ToList().Find(x => x.SubCategory.Contains("Performance Guard") && x.Name.Contains("Consumer"));
            PerformanceGuardCommercial3Year = Products.ToList().Find(x => x.SubCategory.Contains("Performance Guard") && x.Name.Contains("Commercial 3 Year"));
            PerformanceGuardCommercial4Year = Products.ToList().Find(x => x.SubCategory.Contains("Performance Guard") && x.Name.Contains("Commercial 4 Year"));
            ProductsAdvancedView = new AdvancedCollectionView(Products);
            AddToSolutionCommand = new RelayCommand<Product>(AddToSolution);

        }



        public void SelectFormfactor(string formfactor)
        {
            selectedFormFactor = formfactor;
            //    ProdcutsAdvancedView.Filter = x => (!((Product) x).SubCategory.Contains(formfactor));
            ExecuteApplyFilter();
        }
        public void DeselectFormfactor()
        {
            selectedFormFactor = String.Empty;
            ExecuteApplyFilter();
        }
        public void SelectGPU(bool withGPU)
        {
            dedicatedGPU = withGPU;
            ExecuteApplyFilter();
        }
        public void DeselectGPU()
        {
            dedicatedGPU = null;
            ExecuteApplyFilter();
        }
        public void SelectProcessor(string series)
        {
            selectedProcessorSeries = series;
            //  ProdcutsAdvancedView.Filter = x => (!((Product)x).Processor.Contains(selectedProcessorSeries));
            ExecuteApplyFilter();
        }
        public void DeselectProcessor()
        {
            selectedProcessorSeries = String.Empty;
            ExecuteApplyFilter();
        }
        public void SelectMemory(string memcapacity)
        {
            selectedMemoryCapacity = memcapacity;
            //  ProdcutsAdvancedView.Filter = x => (!((Product)x).RAM.Contains(selectedMemoryCapacity));
            ExecuteApplyFilter();
        }
        public void DeselectMemory()
        {
            selectedMemoryCapacity = String.Empty;
            ExecuteApplyFilter();
        }
        public void SelectStorage(string capacity)
        {
            selectedStorageCapacity = capacity;
            //    ProdcutsAdvancedView.Filter = x => (!((Product)x).HDDCap.Contains(selectedStorageCapacity));
            ExecuteApplyFilter();
        }
        public void DeselectStorage()
        {
            selectedStorageCapacity = String.Empty;
            ExecuteApplyFilter();
        }

        private void FilterFormFactor()
        {
            if (selectedMemoryCapacity != String.Empty && selectedProcessorSeries != String.Empty && selectedStorageCapacity != string.Empty)
            {
                
            }
        }

        private List<Predicate<Product>> criteria = new List<Predicate<Product>>();

        private bool dynamic_Filter(object item)
        {
            Product p = item as Product;
            bool isIn = true;
            if (criteria.Count() == 0)
                return isIn;
            isIn = criteria.TrueForAll(x => x(p));

            return isIn;
        }

        

        private void ExecuteApplyFilter()
        {

            ProductsAdvancedView.Filter = null;

            criteria.Clear();

            if (selectedMemoryCapacity != null)
            {
                criteria.Add(new Predicate<Product>(x => x.RAM.Contains(selectedMemoryCapacity)));
            }

            if (selectedStorageCapacity != null)
            {
                criteria.Add(new Predicate<Product>(x => x.HDDCap.Contains(selectedStorageCapacity)));
            }
            if (selectedProcessorSeries != null)
            {
                criteria.Add(new Predicate<Product>(x => x.Processor.Contains(selectedProcessorSeries)));
            }
            if (selectedFormFactor != null)
            {
                criteria.Add(new Predicate<Product>(x => !(x.SubCategory.Contains(selectedFormFactor))));
            }
            if (dedicatedGPU.HasValue)
            {
                if (dedicatedGPU.Value)
                {
                    criteria.Add(new Predicate<Product>(x => x.GPU.Contains("NVIDIA") || x.GPU.Contains("AMD")));
                }
                else
                {
                    criteria.Add(new Predicate<Product>(x => !(x.GPU.Contains("NVIDIA")) && !(x.GPU.Contains("AMD"))));
                }
            }

            ProductsAdvancedView.Filter = dynamic_Filter;
        }

        private void FilterProducts()
        {
            if (selectedFormFactor == null)
            {
                selectedFormFactor = "@#$";
            }
            if (selectedMemoryCapacity == null)
            {
                selectedMemoryCapacity = "@#$";
            }
            if (selectedStorageCapacity == null)
            {
                selectedStorageCapacity = "@#$";
            }
            if (selectedProcessorSeries == null)
            {
                selectedProcessorSeries = "@#$";
            }

            ProductsAdvancedView.Filter = x => (( !((Product)x).SubCategory.Contains(selectedFormFactor)) || !((Product)x).RAM.Contains(selectedMemoryCapacity) || !((Product)x).Processor.Contains(selectedProcessorSeries) || !((Product)x).HDDCap.Contains(selectedStorageCapacity));
            //if (selectedFormFactor != string.Empty && selectedStorageCapacity != string.Empty && selectedFormFactor != string.Empty && selectedMemoryCapacity != string.Empty)
            //{
            //    ProdcutsAdvancedView.Filter = x => (((Product)x).RAM.Contains(selectedFormFactor) && ((Product)x).RAM.Contains(selectedStorageCapacity) && ((Product)x).Processor.Contains(selectedFormFactor) && ((Product)x).HDDCap.Contains(selectedMemoryCapacity));

            //}

        }

        //private Predicate<Product> FilterPredicate;

        //private static bool FilterProducts(Product obj)
        //{
        //    if (b)
        //    {

        //    }
        //    return obj.X * obj.Y > 100000;
        //}
    }
    }



using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Uwp.UI;
using retailemployeetoolset.Common;
using retailemployeetoolset.Models;
using Windows.UI.Popups;
using Microsoft.Graph;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace retailemployeetoolset.ViewModels
{
    
    public class ProductCatalogViewModel : ViewModel
    {
        public enum FilterTypes
        {
            Category, Manufacterer
        }


        public ObservableCollection<ProductCatalogProduct> ManufacturerCheckboxes { get; set; }
        private List<ProductCatalogProduct> _manufacturerCheckboxesList;
        private List<ProductCatalogProduct> _tempManufacturerCheckboxesList;
        public AdvancedCollectionView ManufacturerCheckboxesACV { get; set; }
        public ObservableCollection<ProductCatalogProduct> CategoryCheckboxes { get; set; }
        private List<ProductCatalogProduct> _categoryCheckboxesList;
        private List<ProductCatalogProduct> _tempCategoryCheckboxesList;
        public AdvancedCollectionView CategoryCheckboxesACV { get; set; }

        public ObservableCollection<ProductCatalogProduct> PriceCheckboxes { get; set; }
        private List<ProductCatalogProduct> _priceCheckboxesList;
        private List<ProductCatalogProduct> _tempPriceCheckboxesList;
        public AdvancedCollectionView PriceCheckboxesACV { get; set; }
        public AdvancedCollectionView CheckBoxesCollectionView { get; set; }

        private List<string> _searchFilters;
        private string searchParam = "";
        private bool _mfgSeclected, _categorySelected, _PriceSelected = false;
        private string _mfgFilter, _categoryFilter;
        private double _startPrice, _endPrice = 0;
        public ProductCatalogProduct selectedMfgCheckbox { get; set; }
        public ProductCatalogProduct selectedCatCheckbox { get; set; }
        public ProductCatalogProduct selectedPriceCheckbox { get; set; }
        public Product Prop;
        //Proper VM code base


        public ICommand CheckCategory { get; set; }
        public ICommand UncheckCategory { get; set; }
        public ICommand CheckManufacterer { get; set; }
        public ICommand UncheckManufacterer { get; set; }
        public ICommand CheckPrice { get; set; }
        public ICommand UncheckPrice { get; set; }
        public ICommand AddToSolution { get; set; }

        public AdvancedCollectionView ProductsAdvancedView;

        public ProductCatalogViewModel()
        {
            GenerateCheckBoxLists();
            CategoryCheckboxesACV = new AdvancedCollectionView(CategoryCheckboxes);
            PriceCheckboxesACV = new AdvancedCollectionView(PriceCheckboxes);
            ManufacturerCheckboxesACV = new AdvancedCollectionView(ManufacturerCheckboxes);
            CheckCategory = new RelayCommand<ProductCatalogProduct>(CheckCategoryFilter);
            UncheckCategory = new RelayCommand<ProductCatalogProduct>(UncheckCategoryFilter);
            CheckManufacterer = new RelayCommand<ProductCatalogProduct>(CheckManufacturerFilter);
            UncheckManufacterer = new RelayCommand<ProductCatalogProduct>(UncheckManufacturerFilter);
            CheckPrice = new RelayCommand<ProductCatalogProduct>(CheckPriceFilter);
            UncheckPrice = new RelayCommand<ProductCatalogProduct>(UncheckPriceFilter);
            AddToSolution = new RelayCommand<Product>(AddToProductSolution);
            ProductsAdvancedView = new AdvancedCollectionView(Products);


        }

        //private async void StoreOperationEventHandler(StoreOperationCompletedEvent mobileServiceEvent)
        //{
        //    await Task.Delay(500);
        //    PendingChanges = manager.MobileServiceClient.SyncContext.PendingOperations;
        //    IsStatusBarVisible = PendingChanges > 0;
        //}



        

        private bool PriceWithinRange(ProductCatalogProduct productToCompare)
        {
            if (productToCompare.Price >= _startPrice && productToCompare.Price <= _endPrice)
            {
                return true;
            }
            return false;
        }

        private void GenerateCheckBoxLists() //Generates the object collections that will represent the filter checkboxes in the View
        {
            _tempManufacturerCheckboxesList = new List<ProductCatalogProduct>();
            _tempCategoryCheckboxesList = new List<ProductCatalogProduct>();
            _tempPriceCheckboxesList = new List<ProductCatalogProduct>();
            _priceCheckboxesList = new List<ProductCatalogProduct>();
            _manufacturerCheckboxesList = new List<ProductCatalogProduct>();
            ManufacturerCheckboxes = new ObservableCollection<ProductCatalogProduct>();
            _categoryCheckboxesList = new List<ProductCatalogProduct>();
            CategoryCheckboxes = new ObservableCollection<ProductCatalogProduct>();

            PriceCheckboxes = new ObservableCollection<ProductCatalogProduct>();
            PriceCheckboxes.Add(new ProductCatalogProduct("$0 - $24.99", 0, 24.99));
            PriceCheckboxes.Add(new ProductCatalogProduct("$25 - $49.99", 25, 49.99));
            PriceCheckboxes.Add(new ProductCatalogProduct("$50 - $99.99", 50, 99.99));
            PriceCheckboxes.Add(new ProductCatalogProduct("$100 - $249.99", 100, 249.99));
            PriceCheckboxes.Add(new ProductCatalogProduct("$250 - $499.99", 250, 499.99));
            PriceCheckboxes.Add(new ProductCatalogProduct("$500 - $999.99", 500, 999.99));
            PriceCheckboxes.Add(new ProductCatalogProduct("$1000 - $1999.99", 1000, 1999.99));
            PriceCheckboxes.Add(new ProductCatalogProduct("2000+", 1999.999, 50000));

            foreach (ProductCatalogProduct pcp in PriceCheckboxes)
            {
                foreach (Product item in Products)
                {
                    if ((item.Price >= pcp.StartingPrice && item.Price <= pcp.EndPrice)&& !pcp.ManufacturersWithSameCategory.Contains(item.Manufacturer))
                    {
                        pcp.ManufacturersWithSameCategory.Add(item.Manufacturer);
                    }
                    if ((item.Price >= pcp.StartingPrice && item.Price <= pcp.EndPrice) && !pcp.CategoriesWithSameManufacterer.Contains(item.Category))
                    {
                        pcp.CategoriesWithSameManufacterer.Add(item.Category);
                    }
                }
            }


            _manufacturerCheckboxesList = GenerateDistinctList(ProductsAdvancedView, ProductComparer.ListTypes.Manufacturer);

            foreach (ProductCatalogProduct mfgCheckBoxItem in _manufacturerCheckboxesList)
            {
                foreach (Product product in Products)
                {
                    if (mfgCheckBoxItem.Manufacturer == product.Manufacturer && !mfgCheckBoxItem.CategoriesWithSameManufacterer.Contains(product.Category) )
                    {
                        mfgCheckBoxItem.CategoriesWithSameManufacterer.Add(product.Category);
                    }
                    if (mfgCheckBoxItem.Manufacturer == product.Manufacturer && !mfgCheckBoxItem.PricesSupported.Contains(product.Price))
                    {
                        mfgCheckBoxItem.PricesSupported.Add(product.Price);
                    }
                }
            }
            _manufacturerCheckboxesList.ForEach(x => ManufacturerCheckboxes.Add(x));


            _categoryCheckboxesList = GenerateDistinctList(ProductsAdvancedView, ProductComparer.ListTypes.Category);

            foreach (ProductCatalogProduct catCheckBoxItem in _categoryCheckboxesList)
            {
                foreach (Product product in Products)
                {
                    if (catCheckBoxItem.Category == product.Category && !catCheckBoxItem.ManufacturersWithSameCategory.Contains(product.Manufacturer))
                    {
                        catCheckBoxItem.ManufacturersWithSameCategory.Add(product.Manufacturer);
                    }
                    if (catCheckBoxItem.Category == product.Category && !catCheckBoxItem.PricesSupported.Contains(product.Price))
                    {
                        catCheckBoxItem.PricesSupported.Add(product.Price);
                    }
                }
            }
            _categoryCheckboxesList.ForEach(x => CategoryCheckboxes.Add(x));


        }

        private List<ProductCatalogProduct> GenerateDistinctList(AdvancedCollectionView viewToFilter, ProductComparer.ListTypes list)
        {

            List<ProductCatalogProduct> newACV = new List<ProductCatalogProduct>();
                    List<Product> tempList = new List<Product>();
            foreach (Product pcp in viewToFilter)
            {
                tempList.Add(pcp);
            }
                    IEnumerable<Product> NoDuplicatesInitialCat = tempList.Distinct(new ProductComparer(list));
                    foreach (var p in NoDuplicatesInitialCat)
                    {
                ProductCatalogProduct pCP = new ProductCatalogProduct(p);
                        newACV.Add(pCP);
                    }

            return newACV;


        }

        private void AddToProductSolution(Product product)
        {
            Product productToAdd = product as Product;
            if (SolutionHelper.CurrentSolution != null)
            {
                SolutionHelper.AddProductToCurrentSolution(productToAdd);
            }
            else
            {
                SolutionHelper.CreateNewSolution(new Customer(true));
                SolutionHelper.AddProductToCurrentSolution(productToAdd);
            }
        }
        private void UncheckAllCategories()
        {
            CategoryCheckboxes.ToList().ForEach(x =>
            {
 (x as ProductCatalogProduct).UncheckCategory();

            });
        }
        private void UncheckAllManufacterers()
        {
            ManufacturerCheckboxes.ToList().ForEach(x =>
            {

                    (x as ProductCatalogProduct).UncheckMfg();

            });
        }

        public void CheckPriceFilter(ProductCatalogProduct product)
        {
            if (selectedPriceCheckbox != null)
            {
                selectedPriceCheckbox.UncheckPrice();
                selectedPriceCheckbox = null;
            }
            _PriceSelected = true;
            _startPrice = product.StartingPrice;
            _endPrice = product.EndPrice;
         //   ExecuteButtonManufactureFilter();
         //   ExecuteButtonPriceFilter();
            selectedPriceCheckbox = product;
            if (selectedMfgCheckbox != null)
            {
                selectedMfgCheckbox.CheckMfg();
            }
            if (selectedCatCheckbox != null)
            {
                selectedCatCheckbox.CheckCategory();
            }
            selectedPriceCheckbox.CheckPrice();
            ExecuteProductFilter();
            ExecuteButtonFilters();

        }

        public void UncheckPriceFilter(ProductCatalogProduct product)
        {
            if (selectedPriceCheckbox != null)
            {
                selectedPriceCheckbox.UncheckPrice();
                selectedPriceCheckbox = null;
            }
            _PriceSelected = false;
            _startPrice = 0;
            _endPrice = 0;
         //   ExecuteButtonManufactureFilter();
          //  ExecuteButtonPriceFilter();
            if (selectedMfgCheckbox != null)
            {

                selectedMfgCheckbox.CheckMfg();
            }
            if (selectedCatCheckbox != null)
            {
                selectedCatCheckbox.CheckCategory();
            }
            ExecuteProductFilter();
            ExecuteButtonFilters();

        }


        public void CheckCategoryFilter(ProductCatalogProduct product)
        {
            if (selectedCatCheckbox != null)
            {
                selectedCatCheckbox.UncheckCategory();
                selectedCatCheckbox = null;
            }
            _categorySelected = true;
            _categoryFilter = product.Category;
            //   ExecuteButtonCategoryFilter();
            // ExecuteButtonManufactureFilter();
            selectedCatCheckbox = product;
            if (selectedMfgCheckbox != null)
            {
                selectedMfgCheckbox.CheckMfg();
            }
            if (selectedPriceCheckbox != null)
            {
                selectedPriceCheckbox.CheckPrice();
            }
            selectedCatCheckbox.CheckCategory();
            ExecuteProductFilter();
            ExecuteButtonFilters();

        }

        public void UncheckCategoryFilter(ProductCatalogProduct product)
        {
            if (selectedCatCheckbox != null)
            {
                selectedCatCheckbox.UncheckCategory();
                selectedCatCheckbox = null;
            }
            _categorySelected = false;
            _categoryFilter = String.Empty;
            //   ExecuteButtonCategoryFilter();
            //  ExecuteButtonManufactureFilter();
            if (selectedMfgCheckbox != null)
            {

                selectedMfgCheckbox.CheckMfg();
            }
            if (selectedPriceCheckbox != null)
            {
                selectedPriceCheckbox.CheckPrice();
            }
            ExecuteProductFilter();
            ExecuteButtonFilters();

        }

        public void CheckManufacturerFilter(ProductCatalogProduct product)
        {
            if (selectedMfgCheckbox != null)
            {
                selectedMfgCheckbox.UncheckMfg();
                selectedMfgCheckbox = null;
            }
            _mfgSeclected = true;
            _mfgFilter = product.Manufacturer;
        //    ExecuteButtonManufactureFilter();
        //    ExecuteButtonPriceFilter();
            selectedMfgCheckbox = product;
            if (selectedCatCheckbox != null)
            {
                selectedCatCheckbox.CheckCategory();
            }
            if (selectedPriceCheckbox != null)
            {
                selectedPriceCheckbox.CheckMfg();
            }
            selectedMfgCheckbox.CheckPrice();
            ExecuteProductFilter();
            ExecuteButtonFilters();

        }

        public async void UncheckManufacturerFilter(ProductCatalogProduct product)
        {
            try
            {
                if (selectedMfgCheckbox != null)
                {
                    selectedMfgCheckbox.UncheckMfg();
                    selectedMfgCheckbox = null;
                }
                _mfgSeclected = false;
                _mfgFilter = String.Empty;
                // ExecuteButtonManufactureFilter();
                //  ExecuteButtonPriceFilter();
                if (selectedCatCheckbox != null)
                {
                    selectedCatCheckbox.CheckCategory();
                }
                if (selectedPriceCheckbox != null)
                {
                    selectedPriceCheckbox.CheckPrice();
                }
                ExecuteButtonFilters();
                ExecuteProductFilter();
            }
            catch (Exception h)
            {

                MessageDialog test = new MessageDialog(h.ToString());
                await test.ShowAsync();
            }

            
        }



        private List<Predicate<Product>> _criteria = new List<Predicate<Product>>();
        private List<Predicate<ProductCatalogProduct>> _categoryButtonscriteria = new List<Predicate<ProductCatalogProduct>>();
        private List<Predicate<ProductCatalogProduct>> _manufacturerButtonscriteria = new List<Predicate<ProductCatalogProduct>>();
        private List<Predicate<ProductCatalogProduct>> _priceButtonscriteria = new List<Predicate<ProductCatalogProduct>>();




        private bool dynamic_Filter(object item)
        {
            Product p = item as Product;
            bool isIn = true;
            if (_criteria.Count() == 0)
                return isIn;
            isIn = _criteria.TrueForAll(x => x(p));

            return isIn;
        }

        private bool dynamic_FilterCategoryButtons(object item)
        {
            ProductCatalogProduct p = item as ProductCatalogProduct;
            bool isIn = true;
            if (_categoryButtonscriteria.Count() == 0)
                return isIn;
            isIn = _categoryButtonscriteria.TrueForAll(x => x(p));

            return isIn;
        }

        private bool dynamic_FilterManufacturerButtons(object item)
        {
            ProductCatalogProduct p = item as ProductCatalogProduct;
            bool isIn = true;
            if (_manufacturerButtonscriteria.Count() == 0)
                return isIn;
            isIn = _manufacturerButtonscriteria.TrueForAll(x => x(p));

            return isIn;
        }

        private bool dynamic_FilterPriceButtons(object item)
        {
            ProductCatalogProduct p = item as ProductCatalogProduct;
            bool isIn = true;
            if (_priceButtonscriteria.Count() == 0)
                return isIn;
            isIn = _priceButtonscriteria.TrueForAll(x => x(p));

            return isIn;
        }

        public void ClearAllFilters()
        {
            if (selectedCatCheckbox != null)
            {
                selectedCatCheckbox.UncheckCategory();
            }
            if (selectedMfgCheckbox != null)
            {
                selectedMfgCheckbox.UncheckMfg();
            }
            if (selectedCatCheckbox != null)
            {
                selectedCatCheckbox.UncheckCategory();
            }
            selectedPriceCheckbox = null;
            selectedMfgCheckbox = null;
            selectedCatCheckbox = null;
            _categorySelected = false;
            _mfgSeclected = false;
            _PriceSelected = false;
            _mfgFilter = String.Empty;
            _categoryFilter = String.Empty;
            FilterButtons();
            
        }
        
        private void FilterButtons()
        {
            if (_categorySelected && _mfgSeclected && _PriceSelected)
            {
                foreach (ProductCatalogProduct pcp in CategoryCheckboxes.ToList())
                {
                    if (!(pcp.ManufacturersWithSameCategory.Contains(_mfgFilter)) && !PriceWithinRange(pcp))
                    {
                        CategoryCheckboxes.Remove(pcp);
                        _tempCategoryCheckboxesList.Add(pcp);
                    }
                }

                foreach (ProductCatalogProduct pcp in ManufacturerCheckboxes.ToList())
                {
                    if (!(pcp.CategoriesWithSameManufacterer.Contains(_categoryFilter)) && !PriceWithinRange(pcp))
                    {
                        ManufacturerCheckboxes.Remove(pcp);
                        _tempManufacturerCheckboxesList.Add(pcp);
                    }
                }

                foreach (ProductCatalogProduct pcp in PriceCheckboxes.ToList())
                {
                    if (!(pcp.CategoriesWithSameManufacterer.Contains(_categoryFilter)) && !(pcp.ManufacturersWithSameCategory.Contains(_mfgFilter)))
                    {
                        PriceCheckboxes.Remove(pcp);
                        _tempPriceCheckboxesList.Add(pcp);
                    }
                }
            }

            if (_categorySelected && _mfgSeclected)
            {
                foreach (ProductCatalogProduct pcp in CategoryCheckboxes.ToList())
                {
                    if (!(pcp.ManufacturersWithSameCategory.Contains(_mfgFilter)))
                    {
                        CategoryCheckboxes.Remove(pcp);
                        _tempCategoryCheckboxesList.Add(pcp);
                    }
                }

                foreach (ProductCatalogProduct pcp in ManufacturerCheckboxes.ToList())
                {
                    if (!(pcp.CategoriesWithSameManufacterer.Contains(_categoryFilter)))
                    {
                        ManufacturerCheckboxes.Remove(pcp);
                        _tempManufacturerCheckboxesList.Add(pcp);
                    }
                }

                foreach (ProductCatalogProduct pcp in PriceCheckboxes.ToList())
                {
                    if (!(pcp.CategoriesWithSameManufacterer.Contains(_categoryFilter)) && !(pcp.ManufacturersWithSameCategory.Contains(_mfgFilter)))
                    {
                        PriceCheckboxes.Remove(pcp);
                        _tempPriceCheckboxesList.Add(pcp);
                    }
                }
            }

            if (_PriceSelected && _mfgSeclected)
            {
                foreach (ProductCatalogProduct pcp in CategoryCheckboxes.ToList())
                {
                    if (!(pcp.ManufacturersWithSameCategory.Contains(_mfgFilter)))
                    {
                        CategoryCheckboxes.Remove(pcp);
                        _tempCategoryCheckboxesList.Add(pcp);
                    }
                }

                foreach (ProductCatalogProduct pcp in PriceCheckboxes.ToList())
                {
                    if (!pcp.ManufacturersWithSameCategory.Contains(_mfgFilter))
                    {
                        PriceCheckboxes.Remove(pcp);
                        _tempPriceCheckboxesList.Add(pcp);
                    }
                }

                foreach (ProductCatalogProduct pcp in ManufacturerCheckboxes.ToList())
                {
                    if (!(pcp.CategoriesWithSameManufacterer.Contains(_categoryFilter)) && !PriceWithinRange(pcp))
                    {
                        ManufacturerCheckboxes.Remove(pcp);
                        _tempManufacturerCheckboxesList.Add(pcp);
                    }
                }
            }

            if (_categorySelected && _PriceSelected)
            {
                foreach (ProductCatalogProduct pcp in PriceCheckboxes.ToList())
                {
                    if (pcp.CategoriesWithSameManufacterer.Contains(_categoryFilter))
                    {
                        PriceCheckboxes.Remove(pcp);
                        _tempPriceCheckboxesList.Add(pcp);
                    }
                }

                foreach (ProductCatalogProduct pcp in ManufacturerCheckboxes.ToList())
                {
                    if (!(pcp.CategoriesWithSameManufacterer.Contains(_categoryFilter)))
                    {
                        ManufacturerCheckboxes.Remove(pcp);
                        _tempManufacturerCheckboxesList.Add(pcp);
                    }
                }

                foreach (ProductCatalogProduct pcp in CategoryCheckboxes.ToList())
                {
                    if (!(pcp.ManufacturersWithSameCategory.Contains(_mfgFilter)) && !PriceWithinRange(pcp))
                    {
                        CategoryCheckboxes.Remove(pcp);
                        _tempCategoryCheckboxesList.Add(pcp);
                    }
                }
            }

            if (_mfgSeclected)
            {
                foreach (ProductCatalogProduct pcp in CategoryCheckboxes.ToList())
                {
                    if (!(pcp.ManufacturersWithSameCategory.Contains(_mfgFilter)))
                    {
                        CategoryCheckboxes.Remove(pcp);
                        _tempCategoryCheckboxesList.Add(pcp);
                    }
                }
                foreach (ProductCatalogProduct pcp in PriceCheckboxes.ToList())
                {
                    if (pcp.ManufacturersWithSameCategory.Contains(_mfgFilter))
                    {
                        PriceCheckboxes.Remove(pcp);
                        _tempPriceCheckboxesList.Add(pcp);
                    }
                }
            }

                    if (_categorySelected)
            {
                foreach (ProductCatalogProduct pcp in ManufacturerCheckboxes.ToList())
                {
                    if (!(pcp.CategoriesWithSameManufacterer.Contains(_categoryFilter)))
                    {
                        ManufacturerCheckboxes.Remove(pcp);
                        _tempManufacturerCheckboxesList.Add(pcp);
                    }
                }

                foreach (ProductCatalogProduct pcp in PriceCheckboxes.ToList())
                {
                    if (pcp.CategoriesWithSameManufacterer.Contains(_categoryFilter))
                    {
                        PriceCheckboxes.Remove(pcp);
                        _tempPriceCheckboxesList.Add(pcp);
                    }
                }
            }

            if (_PriceSelected)
            {
                foreach (ProductCatalogProduct pcp in ManufacturerCheckboxes.ToList())
                {
                    if (PriceWithinRange(pcp))
                    {
                        ManufacturerCheckboxes.Remove(pcp);
                        _tempManufacturerCheckboxesList.Add(pcp);
                    }
                }
                foreach (ProductCatalogProduct pcp in CategoryCheckboxes.ToList())
                {
                    if (PriceWithinRange(pcp))
                    {
                        CategoryCheckboxes.Remove(pcp);
                        _tempCategoryCheckboxesList.Add(pcp);
                    }
                }
            }

            if (!_mfgSeclected)
            {
                _tempCategoryCheckboxesList.ForEach(x => CategoryCheckboxes.Add(x));
                _tempCategoryCheckboxesList.Clear();
            }
            if (!_categorySelected)
            {
                _tempManufacturerCheckboxesList.ForEach(x => ManufacturerCheckboxes.Add(x));
                _tempManufacturerCheckboxesList.Clear();
            }
            if (!_PriceSelected)
            {
                _tempPriceCheckboxesList.ForEach(x => PriceCheckboxes.Add(x));
                _tempPriceCheckboxesList.Clear();
            }
        }

        private async void ExecuteButtonFilters()
        {
            try
            {
                ManufacturerCheckboxesACV.Filter = null;
                PriceCheckboxesACV.Filter = null;
                CategoryCheckboxesACV.Filter = null;

                _categoryButtonscriteria.Clear();
                _manufacturerButtonscriteria.Clear();
                _priceButtonscriteria.Clear();

                if (_mfgSeclected)
                {
                    _categoryButtonscriteria.Add(new Predicate<ProductCatalogProduct>(x => x.ManufacturersWithSameCategory.Contains(_mfgFilter)));
                    _priceButtonscriteria.Add(new Predicate<ProductCatalogProduct>(x => x.CategoriesWithSameManufacterer.Contains(_categoryFilter)));
                }
                if (_PriceSelected)
                {
                    _categoryButtonscriteria.Add(new Predicate<ProductCatalogProduct>(x => x.CategoriesWithSameManufacterer.Contains(_categoryFilter)));
                    _manufacturerButtonscriteria.Add(new Predicate<ProductCatalogProduct>(x => x.ManufacturersWithSameCategory.Contains(_mfgFilter)));
                }
                if (_categorySelected)
                {
                    _manufacturerButtonscriteria.Add(new Predicate<ProductCatalogProduct>(x => x.CategoriesWithSameManufacterer.Contains(_categoryFilter)));
                    _priceButtonscriteria.Add(new Predicate<ProductCatalogProduct>(x => x.CategoriesWithSameManufacterer.Contains(_categoryFilter)));
                }
                CategoryCheckboxesACV.Filter = dynamic_FilterCategoryButtons;
                ManufacturerCheckboxesACV.Filter = dynamic_FilterManufacturerButtons;
                PriceCheckboxesACV.Filter = dynamic_FilterPriceButtons;
            }
            catch (Exception h)
            {

                MessageDialog test = new MessageDialog(h.ToString());
                await test.ShowAsync();
            }

            

        }

        private void ExecuteButtonCategoryFilter()
        {
            ManufacturerCheckboxesACV.Filter = null;
            PriceCheckboxesACV.Filter = null;
            CategoryCheckboxesACV.Filter = null;

            _categoryButtonscriteria.Clear();

            if (_mfgSeclected)
            {
                _categoryButtonscriteria.Add(new Predicate<ProductCatalogProduct>(x => x.ManufacturersWithSameCategory.Contains(_mfgFilter)));
            }
            if (_PriceSelected)
            {
                _categoryButtonscriteria.Add(new Predicate<ProductCatalogProduct>(x => x.CategoriesWithSameManufacterer.Contains(_categoryFilter)));
            }

            CategoryCheckboxesACV.Filter = dynamic_FilterCategoryButtons;
        }

        private void ExecuteButtonManufactureFilter()
        {
            ManufacturerCheckboxesACV.Filter = null;

            _manufacturerButtonscriteria.Clear();

            if (_categorySelected)
            {
                _manufacturerButtonscriteria.Add(new Predicate<ProductCatalogProduct>(x => x.CategoriesWithSameManufacterer.Contains(_categoryFilter)));
            }
            if (_PriceSelected)
            {
                _manufacturerButtonscriteria.Add(new Predicate<ProductCatalogProduct>(x => x.ManufacturersWithSameCategory.Contains(_mfgFilter)));
            }

            ManufacturerCheckboxesACV.Filter = dynamic_FilterManufacturerButtons;
        }

        private void ExecuteButtonPriceFilter()
        {
            PriceCheckboxesACV.Filter = null;

            _priceButtonscriteria.Clear();

            if (_mfgSeclected)
            {
                _priceButtonscriteria.Add(new Predicate<ProductCatalogProduct>(x => x.CategoriesWithSameManufacterer.Contains(_categoryFilter)));
            }
            if (_categorySelected)
            {
                _priceButtonscriteria.Add(new Predicate<ProductCatalogProduct>(x => x.ManufacturersWithSameCategory.Contains(_mfgFilter)));
            }

            PriceCheckboxesACV.Filter = dynamic_FilterPriceButtons;
        }


        private void ExecuteProductFilter()
        {

            ProductsAdvancedView.Filter = null;

            _criteria.Clear();

            if (_categorySelected)
            {
                _criteria.Add(new Predicate<Product>(x => x.Category.Contains(_categoryFilter)));
            }
            if (_PriceSelected)
            {
                _criteria.Add(new Predicate<Product>(x => (x.Price >= _startPrice && x.Price <= _endPrice)));
            }
            if (_mfgSeclected)
            {
                _criteria.Add(new Predicate<Product>(x => x.Manufacturer.Contains(_mfgFilter)));
            }

            ProductsAdvancedView.Filter = dynamic_Filter;
        }

       

        public void FilterProductDatabase(string searchParameters) //filters the advancedcollectionview using the SearchFilter method
        {
            searchParam = searchParameters;
            ProductsAdvancedView.Filter = null;
            ProductsAdvancedView.Filter = SearchFilter;
        }

        private bool SearchFilter(object item) //Filters each object in the advancedcollectionview based on user input into a textbox
        {


            Product p = item as Product;
            string searchTerms = "";
            searchTerms = p.Manufacturer + " " + p.Category + " " + p.SubCategory + " " + p.Name + " " + p.Description;
            if (searchTerms.ToLower().Contains(searchParam.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        class ProductComparer : IEqualityComparer<Product>
        {
            public enum ListTypes
            {
                Manufacturer, Category, Price
            }

            public ListTypes lists;

            public ProductComparer(ListTypes listToFill)
            {
               
                lists = listToFill;

            }

            // Products are equal if their names and product numbers are equal.
            public bool Equals(Product x, Product y)
            {

                switch (lists)
                {
                    case ListTypes.Category:
                        if (Object.ReferenceEquals(x, y)) return true;
                        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y.Manufacturer, null))
                            return false;
                        return x.Category == y.Category;

                    case ListTypes.Manufacturer:
                        if (Object.ReferenceEquals(x, y)) return true;
                        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y.Manufacturer, null))
                            return false;
                        return x.Manufacturer == y.Manufacturer;

                    case ListTypes.Price:
                        if (Object.ReferenceEquals(x, y)) return true;
                        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y.Manufacturer, null))
                            return false;
                        return x.Price == y.Price;
                }
                return false;
            }

            // If Equals() returns true for a pair of objects 
            // then GetHashCode() must return the same value for these objects.

            public int GetHashCode(Product product)
            {
                switch (lists)
                {
                    case ListTypes.Category:
                        if (Object.ReferenceEquals(product, null)) return 0;
                        int hashProductCategory = product.Category == null ? 0 : product.Category.GetHashCode();
                        return hashProductCategory;

                    case ListTypes.Manufacturer:
                        if (Object.ReferenceEquals(product, null)) return 0;
                        int hashProductManufacturer = product.Manufacturer == null ? 0 : product.Manufacturer.GetHashCode();
                        return hashProductManufacturer;

                    case ListTypes.Price:
                        if (Object.ReferenceEquals(product, null)) return 0;
                        int hashProductPrice = product.Price == null ? 0 : product.Price.GetHashCode();
                        return hashProductPrice;
                        //Check whether the object is null

                        //Get hash code for the Name field if it is not null.

                        //Calculate the hash code for the product.

                }
                return 0;
            }
        }
    }
}


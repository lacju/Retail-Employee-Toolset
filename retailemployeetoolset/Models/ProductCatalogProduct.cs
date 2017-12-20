using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retailemployeetoolset.Models
{
   public  class ProductCatalogProduct : Product, INotifyPropertyChanged
    {
        private bool _isCheckedCategory;
        public string PriceRange { get; set; }
        public double StartingPrice { get; set; }
        public double EndPrice { get; set; }
        public bool IsCheckedCategory
        {
            get { return _isCheckedCategory; }
            set { _isCheckedCategory = value; NotifyPropertyChanged("IsCheckedCategory"); }
        }

        private bool _isCheckedMfg;
        public bool IsCheckedMfg
        {
            get { return _isCheckedMfg; }
            set { _isCheckedMfg = value; NotifyPropertyChanged("IsCheckedMfg"); }
        }

        private bool _isCheckedPrice;
        public bool IsCheckedPrice
        {
            get { return _isCheckedPrice; }
            set { _isCheckedPrice = value; NotifyPropertyChanged("IsCheckedPrice"); }
        }
        public ProductCatalogProduct(Product productBase)
        {
            Manufacturer = productBase.Manufacturer;
            Category = productBase.Category;
            Price = productBase.Price;
            ManufacturersWithSameCategory = new List<string>();
            CategoriesWithSameManufacterer = new List<string>();
            PricesSupported = new List<double>();

        }

        public ProductCatalogProduct(string Price, double startPrice, double endPrice)
        {
            PriceRange = Price;
            StartingPrice = startPrice;
            EndPrice = endPrice;
            ManufacturersWithSameCategory = new List<string>();
            CategoriesWithSameManufacterer = new List<string>();
            PricesSupported = new List<double>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public List<string> ManufacturersWithSameCategory { get; set; }
        public List<string> CategoriesWithSameManufacterer { get; set; }
        public List<double> PricesSupported { get; set; }

        public void UncheckAll()
        {
            IsCheckedCategory = false;
            IsCheckedMfg = false;
        }

        public void CheckAll()
        {
            IsCheckedCategory = true;
            IsCheckedMfg = true;
        }

        public void CheckCategory()
        {
            IsCheckedCategory = true;
        }

        public void UncheckCategory()
        {
            IsCheckedCategory = false;
        }

        public void CheckMfg()
        {
            IsCheckedMfg = true;
        }

        public void UncheckMfg()
        {
            IsCheckedMfg = false;
        }

        public bool GetCategoryChecked()
        {
            return IsCheckedCategory;
        }

        public bool GetMfgChecked()
        {
            return IsCheckedMfg;
        }

        public void CheckPrice()
        {
            IsCheckedPrice = true;
        }

        public void UncheckPrice()
        {
            IsCheckedPrice = false;
        }

        public bool GetPriceChecked()
        {
            return IsCheckedPrice;
        }
    }
}

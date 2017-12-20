using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using retailemployeetoolset.Models;

namespace retailemployeetoolset.ViewModels
{
    public class ProductViewModel
    {
        private IMobileServiceTable<Product> productTable;
        private MobileServiceCollection<Product, Product> _products;

        public MobileServiceCollection<Product, Product> Products
        {
            get { return _products; }
        }

        public ProductViewModel()
        {
            try
            {
                productTable = App.MobileService.GetTable<Product>();
            }
            catch 
            {

                //handle exception
            }
        }

        public async Task<bool> InsertProduct(Product newProduct)
        {
            try
            {
                await productTable.InsertAsync(newProduct);
                _products.Add(newProduct);
                return true;
            }
            catch 
            {

                return false;
            }
        }

        public async Task<MobileServiceCollection<Product, Product>> GetProducts()
        {
            try
            {
                _products = await productTable.ToCollectionAsync();
                return Products;
            }
            catch 
            {

                return null;
            }
        }
    }
}

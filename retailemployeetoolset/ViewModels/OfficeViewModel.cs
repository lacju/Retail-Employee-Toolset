using retailemployeetoolset.Common;
using retailemployeetoolset.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retailemployeetoolset.ViewModels
{
    public class OfficeViewModel : ViewModel
    {

        private ObservableCollection<Product> officeProducts;


        public OfficeViewModel()
        {


            officeProducts = new ObservableCollection<Product>();
            var tempList = Products.Where<Product>(x => x.Name.Contains("Microsoft Office"));
            tempList.ToList<Product>().ForEach(x => { if (x.Name.Contains("Microsoft Office")) { officeProducts.Add(x); } });
        }
       

        public void AddOffice365ToSolution(string personalOrHome, int subLength)
        {
            for (int i = 0; i < subLength; i++)
            {
                officeProducts.ToList().ForEach(x => { if (x.Name.Contains(personalOrHome) && x.Name.Contains("365")) { SolutionHelper.AddProductToCurrentSolution(x); } });
            }
        }
        public void AddPerpetualOfficeToSolution(string studentOrBusiness)
        {
            officeProducts.ToList().ForEach(x => { if (x.Name.Contains(studentOrBusiness)) { SolutionHelper.AddProductToCurrentSolution(x); } });
        }
    }
}
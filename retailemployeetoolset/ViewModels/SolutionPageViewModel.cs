using retailemployeetoolset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace retailemployeetoolset.ViewModels
{
    class SolutionPageViewModel
    {
        public Solution CurrentSolution { get; set; }
        public Product ProductsInCurrentSolution { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public string BusinessName { get; set; }
    }
}

using PieShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.ViewModels
{
    public class ProductlistViewModel
    {
        public IEnumerable <Product> AllProduct { get; set; }
        public string CurrentCategory { get; set; }
    }
}

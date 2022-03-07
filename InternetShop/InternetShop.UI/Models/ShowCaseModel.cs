using InternetShop.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.UI.Models
{
    public class ShowCaseModel
    {
        public IEnumerable<ModelProduct> BestProducts { get; set; }
        public ModelCategory RootCategory { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BL.Interfaces
{
    public class ModelCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<ModelCategory> Child { get; set; }
        public IList<ModelProduct> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BL.Interfaces
{
    public class ModelOrder
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public IList<ModelProduct> Products { get; set; }
    }
}

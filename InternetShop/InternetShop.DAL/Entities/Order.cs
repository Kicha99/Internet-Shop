using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public IList<Product> Products { get; set; }
        public Order()
        {
            Products = new List<Product>();
        }
    }
}

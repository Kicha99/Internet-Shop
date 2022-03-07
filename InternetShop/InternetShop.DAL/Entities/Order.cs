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
      
        public Guid UserId { get; set; }
        public virtual  IList<Product> Products { get; set; }
        public Order()
        {
            Products = new List<Product>();
        }
    }
}

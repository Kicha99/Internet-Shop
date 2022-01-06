using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Category> Child { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Interfaces.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public override int GetHashCode()
        {
            return Id;
        }
        public override bool Equals(object obj)
        {
            ProductDTO o = (ProductDTO)obj;
            return o.Id == Id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Interfaces.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public IList<ProductDTO> Products { get; set; }
    }
}

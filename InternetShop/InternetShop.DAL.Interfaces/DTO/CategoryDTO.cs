using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Interfaces.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<CategoryDTO> Child { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}

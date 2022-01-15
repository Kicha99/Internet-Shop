using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public IEnumerable<Category> Child { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

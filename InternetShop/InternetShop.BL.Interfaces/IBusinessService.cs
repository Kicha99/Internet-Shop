using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BL.Interfaces
{
    public interface IBusinessService
    {
        IEnumerable<ModelCategory> GetCategories();
        IEnumerable<ModelProduct> GetProductsByCategoryId(int id);
        ModelProduct GetProductById(int id);
    }
}

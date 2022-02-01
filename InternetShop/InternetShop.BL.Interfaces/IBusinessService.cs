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
        IEnumerable<ModelOrder> GetOrders();
        IEnumerable<ModelCategory> GetChildCategoriesById(int id);
        void AddProduct(ModelProduct pr);
        void EditProduct(ModelProduct pr);
        ModelOrder GetOrderById(int orderId);
        void AddProductInOrder(ModelProduct product, ModelOrder order);
        IEnumerable<ModelProduct> SortProductsByPriceAscending(IEnumerable<ModelProduct> mp);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BL.Interfaces
{
    public interface IBusinessService
    {
        ModelCategory GetRootCategory();
        IEnumerable<ModelProduct> GetProductsByCategoryId(int id);
        ModelProduct GetProductById(int id);
        IEnumerable<ModelOrder> GetOrders();
        ModelCategory GetCategoryById(int id);
        void AddProduct(ModelProduct pr);
        void EditProduct(ModelProduct pr);
        ModelOrder GetOrderById(int orderId);
        ModelOrder GetOrderByUserId(Guid userId);
        void AddProductInOrder(ModelProduct product, ModelOrder order);
        IEnumerable<ModelProduct> SortProductsByPriceAscending(IEnumerable<ModelProduct> mp);
        void RemoveFromOrder(ModelProduct product, ModelOrder order);
        void Payment(Guid userId);
        IEnumerable<ModelProduct> GetBestFiveProducts();
    }
}

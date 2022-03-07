using InternetShop.DAL.Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Interfaces
{
    public interface IDataSource
    {
        void AddProduct(ProductDTO product);
        IEnumerable<ProductDTO> GetProductsByCategoryId(int id);
        ProductDTO GetProductById(int id);
        void EditProduct(ProductDTO product);
        void RemoveProductById(int id);
        void AddCategory(CategoryDTO category);
        CategoryDTO GetTopCategory();
        IEnumerable<CategoryDTO> GetChildCategoriesById(int id);
        void EditCategory(CategoryDTO category);
        void RemoveCategoryById(int id);
        void AddOrder(OrderDTO order);
        OrderDTO GetOrderById(int id);
        IEnumerable<OrderDTO> GetOrders();
        OrderDTO GetOrderByClient(Guid id);
        void EditOrder(OrderDTO order);

        void AddProductToOrder(OrderDTO order, ProductDTO product);
        void AddProductToCategory(CategoryDTO category, ProductDTO product);
        CategoryDTO GetCategoryById(int id);
        void RemoveProductFromOrder(ProductDTO product, OrderDTO order);

        IEnumerable<ProductDTO> GetAllProducts();
    }
}

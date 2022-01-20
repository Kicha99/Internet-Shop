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
        void AddProduct(ProductDTO product);//Create Product
        IEnumerable<ProductDTO> GetProductsByCategoryId(int id);//Read Products
        ProductDTO GetProductById(int id);//Read Product
        void EditProduct(ProductDTO product);//Update Product
        void RemoveProductById(int id);//Delete Product
        void AddCategory(CategoryDTO category);//Create Category
        IEnumerable<CategoryDTO> GetTopCategories();//Read TopCategories
        IEnumerable<CategoryDTO> GetChildCategoriesById(int id);//Read ChildCategories
        void EditCategory(CategoryDTO category);//Update Category
        void RemoveCategoryById(int id);//Delete Category
        void AddOrder(OrderDTO order);//Create Order
        OrderDTO GetOrderById(int id);//Read Order
        IEnumerable<OrderDTO> GetOrders();//Read orders
        IEnumerable<OrderDTO> GetOrdersByClient(int id);//Read OrdersByClientId
        void EditOrder(OrderDTO order);//Update Order

        void AddProductToOrder(OrderDTO order, ProductDTO product);
    }
}

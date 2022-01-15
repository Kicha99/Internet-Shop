﻿using InternetShop.DAL.Interfaces.DTO;
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
        void RemoveProductById(int id);
        void EditProduct(ProductDTO product);
        void AddCategory(CategoryDTO category);
        void RemoveCategoryById(int id);
        void EditCategory(CategoryDTO category);
        void AddOrder(OrderDTO order);
        void EditOrder(OrderDTO order);
        IEnumerable<ProductDTO> GetProductsByCategoryId(int id);
        IEnumerable<CategoryDTO> GetTopCategories();
        IEnumerable<CategoryDTO> GetChildCategoriesById(int id);
        IEnumerable<OrderDTO> GetOrders();
        IEnumerable<OrderDTO> GetOrdersByClient(int id);
    }
}

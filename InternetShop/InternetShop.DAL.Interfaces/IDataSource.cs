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
        void RemoveProduct(ProductDTO delProduct);
        void EditProduct(ProductDTO product);
        void AddCategory(CategoryDTO category);
        void RemoveCategory(CategoryDTO category);
        void AddOrder(OrderDTO order);
    }
}

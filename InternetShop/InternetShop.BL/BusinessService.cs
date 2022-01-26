using InternetShop.BL.Interfaces;
using InternetShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BL
{
    public class BusinessService : IBusinessService
    {
        private readonly IDataSource _ds;
        public BusinessService(IDataSource ds)
        {
            _ds = ds;
        }

        public IEnumerable<ModelCategory> GetCategories()
        {
            var categories = _ds.GetTopCategories();
            foreach (var item in categories)
            {
                yield return new ModelCategory()
                {
                    Id = item.Id,
                    Title = item.Title,
                    //Products = (from p in item.Products
                    //            where p != null
                    //            select new ModelProduct()
                    //            {
                    //                Id = p.Id,
                    //                Description = p.Description,
                    //                Price = p.Price,
                    //                Title = p.Title
                    //            }).ToList() //Null Reference Exception
                }; ;
            }
            //map to Model Category
        }

        public IEnumerable<ModelProduct> GetProductsByCategoryId(int id)
        {
            var products = _ds.GetProductsByCategoryId(id);
            foreach (var item in products)
            {
                yield return new ModelProduct()
                {
                    Id = item.Id,
                    Description = item.Description,
                    Price = item.Price,
                    Title = item.Title
                };
            }
        }
    }
}

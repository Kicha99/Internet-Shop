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
            //map to Model Category
        }
    }
}

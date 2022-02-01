using InternetShop.BL;
using InternetShop.BL.Interfaces;
using InternetShop.DAL;
using InternetShop.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InternetShop.CompositionRoot
{
    public static class LayersBuilder
    {
        public static  void Build(IServiceCollection services)
        {
            services.AddTransient<IBusinessService, BusinessService>();
            services.AddTransient<IDataSource, DataSource>();
        }
    }
}

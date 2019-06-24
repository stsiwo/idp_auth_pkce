using Autofac;
using CatalogApi.UI.Validators.ProductQueryString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.DI
{
    public class ProductQueryStringValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryKeyQueryStringValidator>().As<IProductQueryStringValidator>().InstancePerLifetimeScope();
            builder.RegisterType<KeyWordKeyQueryStringValidator>().As<IProductQueryStringValidator>().InstancePerLifetimeScope();
            builder.RegisterType<MaxPriceKeyQueryStringValidator>().As<IProductQueryStringValidator>().InstancePerLifetimeScope();
            builder.RegisterType<MinPriceKeyQueryStringValidator>().As<IProductQueryStringValidator>().InstancePerLifetimeScope();
            builder.RegisterType<RemoveUnspecifiedKeyQueryStringValidator>().As<IProductQueryStringValidator>().InstancePerLifetimeScope();
            builder.RegisterType<ReviewScoreKeyQueryStringValidator>().As<IProductQueryStringValidator>().InstancePerLifetimeScope();
            builder.RegisterType<SortKeyQueryStringValidator>().As<IProductQueryStringValidator>().InstancePerLifetimeScope();
            builder.RegisterType<SubCategoryKeyQueryStringValidator>().As<IProductQueryStringValidator>().InstancePerLifetimeScope();
        }
    }
}

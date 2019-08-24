using Autofac;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder.OrderClauseStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.DI
{
    public class OrderClauseStrategyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreationDateAscStrategy>()
               .Keyed<IOrderClauseStrategy>(SortConstants.DateAsc)
               .InstancePerLifetimeScope();

            builder.RegisterType<CreationDateDescStrategy>()
               .Keyed<IOrderClauseStrategy>(SortConstants.DateDesc)
               .InstancePerLifetimeScope();

            builder.RegisterType<PriceAscStrategy>()
               .Keyed<IOrderClauseStrategy>(SortConstants.PriceAsc)
               .InstancePerLifetimeScope();

            builder.RegisterType<PriceDescStrategy>()
               .Keyed<IOrderClauseStrategy>(SortConstants.PriceDesc)
               .InstancePerLifetimeScope();

            builder.RegisterType<NameAscStrategy>()
               .Keyed<IOrderClauseStrategy>(SortConstants.NameAsc)
               .InstancePerLifetimeScope();

            builder.RegisterType<NameDescStrategy>()
               .Keyed<IOrderClauseStrategy>(SortConstants.NameDesc)
               .InstancePerLifetimeScope();

            builder.RegisterType<ReviewNumbersAscStrategy>()
               .Keyed<IOrderClauseStrategy>(SortConstants.ReviewAsc)
               .InstancePerLifetimeScope();

            builder.RegisterType<ReviewNumbersDescStrategy>()
               .Keyed<IOrderClauseStrategy>(SortConstants.ReviewDesc)
               .InstancePerLifetimeScope();

            builder.RegisterType<ReviewScoreAscStrategy>()
               .Keyed<IOrderClauseStrategy>(SortConstants.ReviewScoreAsc)
               .InstancePerLifetimeScope();

            builder.RegisterType<ReviewScoreDescStrategy>()
               .Keyed<IOrderClauseStrategy>(SortConstants.ReviewScoreDesc)
               .InstancePerLifetimeScope();
        }

    }
}

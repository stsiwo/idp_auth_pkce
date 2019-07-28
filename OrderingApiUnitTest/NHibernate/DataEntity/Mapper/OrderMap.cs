using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using OrderingApiUnitTest.NHibernate.Entity.OrderAgg;
using OrderingApiUnitTest.NHibernate.CustomType;

namespace OrderingApiUnitTest.NHibernate.DataEntity.Mapper
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(o => o.Id)
                .CustomType<OrderIdUserType>();

            Map(o => o.Status).CustomType<OrderStatusConstants>();

            // create foreign key of user_id
            References(o => o.User)
                .Column("user_id");

            HasManyToMany<Product>(o => o.Products);
        }
    }
}

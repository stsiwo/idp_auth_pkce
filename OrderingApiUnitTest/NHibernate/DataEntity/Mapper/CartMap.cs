using FluentNHibernate.Mapping;
using OrderingApiUnitTest.NHibernate.CustomType;
using OrderingApiUnitTest.NHibernate.Entity.CartAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.DataEntity.Mapper
{
    class CartMap : ClassMap<Cart>
    {
        public CartMap()
        {
            Id(u => u.Id)
                .CustomType<CartIdUserType>();

            // foreign key "user_id" in cart
            References(c => c.User)
                .Column("user_id");

            HasMany<Product>(c => c.Products);
            
        }
    }
}

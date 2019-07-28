using FluentNHibernate.Mapping;
using OrderingApiUnitTest.NHibernate.CustomType;
using OrderingApiUnitTest.NHibernate.Entity.CartAgg;
using OrderingApiUnitTest.NHibernate.Entity.UserAgg;
using OrderingApiUnitTest.NHibernate.Entity.OrderAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.DataEntity.Mapper
{
    class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(u => u.Id)
                .CustomType<UserIdUserType>();

            Component<Name>(u => u.Name, n =>
            {
                n.Map(name => name.FirstName, "first_name");
                n.Map(name => name.LastName, "last_name");
            });

            Component<Address>(u => u.HomeAddress, a =>
            {
                a.Map(address => address.Street, "home_address_street");
                a.Map(address => address.City, "home_address_city");
                a.Map(address => address.State, "home_address_state");
                a.Map(address => address.Country, "home_address_country");
                a.Map(address => address.PostalCode, "home_address_postal_code");
            });
            
            Component<Phone>(u => u.Phone, p =>
            {
                p.Map(phone => phone.HomeNumber, "home_phone_number");
            });

            HasOne<Cart>(u => u.Cart)
                //.LazyLoad() 
                .PropertyRef(c => c.User);

            // keep Order records even if User is deleted
            HasMany<Order>(u => u.Orders);

        }
    }
}

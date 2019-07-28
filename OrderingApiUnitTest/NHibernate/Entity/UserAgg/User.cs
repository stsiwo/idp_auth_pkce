using OrderingApiUnitTest.NHibernate.Entity.Base;
using OrderingApiUnitTest.NHibernate.Entity.CartAgg;
using OrderingApiUnitTest.NHibernate.Entity.OrderAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.Entity.UserAgg
{
    public class User : IEntity, IAggregate
    {
        public virtual UserId Id { get; set; }
        public virtual Name Name { get; set; } 
        public virtual Address HomeAddress { get; set; } 
        public virtual Phone Phone { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual IList<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }

        public User(UserId id, Name name, Address homeAddress, Phone phone)
        {
            Id = id;
            Name = name;
            HomeAddress = homeAddress;
            Phone = phone;
        }
    }
}

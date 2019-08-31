using OrderingApi;
using OrderingApi.Domain.CartAgg;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace OrderingApiUnitTest.Infrastructure.DataBase
{
    public class NHibernateTest : InMemoryDatabaseTest
    {
        public NHibernateTest() : base(typeof(Program).Assembly)
        {
        }

        [Fact]
        public void CanSaveAndLoadBlog()
        {
            object id;

            using (var tx = session.BeginTransaction())
            {
                id = session.Save(new Cart
                {
                    Id = Guid.NewGuid(),
                    User = null,
                });

                tx.Commit();
            }

            session.Clear();


            using (var tx = session.BeginTransaction())
            {
                var cart = session.Get<Cart>(id);

                Assert.Equal(id, cart.Id);

                tx.Commit();
            }
        }
    }
}

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using Newtonsoft.Json;
using NHibernate.Tool.hbm2ddl;
using OrderingApiUnitTest.NHibernate.DataEntity.Mapper;
using OrderingApiUnitTest.NHibernate.Entity.CartAgg;
using OrderingApiUnitTest.NHibernate.Entity.UserAgg;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.NHibernate
{
    public class ConfigTest
    {
        private ITestOutputHelper _output;

        public ConfigTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void NHibernate_Config_Test()
        {
            var nhConfiguration = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.InMemory)
                .Mappings(m =>
                    m.FluentMappings
                    .Conventions.Setup(x => x.Add(AutoImport.Never()))
                    .AddFromAssemblyOf<INHMap>())
                .BuildConfiguration();


            var sessionFactory = nhConfiguration.BuildSessionFactory();

            using(var session = sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction())
            {
                // need to session.Connection in argument 
                var exporter = new SchemaExport(nhConfiguration);
                exporter.Execute(true, true, false, session.Connection, null);

                // Cart product
                Product cartProduct = new Product()
                {
                    Id = new ProductId(Guid.NewGuid()),
                    Name = "test_product_1",
                    Price = new Price(1000m, "$"),
                };

                IList<Product> cartProducts = new List<Product>();
                cartProducts.Add(cartProduct);

                // User
                User user = new User()
                {
                    Id = new UserId(Guid.NewGuid()),
                    Name = new Name("fn_1", "ln_1"),
                    HomeAddress = new Address("street", "city", "state", "country", "postal_code"),
                    Phone = new Phone("phone"),
                };

                // Cart
                Cart cart = new Cart()
                {
                    Id = new CartId(Guid.NewGuid()),
                    User = user,
                    Products = cartProducts,
                };

                IList<Cart> carts = new List<Cart>();
                carts.Add(cart);

                user.Cart = cart;
                cartProduct.Carts = carts;


                session.Save(user);

                session.Flush(); // synchronize in memory object with persistence (persistence => in memory) not vice versa
                session.Clear();

                User queryUser  = session.Get<User>(user.Id);

                _output.WriteLine(JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
                _output.WriteLine(JsonConvert.SerializeObject(queryUser, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));

                Assert.True(user.Id.Equals(queryUser.Id));
                tx.Commit();

                Assert.True(false);
            }


    //        var builder = new ContainerBuilder();

    //        // client
    //        builder.RegisterType<>().InstancePerLifetimeScope();

    //        using (var container = builder.Build())
    //        using (var scope = container.BeginLifetimeScope())
    //        {
    //            // dummy Test Aggregate
    //        }

        }

    }
}

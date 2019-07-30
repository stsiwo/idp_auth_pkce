using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using Newtonsoft.Json;
using NHibernate.Tool.hbm2ddl;
using OrderingApiUnitTest.NHibernate.DataEntity.Mapper;
using Cart = OrderingApiUnitTest.NHibernate.Entity.CartAgg;
using OrderingApiUnitTest.NHibernate.Entity.UserAgg;
using Order = OrderingApiUnitTest.NHibernate.Entity.OrderAgg;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

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
                .Database(SQLiteConfiguration.Standard.ConnectionString("data source=OrderingApiUnitTest.db;").ShowSql())
                .Mappings(m =>
                    m.FluentMappings
                    .Conventions.Setup(x => x.Add(AutoImport.Never()))
                    .AddFromAssemblyOf<INHMap>())
                .BuildConfiguration();

            // SQLITE CANNOT USE "DECLARE"
            //var tableNamesFromMappings = nhConfiguration.ClassMappings.Select(x => x.Table.Name);
            //var dropAllForeignKeysSql =
            //  @"
            //      DECLARE @cmd nvarchar(1000)
            //      DECLARE @fk_table_name nvarchar(1000)
            //      DECLARE @fk_name nvarchar(1000)

            //      DECLARE cursor_fkeys CURSOR FOR
            //      SELECT  OBJECT_NAME(fk.parent_object_id) AS fk_table_name,
            //              fk.name as fk_name
            //      FROM    sys.foreign_keys fk  JOIN
            //              sys.tables tbl ON tbl.OBJECT_ID = fk.referenced_object_id
            //      WHERE OBJECT_NAME(fk.parent_object_id) in ('" + String.Join("','", tableNamesFromMappings) + @"')

            //      OPEN cursor_fkeys
            //      FETCH NEXT FROM cursor_fkeys
            //      INTO @fk_table_name, @fk_name

            //      WHILE @@FETCH_STATUS=0
            //      BEGIN
            //        SET @cmd = 'ALTER TABLE [' + @fk_table_name + '] DROP CONSTRAINT [' + @fk_name + ']'
            //        exec dbo.sp_executesql @cmd

            //        FETCH NEXT FROM cursor_fkeys
            //        INTO @fk_table_name, @fk_name
            //      END
            //      CLOSE cursor_fkeys
            //      DEALLOCATE cursor_fkeys
            //    ;";

            var sessionFactory = nhConfiguration.BuildSessionFactory();

            using(var session = sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction())
            {
                // SQLITE can not use "DECLARE"
               // var command = session.Connection.CreateCommand();
               // command.CommandText = dropAllForeignKeysSql;
               // command.ExecuteNonQuery();
                // need to session.Connection in argument 
                var exporter = new SchemaExport(nhConfiguration);
                exporter.Execute(true, true, false, session.Connection, null);

                // Cart product
                Cart::Product cartProduct = new Cart::Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_product_1",
                    Price = new Cart::Price(1000m, "$"),
                };

                IList<Cart::Product> cartProducts = new List<Cart::Product>();
                cartProducts.Add(cartProduct);

                // Order product
                Order::Product orderProduct = new Order::Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_product_1",
                    Price = new Order::Price(1000m, "$"),
                };

                IList<Order::Product> orderProducts = new List<Order::Product>();
                orderProducts.Add(orderProduct);

                // User
                User user = new User()
                {
                    Id = Guid.NewGuid(),
                    Name = new Name("fn_1", "ln_1"),
                    HomeAddress = new Address("street", "city", "state", "country", "postal_code"),
                    Phone = new Phone("phone"),
                };

                // Cart
                Cart::Cart cart = new Cart::Cart()
                {
                    Id = Guid.NewGuid(),
                    User = user,
                    Products = cartProducts,
                };

                IList<Cart::Cart> carts = new List<Cart::Cart>
                {
                    cart
                };

                user.Cart = cart;
                cartProduct.Carts = carts;

                // Order
                Order::Order order = new Order::Order()
                {
                    Id = Guid.NewGuid(),
                    Status = Order::OrderStatusConstants.PENDING,
                    User = user,
                    Products = orderProducts
                };

                IList<Order::Order> orders = new List<Order::Order>
                {
                    order
                };

                //user.Orders = orders;
                //orderProduct.Orders = orders;

                _output.WriteLine(JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));

                session.Save(user);
                tx.Commit();

                session.Flush(); // synchronize in memory object with persistence (persistence => in memory) not vice versa
                session.Clear();

                User queryUser  = session.Get<User>(user.Id);

                _output.WriteLine(JsonConvert.SerializeObject(queryUser, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));

                Assert.True(user.Id.Equals(queryUser.Id));

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

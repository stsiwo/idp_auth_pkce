using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Cart = OrderingApi.Domain.CartAgg;
using Order = OrderingApi.Domain.OrderAgg;
using OrderingApi.Domain.UserAgg;

namespace OrderingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ValuesController));

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            log.Info("log4net configured correctly");

            var nhConfig = new Configuration().Configure();
            var sessionFactory = nhConfig.BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                new SchemaExport(nhConfig).Execute(true, true, false, session.Connection, null);
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
                };

                // Cart::CartProduct
                Cart::CartProduct cP = new Cart::CartProduct()
                {
                    Id = Guid.NewGuid(),
                    Name = new Cart::ProductName("full_name"),
                    Description = new Cart::ProductDescription("full_description"),
                    MainImageUrl = new Cart::ProductMainImageUrl("main_image_url"),
                    Price = new Cart::ProductPrice(1000m),
                    Stock = new Cart::ProductStock(4,2),
                };

                cart.Products.Add(cP);
                user.Cart = cart;

                // Cart
                Order::Order order = new Order::Order()
                {
                    Id = Guid.NewGuid(),
                };

                // OrderProduct
                Order::OrderProduct oP = new Order::OrderProduct()
                {
                    Id = Guid.NewGuid(),
                    Name = new Order::ProductName("full_name"),
                    Price = new Order::ProductPrice(1000m),
                    Stock = new Order::ProductStock(4,2),
                };

                order.User = user;
                order.Products.Add(oP);
                user.Orders.Add(order);


                session.Save(user);

                session.Flush();
                session.Clear();


                log.Debug(JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));

                tx.Commit();

            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

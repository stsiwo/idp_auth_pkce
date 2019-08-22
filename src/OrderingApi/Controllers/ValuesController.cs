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
using System.Threading;
using MediatR;
using OrderingApi.UI.Command;
using System.Transactions;
using OrderingApi.Domain.Base;
using OrderingApi.Application.Repository;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.Repository.StoredEventTranslator;
using OrderingApi.Infrastructure.RabbitMQ.Message;
using Newtonsoft.Json.Linq;
using OrderingApi.Infrastructure.Repository;
using NHibernate;
using OrderingApi.Infrastructure.Repository.MessageStorage.Publishing;

namespace OrderingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ValuesController));

        private IMediator _mediator;

        private IEventStore _eventStore;

        private IStoredEventTranslator _storedEventTranslator;

        private IPublishedMessageStore _publishedMessageStore;

        private ISession _session;

        public ValuesController(IMediator mediator, IEventStore eventStore, IStoredEventTranslator storedEventTranslator, IPublishedMessageStore publishedMessageStore, ISession session)
        {
            _mediator = mediator;
            _eventStore = eventStore;
            _storedEventTranslator = storedEventTranslator;
            _publishedMessageStore = publishedMessageStore;
            _session = session;
        }

        //GET api/values/updatedb
        // use this url when you modify table or database to sync
        [HttpGet]
        public ActionResult<string> Get()
        {
            log.Debug("start sending command ...");

            _mediator.Send(new CreateCartCommand("test-command"));

            log.Debug("start sending response ...");

            return "hey"; 
        }

        [HttpGet("mst")]
        public ActionResult<string> MultiSessionTest()
        {
            var nhConfig = new Configuration().Configure();
            var sessionFactory = nhConfig.BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                RmqPublishMessage msg = session.Get<RmqPublishMessage>(Guid.Parse("c7cc9aae-0c81-433d-97cd-b827db8b49ca"));
                msg.Sender = "update-1";
                tx.Commit();
            }

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                RmqPublishMessage msg = session.Get<RmqPublishMessage>(Guid.Parse("c7cc9aae-0c81-433d-97cd-b827db8b49ca"));
                msg.Sender = "update-2";
                tx.Commit();
            }

            return "multi session test";
        }

        [HttpGet("mtt")]
        public ActionResult<string> MultiThreadsTest()
        {
            var t1 = Task.Run(() => DBWork1(_session));
            var t2 = Task.Run(() => DBWork2(_session));

            t1.Wait();
            t2.Wait();

            return "multi session test";
        }

        private void DBWork1(ISession session)
        {
            using (var tx = session.BeginTransaction())
            {
                RmqPublishMessage msg = session.Get<RmqPublishMessage>(Guid.Parse("c7cc9aae-0c81-433d-97cd-b827db8b49ca"));
                Thread.Sleep(1000);
                msg.Sender = "first-update"; 
                tx.Commit();
            }
        }

        private void DBWork2(ISession session)
        {
            using (var tx = session.BeginTransaction())
            {
                RmqPublishMessage msg = session.Get<RmqPublishMessage>(Guid.Parse("c7cc9aae-0c81-433d-97cd-b827db8b49ca"));
                msg.Sender = "second-update"; 
                tx.Commit();
            }
        }
        //GET api/values/updatedb
        // use this url when you modify table or database to sync
        [HttpGet("updatedb")]
        public ActionResult<string> UpdateDb()
        {
            var nhConfig = new Configuration().Configure();
            var sessionFactory = nhConfig.BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                new SchemaExport(nhConfig).Execute(true, true, false, session.Connection, null);

                log.Debug("start sending command ...");

                //_mediator.Send(new CreateCartCommand("test-command"));

                log.Debug("start sending response ...");
                tx.Commit();
            }
            return "hey"; 
        }

        //GET api/values/event
        [HttpGet("event")]
        public ActionResult<IEnumerable<string>> Event()
        {
            var nhConfig = new Configuration().Configure();
            var sessionFactory = nhConfig.BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                new SchemaExport(nhConfig).Execute(true, true, false, session.Connection, null);

                IDomainEvent domainEvent = new CartCreatedDomainEvent()
                {
                    DomainEventId = Guid.NewGuid().ToString(),
                    DomainEventType = (int)DomainEventTypeConstants.CartCreatedDomainEvent,
                    DomainEventName = "CartCreated",
                    OccurredOn = DateTime.Now,
                    CartId = "test-cart-id"
                };
                session.Save(_storedEventTranslator.TranslateToStoredEvent(domainEvent));
                tx.Commit();
            }
            return new string[] { "value1", "value2"};
        }

        //GET api/values/event
        [HttpGet("msg")]
        public ActionResult<string> Msg()
        {
            var nhConfig = new Configuration().Configure();
            var sessionFactory = nhConfig.BuildSessionFactory();
            string result = "";

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                new SchemaExport(nhConfig).Execute(true, true, false, session.Connection, null);

                IDomainEvent domainEvent = new CartCreatedDomainEvent()
                {
                    DomainEventId = Guid.NewGuid().ToString(),
                    DomainEventType = (int)DomainEventTypeConstants.CartCreatedDomainEvent,
                    DomainEventName = "CartCreated",
                    OccurredOn = DateTime.Now,
                    CartId = "test-cart-id"
                };

                RmqPublishMessage msg = new RmqPublishMessage()
                {
                    MessageId = Guid.NewGuid(),
                    DomainEventType = 0,
                    Sender = "test",
                    OccurredOn = DateTime.Now,
                    Content = JObject.FromObject(domainEvent),
                    DeliveryTag = 1,
                    Status = MessageStatusConstants.Failed
                };
                session.Save(msg);

                session.Flush();
                session.Clear();

                RmqPublishMessage found = session.Get<RmqPublishMessage>(msg.MessageId);
                result = JsonConvert.SerializeObject(found, Formatting.Indented);

                tx.Commit();
            }
            return result; 
        }

        //GET api/values/msg/read/dt
        [HttpGet("msg/read/dt")]
        public ActionResult<string> Read()
        {
            RmqPublishMessage found = _publishedMessageStore.GetByDeliveryTag(1);
            return JsonConvert.SerializeObject(found, Formatting.Indented); 
        }

        //GET api/values/msg/read/dt
        [HttpGet("msg/read/id")]
        public ActionResult<string> GetById()
        {
            RmqPublishMessage msg2 = _publishedMessageStore.GetByMessageId(Guid.Parse("c742759a-dbe1-4f54-a97a-dd96feb5deb7"));

            return JsonConvert.SerializeObject(msg2, Formatting.Indented); 
        }

        //GET api/values/msg/read/dt
        [HttpGet("msg/update")]
        public ActionResult<string> Update()
        {
            RmqPublishMessage found = _publishedMessageStore.GetByDeliveryTag(1);

            found.Status = MessageStatusConstants.Success;
            found.Sender = "updated sender";

            _publishedMessageStore.Update(found);

            RmqPublishMessage updated = _publishedMessageStore.GetByMessageId(found.MessageId);
            
            return JsonConvert.SerializeObject(updated, Formatting.Indented); 
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            Thread thread = Thread.CurrentThread;

            log.Debug("Controller Thread Id: " + thread.ManagedThreadId);
            var result = await DoAsync();

            Thread threadafter = Thread.CurrentThread;

            log.Debug("Controller Thread Id: " + threadafter.ManagedThreadId);

            return "value";
        }

        private async Task<int> DoAsync()
        {
            return await Task.Run<int>( () => 
            {
                for (int i = 0; i<10; i++)
                {
                    
                }
                return 1;

            });
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

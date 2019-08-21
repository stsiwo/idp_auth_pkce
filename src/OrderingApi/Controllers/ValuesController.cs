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

        private IMessageStore _messageStore;

        public ValuesController(IMediator mediator, IEventStore eventStore, IStoredEventTranslator storedEventTranslator, IMessageStore messageStore)
        {
            _mediator = mediator;
            _eventStore = eventStore;
            _storedEventTranslator = storedEventTranslator;
            _messageStore = messageStore;
        }

        //GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {

            log.Debug("start sending command ...");
            _mediator.Send(new CreateCartCommand("test-command"));

            log.Debug("start sending response ...");
            return nameof(MessageStatusConstants.Success); 
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

                RmqMessage msg = new RmqMessage()
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

                RmqMessage found = session.Get<RmqMessage>(msg.MessageId);
                result = JsonConvert.SerializeObject(found, Formatting.Indented);

                tx.Commit();
            }
            return result; 
        }

        //GET api/values/msg/read/dt
        [HttpGet("msg/read/dt")]
        public ActionResult<string> Read()
        {
            RmqMessage found = _messageStore.GetByDeliveryTag(1);
            return JsonConvert.SerializeObject(found, Formatting.Indented); 
        }

        //GET api/values/msg/read/dt
        [HttpGet("msg/read/id")]
        public ActionResult<string> GetById()
        {
            RmqMessage msg2 = _messageStore.GetByMessageId(Guid.Parse("c742759a-dbe1-4f54-a97a-dd96feb5deb7"));

            return JsonConvert.SerializeObject(msg2, Formatting.Indented); 
        }

        //GET api/values/msg/read/dt
        [HttpGet("msg/update")]
        public ActionResult<string> Update()
        {
            RmqMessage found = _messageStore.GetByDeliveryTag(1);

            found.Status = MessageStatusConstants.Success;
            found.Sender = "updated sender";

            _messageStore.Update(found);

            RmqMessage updated = _messageStore.GetByMessageId(found.MessageId);
            
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

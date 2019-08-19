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

        public ValuesController(IMediator mediator, IEventStore eventStore, IStoredEventTranslator storedEventTranslator)
        {
            _mediator = mediator;
            _eventStore = eventStore;
            _storedEventTranslator = storedEventTranslator;
        }

        //GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            log.Debug("start sending command ...");
            _mediator.Send(new CreateCartCommand("test-command"));

            log.Debug("start sending response ...");
            return new string[] { "value1", "value2"};
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

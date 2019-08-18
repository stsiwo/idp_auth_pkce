using log4net;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent
{
    public class CartCreatedDomainEvent : DomainEventBase, INotification 
    {
        public string CartId { get; set; }
    }
}

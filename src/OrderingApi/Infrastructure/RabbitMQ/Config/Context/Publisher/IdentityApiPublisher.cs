using OrderingApi.Infrastructure.RabbitMQ.Config.EventHandler;
using OrderingApi.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher
{
    public class IdentityApiPublisher : PublisherBase
    {
        public IdentityApiPublisher
            (
                IMessageStore messageStore
                , UpdateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker updateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker
                , UpdateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker updateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker
                , StoreUnroutableMessageWhenPublisherGetReturnedMessageFromBroker storeUnroutableMessageWhenPublisherGetReturnedMessageFromBroker
            )
        : base
            (
                ExchangeNameConstants.IdentityApiPublisherExchange
                , RoutingKeyConstants.ToIdentityApi
                , messageStore
                , updateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker 
                , updateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker
                , storeUnroutableMessageWhenPublisherGetReturnedMessageFromBroker
            )
        {
        }
    }
}

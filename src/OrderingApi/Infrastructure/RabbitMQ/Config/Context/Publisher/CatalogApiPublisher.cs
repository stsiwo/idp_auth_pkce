using OrderingApi.Infrastructure.RabbitMQ.Config.EventHandler;
using OrderingApi.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher
{
    public class CatalogApiPublisher : PublisherBase
    {
        public CatalogApiPublisher
            (
                IMessageStore messageStore
                , UpdateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker updateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker
                , UpdateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker updateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker
                , StoreUnroutableMessageWhenPublisherGetReturnedMessageFromBroker storeUnroutableMessageWhenPublisherGetReturnedMessageFromBroker
            )
        : base
            (
                ExchangeNameConstants.CatalogApiPublisherExchange
                , RoutingKeyConstants.ToCatalogApi
                , messageStore
                , updateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker 
                , updateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker
                , storeUnroutableMessageWhenPublisherGetReturnedMessageFromBroker
            )
        {
        }
    }
}

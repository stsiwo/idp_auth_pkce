using OrderingApi.Infrastructure.RabbitMQ.Config.EventHandler;
using OrderingApi.Infrastructure.Repository;
using OrderingApi.Infrastructure.Repository.MessageStorage.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher
{
    public class OrderingApiPublisher : PublisherBase, ICurrentPublisher
    {
        public OrderingApiPublisher
            (
                IPublishedMessageStore publishedMessageStore
                , UpdateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker updateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker
                , UpdateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker updateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker
                , StoreUnroutableMessageWhenPublisherGetReturnedMessageFromBroker storeUnroutableMessageWhenPublisherGetReturnedMessageFromBroker
            )
        : base
            (
                ExchangeNameConstants.OrderingApiPublisherExchange
                , RoutingKeyConstants.ToOrderingApi
                , publishedMessageStore
                , updateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker 
                , updateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker
                , storeUnroutableMessageWhenPublisherGetReturnedMessageFromBroker
            )

        {
        }
    }
}

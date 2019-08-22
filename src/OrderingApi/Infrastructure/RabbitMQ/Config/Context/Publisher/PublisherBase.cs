using OrderingApi.Infrastructure.RabbitMQ.Config.EventHandler;
using OrderingApi.Infrastructure.Repository;
using OrderingApi.Infrastructure.Repository.MessageStorage.Publishing;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher
{
    // #CONTEXT
    public class PublisherBase : IPublisher
    {
        public string ExchangeName { get; }

        public string RoutingKey { get; }

        private readonly IPublishedMessageStore _publishedMessageStore;

        private UpdateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker _updateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker;

        private UpdateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker _updateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker;

        private StoreUnroutableMessageWhenPublisherGetReturnedMessageFromBroker _storeUnroutableMessageWhenPublisherGetReturnedMessageFromBroker;

        public PublisherBase(string exchangeName, 
            string routingKey, 
            IPublishedMessageStore publishedMessageStore, 
            UpdateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker updateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker,
            UpdateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker updateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker,
            StoreUnroutableMessageWhenPublisherGetReturnedMessageFromBroker storeUnroutableMessageWhenPublisherGetReturnedMessageFromBroker)
        {
            ExchangeName = exchangeName;
            RoutingKey = routingKey;
            _publishedMessageStore = publishedMessageStore;
            _updateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker = updateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker;
            _updateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker = updateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker;
            _storeUnroutableMessageWhenPublisherGetReturnedMessageFromBroker = storeUnroutableMessageWhenPublisherGetReturnedMessageFromBroker;
        }
        public void Configure(IModel channel)
        {
            EnablePublisherConfirm(channel);

            // "pc acks" event handler
            SubscribePublisherConfirmAcksReceivedFromBrokerEvent(channel);

            // "pc nacks" event handler
            SubscribePublisherConfirmNacksReceivedFromBrokerEvent(channel);

            // "basic.return" event handler
            SubscribePublisherGetReturnedMessageFromBrokerEvent(channel);

            DeclareExchangeIn(channel);
        }

        public void DeclareExchangeIn(IModel channel)
        {
            channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeTypeConstants.Topic);
        }

        private void EnablePublisherConfirm(IModel channel)
        {
            channel.ConfirmSelect();
        }

        // event handler of "BasicAcksEvent": signal of that broker could successfuly handle the message (Publisher Confirm Ack)
        private void SubscribePublisherConfirmAcksReceivedFromBrokerEvent(IModel channel)
        {
            channel.BasicAcks += ((sender, e) =>
            {
                _updateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker.Handler(sender, e, _publishedMessageStore);
            });
        }

        // envet handler of "BasicNacksEvent": signal of that broker could NOT handle message 
        private void SubscribePublisherConfirmNacksReceivedFromBrokerEvent(IModel channel)
        {
            channel.BasicNacks += ((sender, e) =>
            {
                _updateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker.Handler(sender, e, _publishedMessageStore);
            });
        }

        // envet handler of "BasicReturnEvent": signal of that there was no routable message (no match of RoutingKey); no one receive this message 
        // * must "mandatory" flag = true
        private void SubscribePublisherGetReturnedMessageFromBrokerEvent(IModel channel)
        {
            channel.BasicReturn += ((sender, e) =>
            {
                _storeUnroutableMessageWhenPublisherGetReturnedMessageFromBroker.Handler(sender, e, _publishedMessageStore);
            });
        }

        // also need to impl re-send message when broker return nack to this publisher client
        // #REFACTOR
    }
}

using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.EventHandler
{
    public class UpdateDomainEventStatusToSuccessWhenPublisherReceivedConfirmFromBroker
    {
        void Handler(object sender, BasicAckEventArgs e)
        {
            // use MessageStore Repository to update rmqMessage
            // need id of Message or some ways to identify the message 
            // also BasicAckEvntArgs contains "deliveryTag" but the problem is how to identify the message by its deliverytag??
            /// is there any way to get the message using the delivery tag??
        }
    }
}

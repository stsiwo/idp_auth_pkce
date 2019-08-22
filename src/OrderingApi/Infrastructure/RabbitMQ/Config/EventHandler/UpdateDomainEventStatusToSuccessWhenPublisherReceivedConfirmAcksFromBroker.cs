using OrderingApi.Infrastructure.RabbitMQ.Message;
using OrderingApi.Infrastructure.Repository;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.EventHandler
{
    public class UpdateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker
    {
        public void Handler(object sender, BasicAckEventArgs e, IMessageStore messageStore)
        {
            using(var tx = messageStore.BeginTransaction())
            {
                RmqMessage targetMessage = messageStore.GetByDeliveryTag(e.DeliveryTag);

                targetMessage.Status = MessageStatusConstants.Success;

                messageStore.Commit(tx);
            }
        }
    }
}

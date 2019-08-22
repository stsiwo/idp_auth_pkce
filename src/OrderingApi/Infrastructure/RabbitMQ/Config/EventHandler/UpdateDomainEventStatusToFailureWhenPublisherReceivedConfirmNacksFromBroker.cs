﻿using OrderingApi.Infrastructure.RabbitMQ.Message;
using OrderingApi.Infrastructure.Repository;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.EventHandler
{
    public class UpdateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker
    {
        public void Handler(object sender, BasicNackEventArgs e, IPublishedMessageStore publishedMessageStore)
        {
            using(var tx = publishedMessageStore.BeginTransaction())
            {
                RmqMessage targetMessage = publishedMessageStore.GetByDeliveryTag(e.DeliveryTag);

                targetMessage.Status = MessageStatusConstants.Failed;

                publishedMessageStore.Commit(tx);
            }
        }
    }
}

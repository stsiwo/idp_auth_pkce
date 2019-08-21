using Newtonsoft.Json;
using OrderingApi.Infrastructure.RabbitMQ.Message;
using OrderingApi.Infrastructure.Repository;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.EventHandler
{
    public class StoreUnroutableMessageWhenPublisherGetReturnedMessageFromBroker 
    {
        public void Handler(object sender, BasicReturnEventArgs e, IMessageStore messageStore)
        {
            string body = Encoding.UTF8.GetString(e.Body);

            RmqMessage returnedMessage = JsonConvert.DeserializeObject<RmqMessage>(body);

            returnedMessage.Status = MessageStatusConstants.Unroutable;
            returnedMessage.StatusReason = e.ReplyText;

            messageStore.Update(returnedMessage);
        }
    }
}

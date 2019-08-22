using Newtonsoft.Json;
using OrderingApi.Infrastructure.RabbitMQ.Message;
using OrderingApi.Infrastructure.Repository;
using OrderingApi.Infrastructure.Repository.MessageStorage.Publishing;
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
        public void Handler(object sender, BasicReturnEventArgs e, IPublishedMessageStore publishedMessageStore)
        {
            using(var tx = publishedMessageStore.BeginTransaction())
            {
                string body = Encoding.UTF8.GetString(e.Body);

                RmqPublishMessage returnedMessage = JsonConvert.DeserializeObject<RmqPublishMessage>(body);

                RmqPublishMessage foundMessage = publishedMessageStore.GetByMessageId(returnedMessage.MessageId);

                foundMessage.Status = MessageStatusConstants.Unroutable;
                foundMessage.StatusReason = e.ReplyText;

                publishedMessageStore.Update(foundMessage);

                publishedMessageStore.Commit(tx);
            }
        }
    }
}

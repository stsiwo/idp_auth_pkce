using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Message
{
    public enum MessageStatusConstants
    {
        PendingSend, // have not sent the message yet
        PendingResponse, // sent the message, waiting for an ack
        Success, // ack received
        Failed, // nack received
        Unroutable, // message returned
        NoExchangeFound, // 404 reply code
        Processed, // consumer processed this message
        NotProcessed, // consumer could not process this message
    }
}

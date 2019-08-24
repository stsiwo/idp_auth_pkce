using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config
{
    public static class ExchangeTypeConstants
    {
        public static string Topic = "topic";
        public static string Direct = "direct";
        public static string Fanout = "fanout";
    }
}

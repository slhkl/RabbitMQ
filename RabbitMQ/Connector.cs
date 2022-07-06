using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class Connector
    {
        public static IConnection CreateConnection()
        {
            string uri = Environment.GetEnvironmentVariable("RabbitMQUri");

            var factory = new ConnectionFactory()
            {
                Uri = new Uri(uri)
            };

            return factory.CreateConnection();
        }
    }
}

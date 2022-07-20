using RabbitMQ.Client;

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

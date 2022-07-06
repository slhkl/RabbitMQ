using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class ReceiveQueque
    {
        public static void ReceiveFromQueque()
        {
            string quequeName = Environment.GetEnvironmentVariable("QuequeName");

            using (var channel = Connector.CreateConnection().CreateModel())
            {
                channel.QueueDeclare(
                    queue: quequeName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    string message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"{quequeName} kuyruğundan {message} geldi.");
                };
                channel.BasicConsume(quequeName, true, quequeName, false, false , null, consumer);
                Console.ReadLine();
            }
        }
    }
}

using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ
{
    public class ReceiveQueque
    {
        public static async Task ReceiveFromQueque()
        {
            string quequeName = Environment.GetEnvironmentVariable("QuequeName");

            using (var connection = Connector.CreateConnection())
            {
                using (var channel = connection.CreateModel())
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
                    channel.BasicConsume(quequeName, true, quequeName, false, false, null, consumer);

                    await Task.Delay(-1);
                }
            }
        }
    }
}

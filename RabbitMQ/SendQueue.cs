using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQData.Model;
using System.Text;

namespace RabbitMQ
{
    public class SendQueue
    {
        public static void SendToQueue(User user)
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

                    string userToString = JsonConvert.SerializeObject(user);
                    byte[] stringToByte = Encoding.UTF8.GetBytes(userToString);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: quequeName,
                        basicProperties: null,
                        body: stringToByte
                    );
                }
            }
        }
    }
}
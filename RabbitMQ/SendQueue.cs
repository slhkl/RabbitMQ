using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQData.Dto;
using RabbitMQData.Model;
using System.Text;

namespace RabbitMQ
{
    public class SendQueue
    {
        public static void SendToQueue(User user)
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
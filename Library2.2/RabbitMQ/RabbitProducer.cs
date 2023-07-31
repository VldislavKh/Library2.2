using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Library2._2.RabbitMQ
{
    public class RabbitProducer : IRabbitProducer
    {
        public void SendBookMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "rmuser",
                Password = "rmpassword"
                //Ssl =
                //{
                //    ServerName = "rabbitmq"
                //}
            };

            var connection = factory.CreateConnection();

            using var chanel = connection.CreateModel();

            chanel.QueueDeclare("book", exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            chanel.BasicPublish(exchange: "ex1", routingKey: "book", body: body);
        }
    }
}

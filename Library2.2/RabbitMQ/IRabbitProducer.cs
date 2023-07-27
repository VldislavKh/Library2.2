namespace Library2._2.RabbitMQ
{
    public interface IRabbitProducer
    {
        public void SendBookMessage<T>(T message);
    }
}

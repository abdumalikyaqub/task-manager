namespace Users.Microservice.Rabbitmq
{
    public interface IRabbitMQService
    {
        void SendMessage(object obj);
        void SendMessage(string message);
    }
}

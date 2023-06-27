using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Diagnostics;
using System;
using Tasks.Microservice.Domain;
using System.Text.Json;
using Newtonsoft.Json;

namespace Tasks.Microservice.Services.RabbitMQ
{
    public class RabbitMqListener: BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private TasksService tasksService = new TasksService();

        public RabbitMqListener()
        {
            // Не забудьте вынести значения "localhost" и "MyQueue"
            // в файл конфигурации
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "MyQueue", 
                durable: false, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                // Каким-то образом обрабатываем полученное сообщение
                // Debug.WriteLine($"Получено сообщение: {content}");

                User? person = JsonConvert.DeserializeObject<User>(content);
                //var userdata = JsonSerializer.Serialize(content);
                //var options = new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true
                //};
                //User user = JsonSerializer.DeserializeAsync<User>(content, options);
                Console.WriteLine($"Получено сообщение: {content}");
                var isCreated = tasksService.AddUser(person);
                if (isCreated.Result)
                {
                    Console.WriteLine($"Добавлено: {person?.Fullname}");
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("MyQueue", false, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}

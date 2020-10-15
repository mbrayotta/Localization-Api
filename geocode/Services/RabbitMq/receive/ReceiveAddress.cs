using Domain;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.RabbitMq.Options;
using Services.OpenStreetMap;
using System;

namespace Services.RabbitMq.receive
{
    public class ReceiveAddress : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly IOpenStreetMapService _openStreetMapService;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private readonly int _port;

        public ReceiveAddress(IOpenStreetMapService openStreetMapService, IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _port = rabbitMqOptions.Value.Port;
            _openStreetMapService = openStreetMapService;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            Console.WriteLine($"amqp://{_username}:{_password}@{_hostname}:{_port}/");
            var factory = new ConnectionFactory() 
            { 
                Uri = new Uri($"amqp://{_username}:{_password}@{_hostname}:{_port}/")
            };

            try {

                _connection = factory.CreateConnection();
            
            } catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException e) {
                System.Console.WriteLine(e);
            }

            
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var address = JsonConvert.DeserializeObject<Address>(content);

                HandleMessage(address);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(Address address)
        {
            _openStreetMapService.ConsumerOpenStreetMap(address);
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
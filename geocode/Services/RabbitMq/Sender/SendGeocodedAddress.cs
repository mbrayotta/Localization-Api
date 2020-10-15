using Domain;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Services.RabbitMq.Options;
using Newtonsoft.Json;
using System.Text;
using System;

namespace Services.RabbitMq.Sender
{
    public class SendGeocodedAddress : ISendGeocodedAddress
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private readonly int _port;
        public SendGeocodedAddress(IOptions<RabbitMqConfigurationSender> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _port = rabbitMqOptions.Value.Port;
        }

        public void SendGeocodedAddres(GeocodedAddress gaddress)
        {
            var factory = new ConnectionFactory() 
            { 
                Uri = new Uri($"amqp://{_username}:{_password}@{_hostname}:{_port}")
            };
            
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var json = JsonConvert.SerializeObject(gaddress);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
        }
    }
}
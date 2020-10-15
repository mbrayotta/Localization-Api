namespace Services.RabbitMq.Options
{
    public class RabbitMqConfigurationSender
    {
        public string Hostname { get; set; }

        public string QueueName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
  
        public int Port { get; set; }
    }
}
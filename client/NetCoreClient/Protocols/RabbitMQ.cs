using System;
using System.Text;
using RabbitMQ.Client;


namespace NetCoreClient.Protocols
{
    internal class RabbitMQ : IProtocolInterface, IDisposable
    {
        private const string EXCHANGE_NAME = "exchange_water_coolers";
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQ(string endpoint)
        {
            _factory = new ConnectionFactory()
            {
                Uri = new Uri(endpoint)
            };

            // Usa il metodo sincrono di connessione
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "water_coolers", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: ExchangeType.Topic);
        }

        public void Send(string data, string sensor)
        {
            var body = Encoding.UTF8.GetBytes(data);
            var routingKey = $"water_coolers.123.data.{sensor}";

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(
                exchange: EXCHANGE_NAME,
                routingKey: routingKey,
                basicProperties: properties,
                body: body
            );

            Console.WriteLine($" [x] Sent '{routingKey}':'{data}'");
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
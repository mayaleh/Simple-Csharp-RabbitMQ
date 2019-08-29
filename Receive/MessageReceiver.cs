using RabbitMQ.Client;
using System;
using System.Text;

namespace Receive
{
    public class MessageReceiver : DefaultBasicConsumer
    {
        private readonly IModel _channel;

        public MessageReceiver(IModel channel)

        {
            _channel = channel;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)

        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Consuming Message");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(string.Concat("Message received from the exchange ", exchange));
            Console.WriteLine(string.Concat("Consumer tag: ", consumerTag));
            Console.WriteLine(string.Concat("Delivery tag: ", deliveryTag));
            Console.WriteLine(string.Concat("Routing tag: ", routingKey));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Message: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(Encoding.UTF8.GetString(body));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("=======================================");
            Console.ForegroundColor = ConsoleColor.Gray;

            _channel.BasicAck(deliveryTag, false);

        }
    }
}

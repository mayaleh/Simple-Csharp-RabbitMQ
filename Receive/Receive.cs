using RabbitMQ.Client;
using System;

namespace Receive
{
    class Program
    {
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string HostName = "localhost";
        private const int Port = 5672;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Consumer app! Což je Příjemce. Press [enter] to exit.");
            Console.ForegroundColor = ConsoleColor.Gray;
            CustomConsum();
            Console.ReadLine();
            /*
            Asynchroní zpracování všech zpráv najednou. Probíhá v Eventu přidaném do consumer.Received
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var queue = channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    Console.WriteLine(queue.MessageCount);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] {0} Received -> {1}", DateTime.Now, message);
                    };

                    channel.BasicConsume(queue: "hello",
                                         autoAck: true,
                                         consumer: consumer);

                }
            }

            */

            Console.ReadLine();
        }


        static void CustomConsum()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory

            {
                HostName = HostName,
                Port = Port,
                UserName = UserName,
                Password = Password,

            };

            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            // přimi pouze jednu zprávu
            channel.BasicQos(0, 1, false);
            MessageReceiver messageReceiver = new MessageReceiver(channel);
            channel.BasicConsume("hello", false, messageReceiver);
        }




    }
}

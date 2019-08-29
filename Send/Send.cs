using RabbitMQ.Client;
using System;
using System.Text;

namespace Send
{
    class Program
    {
        static string ExitUserCode = "exitPRG";
        static void Main(string[] args)
        {
            Console.WriteLine("Preceder app neboli odesílatel");

            string exitProgram = "";
            while (exitProgram != ExitUserCode)
            {

                Console.Write("Write ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("exitPRG");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" and press [enter] to exit. Write a message to send:");
                Console.WriteLine();
                exitProgram = Console.ReadLine();
                if (!String.IsNullOrEmpty(exitProgram) && exitProgram != ExitUserCode)
                {
                    Publish(exitProgram);
                }
            }
        }

        static void Publish(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}

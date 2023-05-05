using System.Text;
using AndreTurismAPP.Services;
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

internal class Program
{
    private static void Main(string[] args)
    {
        const string QUEUE_NAME = "message";

        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QUEUE_NAME,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

                while (true)
                {
                    Console.WriteLine("tecla");
                    Console.ReadKey();
                    try
                    {
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var returnMessage = Encoding.UTF8.GetString(body);
                            var ticket = JsonConvert.DeserializeObject<Ticket>(returnMessage);
                            //Console.WriteLine(ticket.ToString());
                            var finish = false;
                            do
                            {
                                try
                                {
                                    var t = TicketService.PostTcket(ticket).Result;
                                    finish = false;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("falha ao persistir os dados");
                                    finish = true;
                                    Thread.Sleep(3000);
                                }
                              

                            } while (finish);


                            

                            //if(t == null)
                        };

                        channel.BasicConsume(queue: QUEUE_NAME,
                                             autoAck: true,
                                             consumer: consumer);

                        Thread.Sleep(2000);
                    }
                    catch (Exception ex) 
                    {
                        throw;
                    }
                }
            }
        }
    }
}
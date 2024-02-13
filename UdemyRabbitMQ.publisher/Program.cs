using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://umlnivor:onJcuoGCaUsIzS4T52YRgGBC1HfIoU3y@fish.rmq.cloudamqp.com/umlnivor");

using var connection = factory.CreateConnection();
var channel = connection.CreateModel();
channel.QueueDeclare("helloque", true, false, false);
Enumerable.Range(1, 50).ToList().ForEach(x =>
{
    string message = $"Message {x}";
    var messageBody = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(string.Empty, "helloque", null, messageBody);
    Console.WriteLine($"mesaj gonderildi: {message}");
});
Console.ReadLine();
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://umlnivor:onJcuoGCaUsIzS4T52YRgGBC1HfIoU3y@fish.rmq.cloudamqp.com/umlnivor");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare("header-exchange", durable: true, type: ExchangeType.Headers);


Dictionary<string, object> headers = new Dictionary<string, object>();

headers.Add("format", "pdf");
headers.Add("shape2", "a4");

var properties = channel.CreateBasicProperties();
properties.Headers = headers;


channel.BasicPublish("header-exchange", string.Empty, properties, Encoding.UTF8.GetBytes("header mesaji"));

Console.WriteLine("mesaj gonderildi");

Console.ReadLine();

public enum LogNames
{
    Critical = 1,
    Error = 2,
    Warning = 3,
    Info = 4
}
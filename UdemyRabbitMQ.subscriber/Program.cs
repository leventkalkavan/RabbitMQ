using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://umlnivor:onJcuoGCaUsIzS4T52YRgGBC1HfIoU3y@fish.rmq.cloudamqp.com/umlnivor");

using var connection = factory.CreateConnection();
var channel = connection.CreateModel();
//publisherda olustugundan eminsen channeli silebilirsin ama degilsen burda otomatik olusacaktir hata almazsin
//channel.QueueDeclare("helloque", true, false, false);

channel.BasicQos(0,1,true);
var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume("helloque", false, consumer);
consumer.Received += (object sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine("gelen mesaj: " + message);
    channel.BasicAck(e.DeliveryTag, false);
};

Console.ReadLine();
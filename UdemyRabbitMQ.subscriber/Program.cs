using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://umlnivor:onJcuoGCaUsIzS4T52YRgGBC1HfIoU3y@fish.rmq.cloudamqp.com/umlnivor");

using var connection = factory.CreateConnection();
var channel = connection.CreateModel();

//kalici kuyruk icin
var randemQue = "log-database-save-queque"; 
    //channel.QueueDeclare().QueueName; gecici kuyruk
channel.QueueDeclare(randemQue, true, false, false);
channel.QueueBind(randemQue, "logs-fanout", "", null);

channel.BasicQos(0, 1, true);
var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(randemQue, false, consumer);
Console.WriteLine("Loglar dinleniyor..");
consumer.Received += (object sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine("gelen mesaj: " + message);
    channel.BasicAck(e.DeliveryTag, false);
};

Console.ReadLine();
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace UdemyRabbitMQWeb.Watermark.Services;

public class RabbitMQPublisher
{
    private readonly RabbitMQClientService _client;

    public RabbitMQPublisher(RabbitMQClientService client)
    {
        _client = client;
    }

    public void Publish(ProductImageCreatedEvent productImageCreatedEvent)
    {
        var channel = _client.Connect();
        var bodyString = JsonSerializer.Serialize(productImageCreatedEvent);
        var bodyByte = Encoding.UTF8.GetBytes(bodyString);
        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName,
            routingKey: RabbitMQClientService.RoutingWatermark,
            basicProperties: properties, body: bodyByte);
    }
}
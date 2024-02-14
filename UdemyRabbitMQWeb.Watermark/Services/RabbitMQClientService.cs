using RabbitMQ.Client;

namespace UdemyRabbitMQWeb.Watermark.Services;

public class RabbitMQClientService : IDisposable
{
    private readonly ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _chanel;
    private readonly ILogger<RabbitMQClientService> _logger;
    public static string ExchangeName = "ImageDirectExchange";
    public static string RoutingWatermark = "watermark-route-image";
    public static string QueueName = "queue-watermark-image";

    public RabbitMQClientService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public IModel Connect()
    {
        _connection = _connectionFactory.CreateConnection();
        if (_chanel is {IsOpen:true})
        {
            return _chanel;
        }

        _chanel = _connection.CreateModel();
        _chanel.ExchangeDeclare(ExchangeName,type:"direct",true,false);
        _chanel.QueueDeclare(QueueName, true, false, false, null);
        _chanel.QueueBind(exchange:ExchangeName,queue: QueueName, routingKey: RoutingWatermark);
        _logger.LogInformation("rabbitmq ile baglanti kuruldu");
        return _chanel;
    }

    public void Dispose()
    {
        _connection?.Dispose();
        _chanel?.Dispose();
        
        _connection?.Close();
        _connection?.Dispose();
        
        _logger.LogInformation("rabbitmq ile baglanti koptu");
    }
}
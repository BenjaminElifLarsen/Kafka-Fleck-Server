using Confluent.Kafka;
using Fleck;

namespace Kafka_Fleck_Server;

internal class Consumer
{
    private readonly string bsServer = "172.16.250.13:9092";
    private const string _topic = "house";
    private readonly ConsumerConfig _config;
    private readonly CancellationTokenSource _cts = new();

    public Consumer()
    {
        Console.CancelKeyPress += (_, e) => {
            e.Cancel = true;
            _cts.Cancel();
        };
        _config = new()
        {
            GroupId = Guid.NewGuid().ToString(),
            BootstrapServers = bsServer,
            AutoOffsetReset = AutoOffsetReset.Latest
        };
    }

    public void Consume(IList<IWebSocketConnection> allSockets)
    {
        using (var consumer = new ConsumerBuilder<Null, string>(_config).Build())
        {
            consumer.Subscribe(_topic);
            try
            {
                while (true)
                {
                    var cr = consumer.Consume(_cts.Token);
                    var data = cr.Message.Value;
                    foreach(var socket in allSockets)
                    {
                        socket.Send(data);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Ctrl-C was pressed.
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}

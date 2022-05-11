using Confluent.Kafka;
using Fleck;

namespace Kafka_Fleck_Server;

internal class Consumer
{
    private readonly string _bServer = "localhost:9092"; //172.16.250.13
    private const string _topic = "data";
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
            BootstrapServers = _bServer,
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
                    foreach (var socket in allSockets)
                    {
                        socket.Send(data);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Ctrl-C was pressed.
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}

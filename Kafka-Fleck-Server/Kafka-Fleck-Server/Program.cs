using Fleck;
using Kafka_Fleck_Server;
//https://github.com/statianzo/Fleck/blob/master/src/Samples/ConsoleApp/Server.cs
FleckLog.Level = LogLevel.Debug;
var allSockets = new List<IWebSocketConnection>();
var server = new WebSocketServer("ws://0.0.0.0:8181");
server.Start(socket =>
{
    socket.OnOpen = () =>
    {
        Console.WriteLine("Open!");
        allSockets.Add(socket);
    };
    socket.OnClose = () =>
    {
        Console.WriteLine("Close!");
        allSockets.Remove(socket);
    };
    socket.OnMessage = message =>
    {
        Console.WriteLine(message);
        allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
    };
});

Task t = Task.Factory.StartNew(() =>
{
    new Consumer().Consume(allSockets);
});

var input = Console.ReadLine();
while (input != "exit")
{
    foreach (var socket in allSockets.ToList())
    {
        socket.Send(input);
    }
    input = Console.ReadLine();
}
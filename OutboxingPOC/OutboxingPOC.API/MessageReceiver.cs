using System.Reflection;
using DotNetCore.CAP;

namespace OutboxingPOC.API;

public class MessageReceiver : ICapSubscribe
{
    private readonly Assembly _assembly;
    public MessageReceiver(Assembly assembly)
    {
        _assembly = assembly;
    }
    [CapSubscribe(nameof(MessageReceiver))]
    public void ReceiveMessage(DateTime time, [FromCap]CapHeader header)
    {
        Console.WriteLine("message time is:" + time);
        Console.WriteLine("message first header :" + header["my.header.first"]);
        Console.WriteLine("message second header :" + header["my.header.second"]);
        // publish to domain sns topic
    }
}
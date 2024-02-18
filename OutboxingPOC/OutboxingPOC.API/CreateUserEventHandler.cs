using DotNetCore.CAP;
using MediatR;

namespace OutboxingPOC.API;

public class CreateUserEventHandler : INotificationHandler<UserCreated>
{
    public ICapPublisher _capBus;

    public CreateUserEventHandler(ICapPublisher capBus)
    {
        _capBus = capBus;
    }

    public async Task Handle(UserCreated createdUser, CancellationToken cancellationToken)
    {       var header = new Dictionary<string, string>()
        {
            ["my.header.first"] = createdUser.FirstName,
            ["my.header.second"] = createdUser.LastName
        };

        await _capBus.PublishAsync(nameof(MessageReceiver), DateTime.Now, header!, cancellationToken);
    }
}
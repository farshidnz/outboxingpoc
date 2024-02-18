using MediatR;

namespace OutboxingPOC.API;

public record UserCreated(string FirstName, string LastName) : INotification
{
    
}
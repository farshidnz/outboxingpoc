using MediatR;
using OutboxingPOC.API.Db;
using OutboxingPOC.API.Db.Models;

namespace OutboxingPOC.API;

public record CreateUserCommand(string FirstName, string LastName) : IRequest
{
    public class CreateUserHandler : AsyncRequestHandler<CreateUserCommand>
        {
            private readonly IPublisher _publisher;
        private readonly UsersDatabaseContext _db;

        public CreateUserHandler(IPublisher publisher, UsersDatabaseContext db)
        {
            _publisher = publisher;
            _db = db;
        }

        protected override async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //transaction {
            await _db.Users.AddAsync(new Users() {FirstName = request.FirstName, LastName = request.LastName}, cancellationToken);
            _db.SaveChanges();
            await _publisher.Publish(new UserCreated(request.FirstName, request.LastName), cancellationToken);
            //}
        }
    }
}
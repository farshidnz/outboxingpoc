using System.Reflection;
using DotNetCore.CAP;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutboxingPOC.API;
using OutboxingPOC.API.Db;
using Savorboard.CAP.InMemoryMessageQueue;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services
    .AddDbContext<UsersDatabaseContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
builder.Services.AddScoped<UsersDatabaseContext>();

builder.Services.AddCap(cap =>
{
    cap.UseEntityFramework<UsersDatabaseContext>();
    cap.UseSqlServer(opt =>
    {
        opt.Schema = "cap_api";
    });
    cap.UseInMemoryMessageQueue();
    cap.UseDashboard();
});

builder.Services.AddTransient<ICapSubscribe>(provider => ActivatorUtilities.CreateInstance<MessageReceiver>(provider, typeof(UserCreated).Assembly));

var app = builder.Build();

app.MapPost("/", ([FromServices] ISender sender, User request) =>
{
    sender.Send(new CreateUserCommand(request.FirstName, request.LastName));
});

app.Run();

public record User(string FirstName, string LastName); 
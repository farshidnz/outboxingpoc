var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<Service>();
builder.Services.AddScoped<ITransaction, SqlServerCapTransaction>();

var app = builder.Build();

app.MapGet("/", async (Service service) =>
{
    service.BeginTransaction();
    return await Task.FromResult("Hello World!");
});

app.Run();


public class Service :IService
{
    private readonly IServiceProvider _service;
    private readonly ITransaction _transaction;

    public Service(IServiceProvider service, ITransaction transaction)
    {
        _service = service;
        _transaction = transaction;
        Transaction = new AsyncLocal<ITransaction>();
    }

    public void BeginTransaction()
    {
        var t =  ActivatorUtilities.CreateInstance<SqlServerCapTransaction>(_service);
        Transaction.Value = _transaction;

    }

    public AsyncLocal<ITransaction> Transaction { get; set; }
}
public interface IService
{
    AsyncLocal<ITransaction> Transaction { get; }
}

public interface ITransaction : IDisposable
{
}


public class SqlServerCapTransaction : ITransaction
{
    //public virtual object? DbTransaction { get; set; }
    public void Dispose()
    {
        Console.WriteLine("Disposing stuff");
    }
}
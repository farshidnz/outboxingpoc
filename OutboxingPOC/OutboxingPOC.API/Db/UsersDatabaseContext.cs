using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OutboxingPOC.API.Db.Models;

namespace OutboxingPOC.API.Db;

public class UsersDatabaseContext : DbContext
{
    public DbSet<Users> Users { get; set; }
    
    public UsersDatabaseContext() { }
    public UsersDatabaseContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}


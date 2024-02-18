using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutboxingPOC.API.Db.Models;

namespace OutboxingPOC.API.Db.Configurations;

public class UsersConfiguration: IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.ToTable("Users")
            .HasIndex(o => o.Id)
            .IsUnique();

        builder
        .Property(b => b.Id)
        .IsRequired();
    }
}
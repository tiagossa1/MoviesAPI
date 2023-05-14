using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class GenderEntityBuilderConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder.HasKey(g => g.Id);
        builder
            .Property(g => g.Id)
            .ValueGeneratedOnAdd();
        builder
            .HasData(new List<Gender>
            {
                new Gender { Id = 1, Name = "Male", CreatedAtUtc = DateTime.UtcNow, UpdatedAtUtc = DateTime.UtcNow },
                new Gender { Id = 2, Name = "Female", CreatedAtUtc = DateTime.UtcNow, UpdatedAtUtc = DateTime.UtcNow }
            });
    }
}
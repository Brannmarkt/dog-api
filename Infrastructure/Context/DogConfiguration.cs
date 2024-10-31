using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context;

public class DogConfiguration : IEntityTypeConfiguration<DogEntity>
{
    public void Configure(EntityTypeBuilder<DogEntity> builder)
    {
        builder.ToTable("Dogs");
        builder.HasKey(d => d.Name);

        builder.Property(d => d.Name)
            .HasColumnName("name").IsRequired();

        builder.Property(d => d.Color)
            .HasColumnName("color").IsRequired();

        builder.Property(d => d.TailLength)
            .HasColumnName("tail_length").IsRequired();

        builder.Property(d => d.Weight)
            .HasColumnName("weight").IsRequired();
    }
}

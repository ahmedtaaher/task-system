using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
{
  public void Configure(EntityTypeBuilder<TaskItem> builder)
  {
    builder.ToTable("tasks");

    builder.HasKey(t => t.Id);

    builder.Property(t => t.Title).IsRequired().HasMaxLength(200);

    builder.Property(t => t.Description);

    builder.Property(t => t.Status).IsRequired();

    builder.Property(t => t.Priority).IsRequired();

    builder.Property(t => t.CreatedAt).IsRequired();

    builder.HasIndex(t => new { t.UserId, t.Title, t.CreatedAt }).IsUnique();
    }
}
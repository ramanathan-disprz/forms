using forms.Model;
using Microsoft.EntityFrameworkCore;

namespace forms.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<FormResponse> FormResponses => Set<FormResponse>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // user mapping
        modelBuilder.Entity<User>()
            .Property(u => u.UserRole)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasColumnName("user_role")
            .IsRequired();

        // form response mapping

        modelBuilder.Entity<FormResponse>(e =>
        {
            e.Property(x => x.Answers)
                .HasColumnName("answers")
                .HasColumnType("json")
                .IsRequired();

            e.HasIndex(x => new { x.FormId, x.UserId })
                .HasDatabaseName("IX_FormResponses_Form_User");
        });
    }
}
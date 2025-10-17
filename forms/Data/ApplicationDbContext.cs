using forms.Model;
using forms.Model.FormSubmission;
using Microsoft.EntityFrameworkCore;

namespace forms.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Admin> Admins => Set<Admin>();

    public DbSet<FormSubmission> FormSubmissions => Set<FormSubmission>();

    public DbSet<FormAnswer> FormAnswers => Set<FormAnswer>();

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

        //admin mapping
        modelBuilder.Entity<Admin>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Admin>(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // form submission mapping
        modelBuilder.Entity<FormSubmission>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(fs => fs.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        //form answers mapping
        modelBuilder.Entity<FormAnswer>()
            .HasOne<FormSubmission>()
            .WithMany()
            .HasForeignKey(fa => fa.SubmissionId)
            .OnDelete(DeleteBehavior.Cascade);

        // form response mapping
        modelBuilder.Entity<FormAnswer>(e =>
        {
            e.Property(x => x.ValueJson)
                .HasColumnName("value_json")
                .HasColumnType("json")
                .IsRequired(false);
        });
    }
}
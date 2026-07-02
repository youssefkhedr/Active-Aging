using ElderCare.Core.Entities;
using ElderCare.Core.Constants;
using Microsoft.EntityFrameworkCore;

namespace ElderCare.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<TreatmentPlan> TreatmentPlans => Set<TreatmentPlan>();
    public DbSet<TrainingPlan> TrainingPlans => Set<TrainingPlan>();
    public DbSet<RomAssessment> RomAssessments => Set<RomAssessment>();
    public DbSet<BalanceAssessment> BalanceAssessments => Set<BalanceAssessment>();
    public DbSet<SarcFResult> SarcFResults => Set<SarcFResult>();
    public DbSet<FiveTstsResult> FiveTstsResults => Set<FiveTstsResult>();
    public DbSet<CognitiveMmseResult> CognitiveMmseResults => Set<CognitiveMmseResult>();
    public DbSet<CognitiveMiniCogResult> CognitiveMiniCogResults => Set<CognitiveMiniCogResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();

            entity.Property(u => u.FullName).IsRequired();
            entity.Property(u => u.Email).IsRequired();
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.Role).IsRequired();
        });

        modelBuilder.Entity<TrainingPlan>(entity =>
        {
            entity.HasKey(tp => tp.Id);

            entity.Property(tp => tp.Exercises).IsRequired();
            entity.Property(tp => tp.Schedule).IsRequired();
        });

        // Seed Admin Account
        var adminUser = new User
        {
            Id = Guid.Parse("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"),
            FullName = "Admin User",
            Email = "yousefahmed012732@gmail.com",
            PasswordHash = "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgNoTfG6YmD8oJ8e.973jR.2W6U.", // Hash for Yy@Aa2004
            Role = Roles.Admin,
            Age = 30,
            Gender = "Male"
        };
        modelBuilder.Entity<User>().HasData(adminUser);
    }
}

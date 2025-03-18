using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolAdministrationSystem.Data.Seeders;

namespace SchoolAdministrationSystem.Data.Entities
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolAdministrationDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Teacher)
                .WithOne(t => t.Class)
                .HasForeignKey<Class>(c => c.TeacherId);

            modelBuilder.Entity<Class>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Class)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Absence>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Absences)
                .HasForeignKey(a => a.StudentId);

            modelBuilder.Entity<Absence>()
                .HasOne(a => a.Class)
                .WithMany(c => c.Absences)
                .HasForeignKey(a => a.ClassId);

            modelBuilder.Entity<Absence>().HasKey(a => a.Id);

            modelBuilder.Entity<Student>()
            .Property(s => s.Gender)
            .HasConversion(new EnumToStringConverter<Gender>());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Holiday> Holidays { get; set; } // idk
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;

namespace SchoolAdministrationSystem.Data.Seeders
{
    public static class DataSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder);
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Teacher", NormalizedName = "TEACHER" },
                new IdentityRole { Id = "3", Name = "Student", NormalizedName = "STUDENT" },
                new IdentityRole { Id = "4", Name = "Parent", NormalizedName = "PARENT" }

            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

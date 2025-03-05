using Microsoft.AspNetCore.Identity;

namespace SchoolAdministrationSystem.Data.Seeders
{
    public static class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin", "Teacher", "Student", "Parent" };
            foreach (var role in roleNames)
            {
                bool roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminuser = new IdentityUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true
            };

            string adminPassword = "Admin#123";

            var teacheruser = new IdentityUser
            {
                UserName = "aneliyaruseva@gmail.com",
                Email = "aneliyaruseva@gmail.com",
                EmailConfirmed = true
            };

            var teacherPassword = "Anelia@123";

            var adminUser = await userManager.FindByEmailAsync(adminuser.Email);
            var teacherUser = await userManager.FindByEmailAsync(teacheruser.Email);

            if (adminUser == null && teacherUser == null)
            {
                var createdAdmin = await userManager
                    .CreateAsync(adminuser, adminPassword);
                var createdTeacher = await userManager
                    .CreateAsync(teacheruser, teacherPassword);
                if (createdAdmin.Succeeded && createdTeacher.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminuser, "Admin");
                    await userManager.AddToRoleAsync(teacheruser, "Teacher");
                }
            }
        }
    }
}

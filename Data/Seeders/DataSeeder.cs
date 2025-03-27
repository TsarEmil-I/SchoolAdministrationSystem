using Microsoft.AspNetCore.Identity;
using System.Drawing.Text;

namespace SchoolAdministrationSystem.Data.Seeders
{
    public static class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            var adminUser = new IdentityUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true
            };

            string adminPassword = "Admin#123";
            await SeedUser(adminUser, adminPassword, "Admin", userManager);

            var teacherUser = new IdentityUser
            {
                UserName = "aneliyaruseva@gmail.com",
                Email = "aneliyaruseva@gmail.com",
                EmailConfirmed = true
            };

            var teacherPassword = "Anelia@123";
            await SeedUser(teacherUser, teacherPassword, "Teacher", userManager);

            var studentUser = new IdentityUser
            {
                UserName = "student@gmail.com",
                Email = "student@gmail.com",
                EmailConfirmed = true
            };

            var studentPassword = "Student@123";
            await SeedUser(studentUser, studentPassword, "Student", userManager);

            var parentUser = new IdentityUser
            {
                UserName = "parent@gmail.com",
                Email = "parent@gmail.com",
                EmailConfirmed = true
            };

            var parentPassword = "Parent@123";
            await SeedUser(parentUser, parentPassword, "Parent", userManager);
        }

        private static async Task SeedUser(IdentityUser user, string password, string roleName, UserManager<IdentityUser> userManager)
        {
            var userInfo = await userManager.FindByEmailAsync(user.Email);
            if (userInfo == null)
            {
                var created = await userManager
                    .CreateAsync(user, password);
                if (created.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

        static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Teacher", "Student", "Parent" };
            foreach (var role in roleNames)
            {
                bool roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

    }

}


using Authentication2F.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication2F.Data
{
    public class SeedData
    {

        public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleName  = { "Admin", "User" };
            foreach (var role in roleName)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // CREATE A DEFAULT ADMIN USER IF NOT EXISTS
            var adminEmail = "akhileshshahu200@gmail.com";
            var adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null) { 
            
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Akhilesh Shahu",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }

            }




            //string adminEmail = configuration["AdminUser:Email"];
            //string adminPassword = configuration["AdminUser:Password"];
            //string adminFullName = configuration["AdminUser:FullName"];

            //string adminRole = "Admin";

            // Ensure Admin Role exists
            //if (!await roleManager.RoleExistsAsync(adminRole))
            //{
            //    await roleManager.CreateAsync(new IdentityRole(adminRole));
            //}
            //// Ensure Admin User exists
            //var adminUser = await userManager.FindByEmailAsync(adminEmail);
            //if (adminUser == null)
            //{
            //    adminUser = new Models.ApplicationUser
            //    {
            //        UserName = adminEmail,
            //        Email = adminEmail,
            //        FullName = adminFullName,
            //        EmailConfirmed = true // Assuming admin email is confirmed by default
            //    };
            //    var result = await userManager.CreateAsync(adminUser, adminPassword);
            //    if (result.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(adminUser, adminRole);
            //    }
            //    else
            //    {
            //        throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            //    }
            //}
            //else
            //{
            //    // Ensure the user has the Admin role
            //    if (!await userManager.IsInRoleAsync(adminUser, adminRole))
            //    {
            //        await userManager.AddToRoleAsync(adminUser, adminRole);
            //    }
            //}
        }
    }
}

using AuthServer.Api.Contextes;
using AuthServer.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Api.SeedData
{
    public class SeedDbContext
    {
        public async static void Seed(IApplicationBuilder applicationBuilder)
        {
            var scope = applicationBuilder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<AuthDemoDbContext>();

            var roles = new List<IdentityRole>
                {
                  new IdentityRole("EXAMPLE1URLGET"),
                  new IdentityRole("EXAMPLE1URLPOST"),
                  new IdentityRole("PRODUCTADD"),
                  new IdentityRole("PRODUCTUPDATE"),
                  new IdentityRole("PRODUCTDELETE"),
                  new IdentityRole("PRODUCTREAD"),
                  new IdentityRole("ADMIN"),
                  new IdentityRole("SUPERVISOR"),
                  new IdentityRole("USER")
                }; // Defaultta sadece gerekli roller eklenmeli...

            // Eğer roller tablosunda veri yoksa seed verilerini ekle
            if (!await context.Roles.AnyAsync())
            {
                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }

            // Eğer kullanıcılar tablosunda veri yoksa seed verilerini ekle
            if (!await context.Users.AnyAsync())
            {
                var adminUser = new ExtendedIdentityUser
                {
                    UserName = "sysadmin",
                    NormalizedUserName ="SYSADMIN",
                    Email = "sysadmin",
                    NormalizedEmail = "SYSADMIN",
                    PasswordHash = "AQAAAAIAAYagAAAAEH7B5APYWjaviXvpuVKhUZ7v7BifG4Ux78i75m8iWMqY+9CPQwMzJGB+YrnqoXwsjw==" // pwd : 123
                };
                var result = await context.Users.AddAsync(adminUser);

                if (result.State == EntityState.Added)
                {
                    var userRole = new List<IdentityUserRole<string>>();

                    foreach (var roleId in roles)
                    {
                        userRole.Add(new IdentityUserRole<string>() { UserId = adminUser.Id, RoleId = roleId.Id });
                    }

                    await context.UserRoles.AddRangeAsync(userRole);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}

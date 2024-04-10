using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebTechnologies.Services
{
	public class DbInitializer
	{
		public static async Task Seed(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			// создать БД, если она еще не создана
			context.Database.EnsureCreated();
			// проверка наличия ролей
			if (!context.Roles.Any())
			{
				var roleAdmin = new IdentityRole
				{
					Name = "admin",
					NormalizedName = "admin"
				};
				// создать роль admin
				await roleManager.CreateAsync(roleAdmin);
			}
			// проверка наличия пользователей
			if (!context.Users.Any())
			{
				// создать пользователя user@mail.ru
				var user = new ApplicationUser
				{
					Email = "user@mail.ru",
					UserName = "user@mail.ru"
				};
				var result = await userManager.CreateAsync(user, "%12Aaa3456");
				// создать пользователя admin@mail.ru
				var admin = new IdentityUser
				{
					Email = "admin@mail.ru",
					UserName = "admin@mail.ru"
				};
				await userManager.CreateAsync(admin, "%12Aaa3456");
				// назначить роль admin
				var tempadmin = await userManager.Users.FirstOrDefaultAsync(u => u.Email=="admin@mail.ru");
				await userManager.AddToRoleAsync(admin, "admin");
			}
		}

	}
}

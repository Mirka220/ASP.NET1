using Microsoft.EntityFrameworkCore;

namespace Practice1.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDBContext(serviceProvider.GetRequiredService<DbContextOptions<AppDBContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }


                context.Users.AddRange(
                    new User
                    {
                        Login = "user1@user.com",
                        Password = "qwerty123"
                    }
                );

                context.SaveChanges();
            }

        }
    }
}

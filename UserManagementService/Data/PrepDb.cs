using UserService.Models;
using BCrypt.Net;
namespace UserService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<UserDbContext>());
            }
        }

        private static void SeedData(UserDbContext context)
        {
            if(!context.Users.Any())
            {
                Console.WriteLine("Seeding data");

                context.Users.AddRange(
                    new User() { Email = "Jozo.raz@email.com", Name = "Jozo Raz", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Jozo123!") },
                    new User() { Email = "Peto.Vrba@email.com", Name = "Peter Vrba", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Petkovic2000") }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Already have data");
            }
        }
    }
}
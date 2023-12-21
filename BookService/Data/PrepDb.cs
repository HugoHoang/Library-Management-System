using BookService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<BookDbContext>());
            }
        }

        private static void SeedData(BookDbContext context)
        {
            Console.WriteLine("--- Attempting migrations");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            if(!context.Books.Any())
            {
                Console.WriteLine("Seeding data");

                context.Books.AddRange(
                    new Book() {Title = "Jozo", Author = "Raz", Publisher = "peter", PublicationYear = 1990, AvailableCount = 2},
                    new Book() {Title = "Juro", Author = "Nas", Publisher = "picus", PublicationYear = 1991, AvailableCount = 1 },
                    new Book() {Title = "Peto", Author = "Vas", Publisher = "kokot", PublicationYear = 1992, AvailableCount = 3}
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
using LoanService.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<LoanDbContext>());
            }
        }

        private static void SeedData(LoanDbContext context)
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

            if (!context.Loans.Any())
            {
                Console.WriteLine("Seeding data");

                DateTime dateTime = DateTime.Now.ToUniversalTime();
                context.Loans.AddRange(
                    new Loan() { BookId = 1, UserId = 2, LoanDate = dateTime, DueDate = dateTime.AddDays(30)} ,
                    new Loan() { BookId = 1, UserId = 2, LoanDate = dateTime.AddDays(-2), DueDate = dateTime.AddDays(28) }
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
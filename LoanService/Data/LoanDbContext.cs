using LoanService.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanService.Data
{
    public class LoanDbContext : DbContext
    {
        public LoanDbContext(DbContextOptions<LoanDbContext> opt) : base(opt)
        {
            
        }
        public DbSet<Loan> Loans { get; set; }
        
    }
}

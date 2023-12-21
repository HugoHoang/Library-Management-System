using LoanService.Dtos;
using LoanService.Models;

namespace LoanService.Data
{
    public class LoanRepo : ILoanRepo
    {
        private readonly LoanDbContext _context;

        public LoanRepo(LoanDbContext context)
        {
            _context = context;
        }

        public void AddLoan(Loan loan)
        {
            if (loan == null)
            {
                throw new ArgumentNullException(nameof(loan));
            }
            DateTime loanDate = DateTime.Now;
            loan.LoanDate = loanDate.ToUniversalTime();
            loan.DueDate = loanDate.AddDays(30).ToUniversalTime();
             _context.Loans.Add(loan);
        }

        public IEnumerable<Loan> GetAllLoans()
        {
            return _context.Loans.ToList();
        }

        public Loan GetLoanByID(int id)
        {
            var loan = _context.Loans.Find(id);
            if (loan == null)
            {
                throw new KeyNotFoundException($"The loan with ID {id} does not exist.");
            }

            return loan; 
        }

        public IEnumerable<Loan> GetLoansByUser(int userId)
        {
            return _context.Loans.Where(l => l.UserId == userId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateLoan(LoanUpdateDto loanUpdateDto)
        {
            var loan = _context.Loans.FirstOrDefault(l => l.LoanId == loanUpdateDto.LoanId);

            if (loan == null)
            {
                throw new KeyNotFoundException("Loan was not found.");
            }

            loan.ReturnDate = loanUpdateDto.ReturnDate.ToUniversalTime();
            _context.SaveChanges();
        }
    }
}

using LoanService.Dtos;
using LoanService.Models;

namespace LoanService.Data
{
    public interface ILoanRepo
    {
        bool SaveChanges();
        IEnumerable<Loan> GetAllLoans();
        Loan GetLoanByID(int id);
        void AddLoan(Loan loan);
        IEnumerable<Loan> GetLoansByUser(int userId);
        public void UpdateLoan(LoanUpdateDto loanUpdateDto);
    }
}

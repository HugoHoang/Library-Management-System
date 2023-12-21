using System.ComponentModel.DataAnnotations;

namespace LoanService.Models
{
    public class Loan
    {
        [Key]
        [Required]
        public int LoanId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime LoanDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }
        public bool IsReturned => ReturnDate.HasValue;
    }
}

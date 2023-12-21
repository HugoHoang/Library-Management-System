using System.ComponentModel.DataAnnotations;

namespace LoanService.Dtos
{
    public class LoanCreateDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BookService.Dtos
{
    public class BookUpdateDto
    {
        [Required]
        public int bookId { get; set; }
        [Required]
        public int AvailableCount { get; set; }
    }
}

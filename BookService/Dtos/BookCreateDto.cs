using System.ComponentModel.DataAnnotations;

namespace BookService.Dtos
{
    public class BookCreateDto
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Author { get; set; }

        public string? ISBN { get; set; }

        public string? Category { get; set; }

        [Required]
        public string? Publisher { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public int AvailableCount { get; set; }
    }
}

namespace BookService.Dtos
{
    public class BookReadDto
    {
        public int bookId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public string? Category { get; set; }
        public string? Publisher { get; set; }
        public int PublicationYear { get; set; }
        public int AvailableCount { get; set; }
    }
}
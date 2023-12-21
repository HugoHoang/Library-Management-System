namespace LoanService.Dtos
{
    public class LoanReadDto
    {
        public int LoanId { get; set; }
        public int BookId { get; set; }
        public string? BookName { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}

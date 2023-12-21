using BookService.Models;


namespace BookService.Data
{
    public interface IBookRepo
    {
        bool SaveChanges();
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        Task AddBook(Book book);
        IEnumerable<Book> GetBooksByCategory(string category);
        int GetAvailableCount(int id);
        void UpdateBook(int id, int newCount);
    }
}

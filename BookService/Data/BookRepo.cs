using BookService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookService.Data
{
    public class BookRepo : IBookRepo
    {
        private readonly BookDbContext _context;

        public BookRepo(BookDbContext context)
        {
            _context = context;
        }

        public async Task AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            
            var existingBook = await _context.Books
                                             .Where(b => b.Title == book.Title)
                                             .Where(b => b.Author == book.Author)
                                             .Where(b => b.Publisher == book.Publisher)
                                             .FirstOrDefaultAsync();
            if (existingBook == null) 
            {
                _context.Books.Add(book);
            }
            else
            {
                throw new ArgumentException($"The book {existingBook.Title} (id: {existingBook.bookId}) already exists.");
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public int GetAvailableCount(int id)
        {
            var count = _context.Books.Where(b => b.bookId == id).Select(b => b.AvailableCount).FirstOrDefault();
            if(count == default)
            {
                throw new KeyNotFoundException($"A book with ID {id} was not found.");
            }

            return count;
        }

        public Book GetBookById(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
            {
                throw new KeyNotFoundException($"A book with ID {id} was not found.");
            }

            return book;  
        }


        public IEnumerable<Book> GetBooksByCategory (string category)
        {
            return _context.Books.Where(b => b.Category == category).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateBook(int id, int newCount)
        {
            if (newCount < 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(newCount));
            }

            var book = GetBookById(id);
            book.AvailableCount = newCount;
        }
    }
}
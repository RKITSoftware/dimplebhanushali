using Filters_Web_API.MAL;
using Microsoft.Extensions.Caching.Memory;

namespace Filters_Web_API.BAL
{
    public class BooksService
    {
        public static IMemoryCache _cache;
        private const string CacheKey = "BooksCacheKey";

        public BooksService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            // Try to get books from cache
            if (_cache.TryGetValue(CacheKey, out IEnumerable<Book> cachedBooks))
            {
                return cachedBooks;
            }

            // If not found in cache, retrieve from source and cache it
            var books = GetBooksFromSource();
            _cache.Set(CacheKey, books, TimeSpan.FromMinutes(5)); // Cache for 5 minutes
            return books;
        }

        public Book GetBookById(int id)
        {
            var books = GetAllBooks();
            return books.FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            var books = GetAllBooks().ToList();
            book.Id = books.Count + 1;
            books.Add(book);
            _cache.Set(CacheKey, books, TimeSpan.FromMinutes(5)); // Update cache
        }

        public bool UpdateBook(int id, Book book)
        {
            var books = GetAllBooks().ToList();
            var existingBook = books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
                return false;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;

            _cache.Set(CacheKey, books, TimeSpan.FromMinutes(5)); // Update cache
            return true;
        }

        public bool DeleteBook(int id)
        {
            var books = GetAllBooks().ToList();
            var existingBook = books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
                return false;

            books.Remove(existingBook);
            _cache.Set(CacheKey, books, TimeSpan.FromMinutes(5)); // Update cache
            return true;
        }

        // Simulated data source
        private List<Book> GetBooksFromSource()
        {
            return new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", Year = 2000 },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", Year = 2005 },
                new Book { Id = 3, Title = "Book 3", Author = "Author 3", Year = 2010 }
            };
        }
    }
}

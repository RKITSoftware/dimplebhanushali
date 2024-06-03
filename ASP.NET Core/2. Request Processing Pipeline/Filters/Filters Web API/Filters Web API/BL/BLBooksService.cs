using Filters_Web_API.MAL;
using Microsoft.Extensions.Caching.Memory;

namespace Filters_Web_API.BAL
{
    /// <summary>
    /// Service class to manage book-related operations including caching.
    /// </summary>
    public class BLBooksService
    {
        #region Private Member
        private static IMemoryCache _cache;
        private const string CacheKey = "BooksCacheKey";
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BLBooksService"/> class with the specified cache.
        /// </summary>
        /// <param name="cache">The memory cache to be used for caching books data.</param>
        public BLBooksService(IMemoryCache cache)
        {
            _cache = cache;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves all books, either from the cache or from the data source if not cached.
        /// </summary>
        /// <returns>An enumerable collection of books.</returns>
        public List<Book> GetAllBooks()
        {
            // Try to get books from cache
            if (_cache.TryGetValue(CacheKey, out IEnumerable<Book> cachedBooks))
            {
                return cachedBooks.ToList();
            }

            // If not found in cache, retrieve from source and cache it
            List<Book> books = GetBooksFromSource();
            //// POST
            _cache.Set(CacheKey, books, TimeSpan.FromMinutes(5)); // Cache for 5 minutes
            return books;
        }

        /// <summary>
        /// Retrieves a book by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <returns>The book with the specified identifier, or null if not found.</returns>
        public Book GetBookById(int id)
        {
            List<Book> books = GetAllBooks();
            return books.FirstOrDefault(b => b.Id == id);
        }

        /// <summary>
        /// Adds a new book to the collection and updates the cache.
        /// </summary>
        /// <param name="book">The book to add.</param>
        public void AddBook(Book book)
        {
            List<Book> books = GetAllBooks().ToList();
            book.Id = books.Count + 1;
            books.Add(book);
            _cache.Set(CacheKey, books, TimeSpan.FromMinutes(5)); // Update cache
        }

        /// <summary>
        /// Updates an existing book's information and updates the cache.
        /// </summary>
        /// <param name="id">The unique identifier of the book to update.</param>
        /// <param name="book">The updated book information.</param>
        /// <returns>True if the book was updated successfully, otherwise false.</returns>
        public bool UpdateBook(int id, Book book)
        {
            List<Book> books = GetAllBooks().ToList();
            Book existingBook = books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
                return false;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;

            _cache.Set(CacheKey, books, TimeSpan.FromMinutes(5)); // Update cache
            return true;
        }

        /// <summary>
        /// Deletes a book by its unique identifier and updates the cache.
        /// </summary>
        /// <param name="id">The unique identifier of the book to delete.</param>
        /// <returns>True if the book was deleted successfully, otherwise false.</returns>
        public bool DeleteBook(int id)
        {
            List<Book> books = GetAllBooks().ToList();
            Book existingBook = books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
                return false;

            books.Remove(existingBook);
            _cache.Set(CacheKey, books, TimeSpan.FromMinutes(5)); // Update cache
            return true;
        }

        /// <summary>
        /// Simulated data source method to retrieve a list of books.
        /// </summary>
        /// <returns>A list of books from the simulated data source.</returns>
        private List<Book> GetBooksFromSource()
        {
            return new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", Year = 2000 },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", Year = 2005 },
                new Book { Id = 3, Title = "Book 3", Author = "Author 3", Year = 2010 }
            };
        }

        #endregion
    }
}

using Filters_Web_API.BAL;
using Filters_Web_API.Filters;
using Filters_Web_API.MAL;
using Microsoft.AspNetCore.Mvc;

namespace Filters_Web_API.Controllers
{
    /// <summary>
    /// API controller for managing books.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    [ServiceFilter(typeof(CustomResourceFilter))]
    [ServiceFilter(typeof(CustomActionFilter))]
    [ServiceFilter(typeof(CustomResultFilter))]
    public class BooksController : ControllerBase
    {
        #region Private Members
        private readonly BooksService _booksService;
        private readonly ILogger<BooksController> _logger;
        #endregion

        #region Construcotr
        /// <summary>
        /// Initializes a new instance of the <see cref="BooksController"/> class.
        /// </summary>
        /// <param name="booksService">The books service to manage book data.</param>
        /// <param name="logger">The logger for logging information.</param>
        public BooksController(BooksService booksService, ILogger<BooksController> logger)
        {
            _booksService = booksService;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>An action result containing a list of all books.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            List<Book> books = _booksService.GetAllBooks();
            if (books == null || !books.Any()) // Short-circuit if no books found
                return NoContent();

            return Ok(books);
        }

        /// <summary>
        /// Gets a book by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <returns>An action result containing the book with the specified identifier.</returns>        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            Book book = _booksService.GetBookById(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="book">The book to create.</param>
        /// <returns>An action result containing the created book.</returns>
        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _booksService.AddBook(book);
            //return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            return RedirectToAction("GetAll");
        }

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        /// <param name="id">The unique identifier of the book to update.</param>
        /// <param name="book">The updated book information.</param>
        /// <returns>An action result indicating the result of the update operation.</returns>
        [HttpPut("{id}")]
        public ActionResult Update(int id, Book book)
        {
            bool isUpdated = _booksService.UpdateBook(id, book);
            if (!isUpdated)
                return NotFound();

            return Ok($"Book With {id} Updated Successfully");

        }

        /// <summary>
        /// Deletes a book by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book to delete.</param>
        /// <returns>An action result indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool isDeleted = _booksService.DeleteBook(id);
            if (!isDeleted)
                return NotFound();

            return Ok($"Book With {id} Deleted Successfully");
        }
    }
}

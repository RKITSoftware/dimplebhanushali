using Filters_Web_API.BAL;
using Filters_Web_API.Filters;
using Filters_Web_API.MAL;
using Microsoft.AspNetCore.Mvc;

namespace Filters_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    [ServiceFilter(typeof(CustomResourceFilter))]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BooksService booksService, ILogger<BooksController> logger)
        {
            _booksService = booksService;
            _logger = logger;
        }

        // GET: api/books
        [HttpGet]
        [ServiceFilter(typeof(CustomActionFilter))]
        [ServiceFilter(typeof(CustomResultFilter))]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            var books = _booksService.GetAllBooks();
            if (books == null || !books.Any()) // Short-circuit if no books found
                return NoContent();

            return Ok(books);
        }

        // GET: api/books/1
        [HttpGet("{id}")]
        [ServiceFilter(typeof(CustomActionFilter))]
        [ServiceFilter(typeof(CustomResultFilter))]
        public ActionResult<Book> GetById(int id)
        {
            var book = _booksService.GetBookById(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        [ServiceFilter(typeof(CustomActionFilter))]
        [ServiceFilter(typeof(CustomResultFilter))]
        public ActionResult<Book> Create(Book book)
        {
            _booksService.AddBook(book);
            //return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            return RedirectToAction("GetAll");
        }

        // PUT: api/books/1
        [HttpPut("{id}")]
        [ServiceFilter(typeof(CustomActionFilter))]
        [ServiceFilter(typeof(CustomResultFilter))]
        public ActionResult Update(int id, Book book)
        {
            var isUpdated = _booksService.UpdateBook(id, book);
            if (!isUpdated)
                return NotFound();

            return Ok($"Book With {id} Updated Successfully");

        }

        // DELETE: api/books/1
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(CustomActionFilter))]
        [ServiceFilter(typeof(CustomResultFilter))]
        public ActionResult Delete(int id)
        {
            var isDeleted = _booksService.DeleteBook(id);
            if (!isDeleted)
                return NotFound();

            return Ok($"Book With {id} Deleted Successfully");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBookstoreAPI;
using SimpleBookstoreAPI.Models;
using System.Drawing;


[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly WorldDbContext _context;

    public BooksController(WorldDbContext context)
    {
        _context = context;
    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        if (_context.Books == null)
        {
            return NotFound();
        }

        return await _context.Books.Include(p => p.AuthorName).ToListAsync();
    }

    // GET: api/Books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.Books.Include(p => p.AuthorName).FirstOrDefaultAsync(p => p.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    // PUT: api/Books/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (!this.ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != book.Id)
        {
            return BadRequest();
        }

        _context.Update(book);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Books
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        if (!this.ModelState.IsValid)
            return BadRequest(ModelState);

        if (_context.Books == null)
        {
            return Problem("Book is null.");
        }

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    // DELETE: api/Books/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        if (_context.Books == null)
        {
            return NotFound();
        }
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookExists(int id)
    {
        return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}

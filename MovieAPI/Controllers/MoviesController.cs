using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;
using MovieAPI.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace MovieAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MoviesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET con paginación y búsqueda
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movies>>> GetMovies(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null)
    {
        var query = _context.Movies.Include(m => m.Director).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(m => m.Name.Contains(search));
        }

        var totalItems = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new {
            totalItems,
            page,
            pageSize,
            items
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movies>> GetMovie(int id)
    {
        var movie = await _context.Movies
            .Include(m => m.Director)
            .FirstOrDefaultAsync(m => m.MoviesId == id);

        if (movie == null)
            return NotFound();

        return movie;
    }

    [HttpPost]
    public async Task<ActionResult<Movies>> CreateMovie(MovieCreateDto dto)
    {
        var movie = new Movies
        {
            Name = dto.Name,
            ReleaseYear = dto.ReleaseYear,
            Gender = dto.Gender,
            Duration = dto.Duration,
            DirectorId = dto.DirectorId
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMovie), new { id = movie.MoviesId }, movie);
    }


    [HttpPut("{id}")]
    [SwaggerRequestExample(typeof(MovieUpdateDto), typeof(MovieUpdateExample))]
    public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDto dto)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
            return NotFound();

        movie.Name = dto.Name;
        movie.ReleaseYear = dto.ReleaseYear;
        movie.Gender = dto.Gender;
        movie.Duration = dto.Duration;
        movie.DirectorId = dto.DirectorId;

        await _context.SaveChangesAsync();

        return Ok(movie);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
            return NotFound();

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Película eliminada exitosamente",
            deletedMovie = movie
        });
    }

}

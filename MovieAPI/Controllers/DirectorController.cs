using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;
using MovieAPI.DTOs;
namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DirectorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetDirectors(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null)
        {
            var query = _context.Director
                .Include(d => d.Movies) // Include related movies
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d => d.Name.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var directors = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(d => new DirectorDto
                {
                    DirectorId = d.DirectorId,
                    Name = d.Name,
                    Nationality = d.Nationality,
                    Age = d.Age,
                    Active = d.Active,
                    Movies = d.Movies != null ? d.Movies.Select(m => new MovieDto
                    {
                        MoviesId = m.MoviesId,
                        Name = m.Name,
                        ReleaseYear = m.ReleaseYear,
                        Gender = m.Gender,
                        Duration = m.Duration
                    }).ToList() : new List<MovieDto>()
                })
                .ToListAsync();

            return Ok(new
            {
                totalItems,
                page,
                pageSize,
                directors
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Director>> GetDirector(int id)
        {
            var director = await _context.Director.FindAsync(id);
            if (director == null) return NotFound();
            return director;
        }

        [HttpPost]
        public async Task<ActionResult<Director>> PostDirector(DirectorCreateDto dto)
        {
            var director = new Director
            {
                Name = dto.Name,
                Nationality = dto.Nationality,
                Age = dto.Age,
                Active = dto.Active
            };
            _context.Director.Add(director);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDirector), new { id = director.DirectorId }, director);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDirector(int id, DirectorCreateDto dto)
        {
            var director = new Director
            {
                DirectorId = id,
                Name = dto.Name,
                Nationality = dto.Nationality,
                Age = dto.Age,
                Active = dto.Active
            };
            if (id != director.DirectorId) return BadRequest();

            _context.Entry(director).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Director.Any(e => e.DirectorId == id)) return NotFound();
                throw;
            }

            return Ok(director);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var director = await _context.Director.FindAsync(id);
            if (director == null) return NotFound();

            _context.Director.Remove(director);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

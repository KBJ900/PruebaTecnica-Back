// DTOs/MovieCreateExample.cs
using Swashbuckle.AspNetCore.Filters;

namespace MovieAPI.DTOs
{
    public class MovieCreateExample : IExamplesProvider<MovieCreateDto>
    {
        public MovieCreateDto GetExamples()
        {
            return new MovieCreateDto
            {
                Name = "peli 2",
                ReleaseYear = DateTime.Parse("2025-06-12T23:45:21.534Z"),
                Gender = "Accion 1",
                Duration = TimeSpan.Parse("01:00:00"),
                DirectorId = 1
            };
        }
    }
}

using Swashbuckle.AspNetCore.Filters;

namespace MovieAPI.DTOs
{
    public class MovieUpdateExample : IExamplesProvider<MovieUpdateDto>
    {
        public MovieUpdateDto GetExamples()
        {
            return new MovieUpdateDto
            {
                Name = "Película actualizada",
                ReleaseYear = new DateTime(2024, 12, 1),
                Gender = "Drama",
                Duration = TimeSpan.FromMinutes(90),
                DirectorId = 2
            };
        }
    }
}

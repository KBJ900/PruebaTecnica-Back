namespace MovieAPI.DTOs
{
    public class MovieDto
    {
        public int MoviesId { get; set; }
        public string Name { get; set; }
        public DateTime? ReleaseYear { get; set; }
        public string Gender { get; set; }
        public TimeSpan? Duration { get; set; }
    }

    public class DirectorDto
    {
        public int DirectorId { get; set; }
        public string Name { get; set; }
        public string? Nationality { get; set; }
        public int Age { get; set; }
        public bool Active { get; set; }
        public List<MovieDto>? Movies { get; set; }
    }
}
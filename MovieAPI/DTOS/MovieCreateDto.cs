using System;

namespace MovieAPI.DTOs
{
    public class MovieCreateDto
    {
        public string Name { get; set; }
        public DateTime? ReleaseYear { get; set; }
        public string Gender { get; set; }
        public TimeSpan? Duration { get; set; }
        public int DirectorId { get; set; }
    }
}

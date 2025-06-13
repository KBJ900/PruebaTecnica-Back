using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models;

public class Movies
{
    public int MoviesId { get; set; }
    public string Name { get; set; }
    public DateTime? ReleaseYear { get; set; }
    public string Gender { get; set; }
    public TimeSpan? Duration { get; set; }
    public int DirectorId { get; set; }
    public Director? Director { get; set; }
}

